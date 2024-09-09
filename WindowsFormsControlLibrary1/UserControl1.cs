using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsControlLibrary1
{
    public partial class UserControl1: UserControl
    {
        private void siticoneCustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            Color solidColor = Color.FromArgb(70, 70, 100); // Màu đơn

            using (SolidBrush brush = new SolidBrush(solidColor))
            {
                e.Graphics.FillRectangle(brush, siticoneCustomGradientPanel1.ClientRectangle);
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {

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
