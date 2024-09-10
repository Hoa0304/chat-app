using System;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            // Khởi động server khi form được tải
            chatServer1.StartServer();
        }
    }
}