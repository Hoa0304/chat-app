namespace ChatServer
{
    partial class ServerForm
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
            this.chatServer1 = new ChatServer();
            this.SuspendLayout();
            // 
            // chatServer1
            // 
            this.chatServer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatServer1.Location = new System.Drawing.Point(0, 0);
            this.chatServer1.Name = "chatServer1";
            this.chatServer1.Size = new System.Drawing.Size(800, 450);
            this.chatServer1.TabIndex = 0;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chatServer1);
            this.Name = "ServerForm";
            this.Text = "Chat Server";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
        }

        private ChatServer chatServer1;
    }
}