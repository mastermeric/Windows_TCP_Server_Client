/*
 CLIENT icin Notlar:
 - Server kapali ise bu hata gelir:
  "No connection could be made because the target machine actively refused it..."

 
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;  // tip çevirme işlemleri...
using System.IO; // dosyalama işlemleri
using System.Net.Sockets;// soket işlemleri...
using System.Threading; // thread işlemleri...
using System.Net; // IPEndPoint 

namespace WindowsFormsApplication1
{
    public partial class ClientMakina : Form
    {
        public ClientMakina()
        {
            InitializeComponent();
        }

        TcpClient client = new TcpClient();
        //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("192.168.1.2"),25000);// ev
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("128.1.0.80"), 25000);// ofis
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //client = new TcpClient();
                if (!client.Connected)
                {
                    client = new TcpClient();
                    client.Connect(endpoint);
                    NetworkStream netStream = client.GetStream();//  stream e  veri yaz  ve oku
                }
                else
                    MessageBox.Show("Zaten bağnatı var...");
                label1.Text = client.Connected.ToString();
            }

            catch(SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //client = new TcpClient();
            if (!client.Connected)
            {
                MessageBox.Show("Bağlantı Zaten Kapalı");
            }
            else 
            {
                client.Close();
            }                

            label1.Text = client.Connected.ToString();
        }
    }
}
