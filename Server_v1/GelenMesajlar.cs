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
    public partial class GelenMesajlar : Form
    {
        public GelenMesajlar()
        {
            InitializeComponent();
        }

        Thread kanal1;
        ThreadStart ts1;        
        TcpClient istemci ;
        TcpListener listener;
        Socket soket1;
        IPEndPoint endpoit ;
        
        void TCPMesajOku()
        {            
            while(true)
            {
                Thread.Sleep(500);
                try
                {
                    //Application.DoEvents(); 
                    soket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //endpoit = new IPEndPoint(IPAddress.Parse("192.168.1.3"), 1000);
                    endpoit = new IPEndPoint(IPAddress.Any, 1000);
                    soket1.Connect(endpoit);
                    byte[] buffer = new byte[512];  // length of the text "Hello world!"
                    //istemci = new TcpClient(endpoit);
                    listener = new TcpListener(endpoit);
                    soket1.Receive(buffer);
                    listBox1.Items.Add(Encoding.GetEncoding(0).GetString(buffer));
                    soket1.Close();
                }
                catch(SocketException ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

 
        private void GelenMesajlar_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ts1 = new ThreadStart(TCPMesajOku);
            kanal1 = new Thread(ts1);
            kanal1.Start();
        }
    }
}
