using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class ChatServer : UserControl
    {
        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>();

        public ChatServer()
        {
            InitializeComponent();
        }

        public async void StartServer() // Đảm bảo phương thức này có thể được gọi từ bên ngoài
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            LogMessage("Server started on port 5000.");

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                LogMessage("Client connected.");
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
                    LogMessage($"Received: {message}");
                    //BroadcastMessage(message, client);

                    //string responseMessage = $"Server received: {message}";
                    //byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                    //await stream.WriteAsync(responseData, 0, responseData.Length);
                }
            }

            lock (client)
            {
                clients.Remove(client);
            } // Xóa client khi ngắt kết nối
            LogMessage("Client disconnected.");
        }

        //private void BroadcastMessage(string message, TcpClient sender)
        //{
        //    byte[] data = Encoding.UTF8.GetBytes(message);
        //    foreach (var client in clients)
        //    {
        //        if (client != sender)
        //        {
        //            var stream = client.GetStream();
        //            stream.Write(data, 0, data.Length);
        //        }
        //    }
        //    LogMessage($"Broadcasted: {message}");
        //}

        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogMessage), message);
                return;
            }

            txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
        }

        private void ChatServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener?.Stop();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartServer(); // Gọi phương thức để bắt đầu server
        }
    }
}