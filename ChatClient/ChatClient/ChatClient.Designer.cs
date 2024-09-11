using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;

namespace ChatClient
{
    partial class ChatClient
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.siticoneCustomGradientPanel1 = new Siticone.Desktop.UI.WinForms.SiticoneCustomGradientPanel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 469);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(105)))));
            this.panel2.Location = new System.Drawing.Point(272, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(521, 54);
            this.panel2.TabIndex = 1;
            // 
            // siticoneCustomGradientPanel1
            // 
            this.siticoneCustomGradientPanel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.siticoneCustomGradientPanel1.Location = new System.Drawing.Point(272, 60);
            this.siticoneCustomGradientPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.siticoneCustomGradientPanel1.Name = "siticoneCustomGradientPanel1";
            this.siticoneCustomGradientPanel1.Size = new System.Drawing.Size(520, 349);
            this.siticoneCustomGradientPanel1.TabIndex = 3;
            this.siticoneCustomGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.siticoneCustomGradientPanel1_Paint);            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(288, 428);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(288, 20);
            this.txtMessage.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(646, 428);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(56, 19);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.siticoneCustomGradientPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChatClient";
            this.Size = new System.Drawing.Size(794, 470);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private string PromptForClientName()
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter your name:", "Client Name", "Client");
            return string.IsNullOrEmpty(name) ? "Client" : name;
        }

        private async void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string messageText = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                AddMessage(messageText);
            }
        }

        private void AddMessage(string messageText)
        {
            if (messageText.StartsWith("Server: "))
            {
                Message newMessage = new Message(messageText);
                messages.Add(newMessage);
            }
            else
            {
                Message newMessage = new Message($"{clientName}: {messageText}");
                messages.Add(newMessage);
            }

            DisplayMessages();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string messageText = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(messageText))
            {
                // Send message to server
                SendMessage(messageText);
                txtMessage.Clear();

                // Create a new message and display it
                Message newMessage = new Message(messageText);
                messages.Add(newMessage);
                DisplayMessages();
            }
        }

        private async void SendMessage(string messageText)
        {
            byte[] data = Encoding.UTF8.GetBytes(messageText);
            await stream.WriteAsync(data, 0, data.Length);
        }

        private void DisplayMessages()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(DisplayMessages));
                return;
            }
            siticoneCustomGradientPanel1.Controls.Clear(); // Clear old messages
            int yPos = 10; // Starting Y position

            foreach (var message in messages)
            {
                // Create a panel for each message
                Panel messagePanel = new Panel
                {
                    AutoSize = true,
                    MaximumSize = new Size(250, 0),
                    Location = new Point(10, yPos),
                    Padding = new Padding(5),
                    BorderStyle = BorderStyle.None,
                    BackColor = Color.FromArgb(70, 70, 100),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left
                };

                messagePanel.Paint += (s, e) =>
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        int radius = 10;
                        path.AddArc(0, 0, radius, radius, 180, 90);
                        path.AddArc(messagePanel.Width - radius, 0, radius, radius, 270, 90);
                        path.AddArc(messagePanel.Width - radius, messagePanel.Height - radius, radius, radius, 0, 90);
                        path.AddArc(0, messagePanel.Height - radius, radius, radius, 90, 90);
                        path.CloseFigure();

                        messagePanel.Region = new Region(path);

                        using (LinearGradientBrush brush = new LinearGradientBrush(
                            messagePanel.ClientRectangle,
                            ColorTranslator.FromHtml("#7F00FF"),
                            ColorTranslator.FromHtml("#E100FF"),
                            LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillRectangle(brush, messagePanel.ClientRectangle);
                        }
                    }
                };

                Label messageLabel = new Label
                {
                    Text = $"{message.Timestamp.ToShortTimeString()} [{clientName}]: {message.Text}",
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                messagePanel.Controls.Add(messageLabel);
                siticoneCustomGradientPanel1.Controls.Add(messagePanel);
                yPos += messagePanel.Height + 5;
            }

        }
        
        #region Designer variables
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Siticone.Desktop.UI.WinForms.SiticoneCustomGradientPanel siticoneCustomGradientPanel1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        #endregion
    }
}