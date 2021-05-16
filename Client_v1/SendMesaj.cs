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
    public partial class SendMesaj : Form
    {
        public SendMesaj()
        {
            InitializeComponent();
        }


        Thread kanal1;
        ThreadStart ts1;

        private Socket soket1;
        int sayac = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            ts1 = new ThreadStart(TCPMesajGonder);
            kanal1 = new Thread(ts1);
            
            kanal1.Start();
        }


        void TCPMesajGonder()
        {
            while (true)
            {
                Thread.Sleep(1500);
                try
                {
                    Application.DoEvents();
                    soket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("192.168.1.2"), 1000);
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 1000);
                    soket1.Connect(endpoint);
                    // gonderilecek veri byte dizilerine dönüştürülmeli... aşağıdaki gibi
                    string gidenveri = "GİDENLER...";
                    byte[] gidenbyte = Encoding.ASCII.GetBytes(gidenveri.ToCharArray());
                    soket1.Send(gidenbyte, gidenbyte.Length, 0);
                    soket1.Close();

                }
                catch (SocketException ex)
                { }
            }
        }

        private void SendMesaj_Load(object sender, EventArgs e)
        {

        }
    }
}
