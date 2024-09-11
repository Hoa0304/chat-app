using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ChatClient
{
    public partial class ChatClient : UserControl
    {
        private TcpClient client;
        private NetworkStream stream;
        private List<Message> messages = new List<Message>();
        private string clientName;
        

        private void siticoneCustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            Color solidColor = Color.FromArgb(70, 70, 100);

            using (SolidBrush brush = new SolidBrush(solidColor))
            {
                e.Graphics.FillRectangle(brush, siticoneCustomGradientPanel1.ClientRectangle);
            }
        }

        public ChatClient()
        {
            InitializeComponent();
            btnSend.Click += new EventHandler(btnSend_Click);
            ConnectToServer("127.0.0.1");
        }

        private async void ConnectToServer(string ipAddress)
        {
            client = new TcpClient();
            await client.ConnectAsync(IPAddress.Parse(ipAddress), 5000);
            stream = client.GetStream();

            clientName = PromptForClientName();
            SendMessage(clientName); 

            _ = Task.Run(ReceiveMessages);
        }
    }

    internal class Message
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public Message(string text)
        {
            Text = text;
            Timestamp = DateTime.Now;
        }
    }
}