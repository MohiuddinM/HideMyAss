namespace FreeProxy
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UpdateButton = new System.Windows.Forms.Button();
            this.UseProxyButton = new System.Windows.Forms.Button();
            this.ProxyList = new System.Windows.Forms.ListView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.RemoveProxyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(12, 230);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 1;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // UseProxyButton
            // 
            this.UseProxyButton.Location = new System.Drawing.Point(384, 230);
            this.UseProxyButton.Name = "UseProxyButton";
            this.UseProxyButton.Size = new System.Drawing.Size(75, 23);
            this.UseProxyButton.TabIndex = 2;
            this.UseProxyButton.Text = "Use Proxy";
            this.UseProxyButton.UseVisualStyleBackColor = true;
            this.UseProxyButton.Click += new System.EventHandler(this.UseProxyButton_Click);
            // 
            // ProxyList
            // 
            this.ProxyList.Location = new System.Drawing.Point(12, 12);
            this.ProxyList.MultiSelect = false;
            this.ProxyList.Name = "ProxyList";
            this.ProxyList.Size = new System.Drawing.Size(447, 212);
            this.ProxyList.TabIndex = 3;
            this.ProxyList.UseCompatibleStateImageBehavior = false;
            this.ProxyList.View = System.Windows.Forms.View.Details;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(198, 234);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(49, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Busy";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // RemoveProxyButton
            // 
            this.RemoveProxyButton.Location = new System.Drawing.Point(303, 230);
            this.RemoveProxyButton.Name = "RemoveProxyButton";
            this.RemoveProxyButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveProxyButton.TabIndex = 5;
            this.RemoveProxyButton.Text = "Remove";
            this.RemoveProxyButton.UseVisualStyleBackColor = true;
            this.RemoveProxyButton.Click += new System.EventHandler(this.RemoveProxyButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 261);
            this.Controls.Add(this.RemoveProxyButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.ProxyList);
            this.Controls.Add(this.UseProxyButton);
            this.Controls.Add(this.UpdateButton);
            this.Name = "Form1";
            this.Text = "FreeProxy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button UseProxyButton;
        private System.Windows.Forms.ListView ProxyList;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button RemoveProxyButton;
    }
}

