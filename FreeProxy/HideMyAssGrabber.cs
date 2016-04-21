using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace FreeProxy
{
    class Proxy
    {
        public string country, port, protocol, anonimity, ip, speed;
        public Proxy(string country, string port, string protocol, string anon, string ip, string speed)
        {
            this.anonimity = anon;
            this.country = country;
            this.port = port;
            this.protocol = protocol;
            this.ip = ip;
            this.speed = speed; 
        }
        public string ToString()
        {
            return country + "\t" + port + "\t" + anonimity + "\t" + protocol + "\t" + ip + "\t" + speed;
        }
    }
    delegate void UpdateHandler();
    class HideMyAssGrabber
    {
        public string CountryRegex = "1x1.png\">\\s+[A-Z][a-zA-Z]\\w+".Replace(@"\\", @"\");
        public string PortRegex = "[0-9]\\d+\\s+<\\/td>".Replace(@"\\", @"\");
        public string ProtocolRegex = "<td>\\s+(HTTPS|HTTP|socks4\\/5)\\s+<\\/td>".Replace(@"\\", @"\"); 
        public string AnonRegex = "(None\\s+<\\/td|Medium\\s+<\\/td|Low\\s+<\\/td|High\\s+<\\/td|High \\+KA\\s+<\\/td)".Replace(@"\\", @"\");
        public string SpeedRegex = "width: ([0-9]\\d+|[0-9])%;".Replace(@"\\", @"\");
        public string IpaddrRegex = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}";


        public event UpdateHandler Updated;
        string URL = "http://proxylist.hidemyass.com/";
        private List<string> countries, ports, protocols, anonimity, ips, speeds;
        public List<Proxy> Proxies = new List<Proxy>();
        string HtmlResponse = string.Empty, HtmlRendered = string.Empty;

        public void Update()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(GetPage));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(URL);
            thread.Join();
            
            ExtractProxies();
            Proxies.Clear();
            for(int i = 0; i < countries.Count; i++) 
                Proxies.Add(new Proxy(countries[i], ports[i], protocols[i], anonimity[i], ips[i], speeds[i]));
            this.Updated();
        }
        List<string> Match(string regex, string text)
        {
            List<string> matches = new List<string>();
            foreach (Match match in new Regex(regex).Matches(text))
            {
                matches.Add(match.Value.Replace("1x1.png", "").Replace("\"", "").Replace("\n", "").Replace("<", "").Replace("/", "").Replace(">", "").Replace("td", "").Replace("\"flag \"", "").Replace("width: ", "").Replace(";", "").Trim());
            }
            return matches;
        }
        void GetPage(object addr)
        {
            var browser = new WebBrowser();

            browser.ScrollBarsEnabled = false;
            browser.Navigate((string)addr);
            while (browser.ReadyState != WebBrowserReadyState.Complete) Application.DoEvents();

            browser.ClientSize =
                new Size(browser.Document.Body.ScrollRectangle.Width + 300, browser.Document.Body.ScrollRectangle.Bottom);
            browser.ScrollBarsEnabled = false;
            Bitmap img =
                new Bitmap(browser.Document.Body.ScrollRectangle.Width, browser.Document.Body.ScrollRectangle.Bottom);
            browser.BringToFront();

            browser.Document.ExecCommand("SelectAll", false, null);
            browser.Document.ExecCommand("Copy", false, null);


            HtmlResponse = browser.Document.Body.InnerHtml;
            HtmlRendered = Clipboard.GetText().Replace("\n", "").Replace("\r", "").Trim();
            browser.Dispose();
        }

        void ExtractProxies()
        {
            ips = Match(IpaddrRegex, HtmlRendered);
            countries = Match(CountryRegex, HtmlResponse);
            ports = Match(PortRegex, HtmlResponse);
            protocols = Match(ProtocolRegex, HtmlResponse);
            anonimity = Match(AnonRegex, HtmlResponse);
            speeds = Match(SpeedRegex, HtmlResponse);
        }
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;


        public void setProxy(Proxy proxy, bool proxyEnabled)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot + "\\" + subkey;

            string proxyhost = proxy.ip + ":" + proxy.port;

            Registry.SetValue(keyName, "ProxyServer", proxyhost);
            Registry.SetValue(keyName, "ProxyEnable", proxyEnabled ? "1" : "0");

            // These lines implement the Interface in the beginning of program 
            // They cause the OS to refresh the settings, causing IP to realy update
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
    }
}
