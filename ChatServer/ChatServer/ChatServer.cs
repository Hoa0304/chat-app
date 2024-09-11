using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;

namespace ChatServer
{
    public partial class ChatServer : UserControl
    {
        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>();
        private List<string> clientNames = new List<string>(); // Store client names

        public ChatServer()
        {
            InitializeComponent();
        }

        public async void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            LogMessage("Server started on port 5000.");

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                clients.Add(client);

                // Prompt for client name
                string clientName = $"Client {clients.Count}";
                clientNames.Add(clientName);
                UpdateClientList();

                LogMessage($"Client connected: {clientName}");
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    LogMessage($"Received from {clientNames[clients.IndexOf(client)]}: {message}");
                }
            }

            lock (clients)
            {
                int index = clients.IndexOf(client);
                clients.Remove(client);
                clientNames.RemoveAt(index);
                UpdateClientList();
            }
            LogMessage("Client disconnected.");
        }

        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogMessage), message);
                return;
            }

            txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
        }

        private void UpdateClientList()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateClientList));
                return;
            }

            lstClients.Items.Clear();
            foreach (var name in clientNames)
            {
                lstClients.Items.Add(name);
            }
        }

        private void ChatServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener?.Stop();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string messageText = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(messageText)) return;

            byte[] data = Encoding.UTF8.GetBytes(messageText);
            foreach (var client in clients)
            {
                if (client.Connected)
                {
                    var stream = client.GetStream();
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }

            LogMessage($"Sent: {messageText}");
            txtMessage.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartServer();
        }
    }
}