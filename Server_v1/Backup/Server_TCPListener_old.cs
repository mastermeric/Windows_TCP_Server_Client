/*
SERVER icin Notlar: 
  1) Client tan gelen connectionla  0.0.0.0  IP ile gorunuyorsa :
  IPAddress.Any  oldugu icin  local networkte her connected IP yi  0.0.0.0  yapar!  NORMAL dir.    External  IP lerde asagidakini kullan  //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("128.1.0.148"), 25000);

  2) Only one usage of each socket address (protocol/network address/port) is normally permitted
    Bu durum NORMAL dir,  cunku  TcpListener.Start();  metodu  zaten  formLoad  icinde 1 kere calismalidir, ButonClick ile degil


 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// soket islemleri
using System.Text;  // tip çevirme işlemleri...
using System.IO; // dosyalama işlemleri
using System.Net.Sockets;// soket işlemleri...
using System.Threading; // thread işlemleri...
using System.Net; // IPEndPoint icin

namespace WindowsFormsApplication1
{
    public partial class Server_TCPListener_old : Form
    {
        public Server_TCPListener_old()
        {
            InitializeComponent();
        }

        Thread kanal1;
        ThreadStart ts1;
        TcpClient istemci;
        TcpListener listener;
        Socket soket1;
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 25000);
        //  IPAddress.Any  -->  local networkte her connected IP 0.0.0.0  olur  !  NORMALdir.    External  IP lerde asagidakini kullan  //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("128.1.0.148"), 25000);


        

        //void TCPGelenmesajlariOku()
        //{
        //    soket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    while (true)
        //    {                
        //        if (!soket1.Connected)
        //        {
        //            try
        //            {
        //                listener = new TcpListener(endpoint);
        //                listener.Start();
        //                listener.AcceptSocket();
        //                this.Text = listener.LocalEndpoint.ToString() + "  Bağlandı !"; // servera bağlanan makina   0.0.0.0 olmasının nedeni  socket tum local networku dinliyor yani zaten -->  IPEndpoint.Any   =  0.0.0.0  dan dolayı
        //            }
        //            catch (SocketException ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }                    
        //         }
        //        else
        //            MessageBox.Show(" Zaten bağlantı açık");
        //    }
        //}

        void TCPGelenmesajlariOku()
        {
            soket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (!soket1.Connected)
                {
                    try
                    {
                        listener = new TcpListener(endpoint);
                        listener.Start();
                        listener.AcceptSocket();
                        this.Text = listener.LocalEndpoint.ToString() + "  Bağlandı !"; // servera bağlanan makina   0.0.0.0 olmasının nedeni  socket tum local networku dinliyor yani zaten -->  IPEndpoint.Any   =  0.0.0.0  dan dolayı
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show(" Zaten bağlantı açık");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //soket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Form.CheckForIllegalCrossThreadCalls = false;  //  Gecici cozum
            ts1 = new ThreadStart(TCPGelenmesajlariOku);
            kanal1 = new Thread(ts1);
            kanal1.Start();            
        }

        private void Server_TCPListener_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
                listener.Stop();
                label2.Text = soket1.Connected.ToString();

            //soket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //if (soket1.Connected)
            //{
            //    listener.Stop();
            //    label2.Text = soket1.Connected.ToString();
            //}
            //else
            //    MessageBox.Show("Bağlantı Zaten Kapalı");

        }
    }
}




