using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;

namespace WindowsFormsControlLibrary1
{
    partial class UserControl1 : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        private List<Message> messages = new List<Message>();

        public UserControl1()
        {
            InitializeComponent();
            btnSend.Click += new EventHandler(btnSend_Click);
        }

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
            this.siticoneCustomGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.siticoneCustomGradientPanel1_Paint);
            // 
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
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click_1);
            // 
            // UserControl1
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
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(794, 470);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string messageText = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(messageText))
            {
                // Tạo tin nhắn mới
                Message newMessage = new Message(messageText);
                messages.Add(newMessage);

                // Hiển thị tin nhắn
                DisplayMessages();

                // Xóa nội dung TextBox
                txtMessage.Clear();
            }
        }

        private void DisplayMessages()
        {
            siticoneCustomGradientPanel1.Controls.Clear(); // Xóa tin nhắn cũ
            int yPos = 10; // Vị trí Y bắt đầu

            foreach (var message in messages)
            {
                // Tạo một panel mới cho mỗi tin nhắn
                Panel messagePanel = new Panel
                {
                    AutoSize = true, // Kích thước panel tự động
                    MaximumSize = new Size(250, 0),
                    Location = new Point(10, yPos),
                    Padding = new Padding(5), // Thêm khoảng cách giữa viền và label
                    BorderStyle = BorderStyle.None, // Xóa viền
                    BackColor = Color.FromArgb(70, 70, 100),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left // Giữ vị trí
                };

                // Sự kiện Paint để vẽ gradient và border radius
                messagePanel.Paint += (s, e) =>
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        int radius = 10; // Đường kính bo góc
                        path.AddArc(0, 0, radius, radius, 180, 90); // Góc trên bên trái
                        path.AddArc(messagePanel.Width - radius, 0, radius, radius, 270, 90); // Góc trên bên phải
                        path.AddArc(messagePanel.Width - radius, messagePanel.Height - radius, radius, radius, 0, 90); // Góc dưới bên phải
                        path.AddArc(0, messagePanel.Height - radius, radius, radius, 90, 90); // Góc dưới bên trái
                        path.CloseFigure();

                        messagePanel.Region = new Region(path); // Thiết lập vùng cho panel

                        // Vẽ gradient
                        using (LinearGradientBrush brush = new LinearGradientBrush(
                            messagePanel.ClientRectangle,
                            ColorTranslator.FromHtml("#7F00FF"), // Màu bắt đầu
                            ColorTranslator.FromHtml("#E100FF"), // Màu kết thúc
                            LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillRectangle(brush, messagePanel.ClientRectangle);
                        }
                    }
                };

                // Tạo một label mới cho tin nhắn
                Label messageLabel = new Label
                {
                    Text = $"{message.Timestamp.ToShortTimeString()}: {message.Text}",
                    AutoSize = true, // Tự động điều chỉnh kích thước
                    BackColor = Color.Transparent, // Đặt nền trong suốt cho label
                    TextAlign = ContentAlignment.MiddleCenter // Căn giữa văn bản
                };

                // Thêm label vào panel
                messagePanel.Controls.Add(messageLabel);

                // Thêm panel vào siticoneCustomGradientPanel
                siticoneCustomGradientPanel1.Controls.Add(messagePanel);

                // Cập nhật vị trí cho tin nhắn tiếp theo
                yPos += messagePanel.Height + 5; // Tăng khoảng cách giữa các tin nhắn
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