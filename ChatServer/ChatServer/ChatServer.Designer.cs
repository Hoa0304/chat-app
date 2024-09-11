using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;

namespace ChatServer
{
    partial class ChatServer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtLog = new SiticoneTextBox();
            this.txtMessage = new SiticoneTextBox();
            this.btnStart = new SiticoneButton();
            this.btnSend = new SiticoneButton();
            this.lstClients = new ListBox(); // New ListBox for clients
            this.SuspendLayout();

            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(200, 12);
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(588, 400);
            this.txtLog.TabIndex = 0;

            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(200, 418);
            this.txtMessage.Size = new System.Drawing.Size(680, 23);
            this.txtMessage.TabIndex = 1;

            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 418);
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start Server";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(700, 450);
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // 
            // lstClients
            // 
            this.lstClients.Location = new System.Drawing.Point(12, 12);
            this.lstClients.Size = new System.Drawing.Size(182, 400);
            this.lstClients.TabIndex = 4;

            // 
            // ChatServerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstClients); // Add ListBox to the controls
            this.Name = "ChatServerControl";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private SiticoneTextBox txtLog;
        private SiticoneTextBox txtMessage;
        private SiticoneButton btnStart;
        private SiticoneButton btnSend;
        private ListBox lstClients;
    }
}