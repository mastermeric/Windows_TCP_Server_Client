using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// soket programlamada aşağıdakileri herzaman ekle
using System.Text;  // tip çevirme işlemleri...
using System.IO; // dosyalama işlemleri
using System.Net.Sockets;// soket işlemleri...
using System.Threading; // thread işlemleri...
using System.Net; // IPEndPoint icin

namespace WindowsFormsApplication1
{
    public partial class Server_TCPListener : Form
    {
        public Server_TCPListener()
        {
            InitializeComponent();
        }

        Thread kanal1;
        ThreadStart ts1;
        TcpClient istemci;
        TcpListener listener;
        Socket soket1;
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 25000);


        void TCPGelenmesajlariOku()
        {
            while(true)
            {
                try 
                {
                    listener = new TcpListener(endpoint);
                    listener.Start();
                    listener.AcceptSocket();
                    this.Text = listener.LocalEndpoint.ToString() + "  Bağlandı !"; // servera bağlanan makina
                }
                catch(SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ts1 = new ThreadStart(TCPGelenmesajlariOku);
            kanal1 = new Thread(ts1);
            kanal1.Start();
        }

        private void Server_TCPListener_Load(object sender, EventArgs e)
        {
            
        }
    }
}
