using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FreeProxy
{
    public partial class Form1 : Form
    {
        HideMyAssGrabber grabber;
        private List<Proxy> Proxies = new List<Proxy>();
        private readonly SynchronizationContext syncContext;
        public Form1()
        {
            InitializeComponent();
            grabber = new HideMyAssGrabber();
            grabber.Updated += grabber_Updated;
            syncContext = AsyncOperationManager.SynchronizationContext;
        }
        void grabber_Updated()
        {
            syncContext.Post(e => PopulateList(), null);    //cross-threading stuff            
        }
        void PopulateList()
        {
            Proxies = grabber.Proxies;
            ProxyList.Clear();
            ProxyList.Columns.Add("Country");
            ProxyList.Columns.Add("Anonomity");
            ProxyList.Columns.Add("Speed");
            ProxyList.Columns.Add("Protocol");
            ProxyList.Columns.Add("IP");
            ProxyList.Columns.Add("Port");
            for (int i = 0; i < Proxies.Count; i++)
            {
                var root = ProxyList.Items.Add(Proxies[i].country);
                root.SubItems.Add(Proxies[i].anonimity);
                root.SubItems.Add(Proxies[i].speed);
                root.SubItems.Add(Proxies[i].protocol);
                root.SubItems.Add(Proxies[i].ip);
                root.SubItems.Add(Proxies[i].port);

            }
            UpdateButton.UseWaitCursor = false;
            UpdateButton.Enabled = true;
            this.UseWaitCursor = false;
        }
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateButton.UseWaitCursor = true;
            UpdateButton.Enabled = false;
            this.UseWaitCursor = true;
            new Thread(new ThreadStart(grabber.Update)).Start();
            
        }
        private void UseProxyButton_Click(object sender, EventArgs e)
        {
            if (ProxyList.SelectedItems.Count == 0) return;
            grabber.setProxy(Proxies[ProxyList.SelectedIndices[0]], true);
            string prox = grabber.Proxies[ProxyList.SelectedIndices[0]].ip + ":" + grabber.Proxies[ProxyList.SelectedIndices[0]].port;
            this.Text = "Current Proxy : " + prox;
        }

        private void RemoveProxyButton_Click(object sender, EventArgs e)
        {
            grabber.setProxy(new Proxy("", "", "", "", "", ""), false);
        }
    }
}
