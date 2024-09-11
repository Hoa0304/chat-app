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
            chatServer1.StartServer();
        }
    }
}