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
    public partial class TCPClient : Form
    {
        //Burda server da tanımladıklarımızdan farklı olarak TcpClient sınıfı ile serverdan gelen bilgileri alıyoruz
        public TcpClient Istemci = new TcpClient();
        private NetworkStream myNetworkStream;
        private StreamReader myStreamReader;
        private StreamWriter myStreamWriter;

        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("192.168.1.101"), 1000);
        //ofisPC: 128.1.0.80    ofisLAPTOP: 128.1.0.107  evLAPTOP:192.168.1.5   evPC:192.168.1.50
        
        public TCPClient()
        {
            InitializeComponent();
        }

        private void TCPClient_Load(object sender, EventArgs e)
        {
            try
            {
                Istemci = new TcpClient();
                Istemci.Connect(endpoint);
            }
            catch
            {
                MessageBox.Show("Baglanamadi");
                return;
            }
            //Server programında yaptıklarımızı burda da yapıyoruz.
            myNetworkStream = Istemci.GetStream();
            myStreamReader = new StreamReader(myNetworkStream);
            myStreamWriter = new StreamWriter(myNetworkStream);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Kullanıcı butona her tıkladığında textbox'ta yazı yoksa uyarı veriyoruz
            //Sonra AkimYazici vasıtası ile AgAkımına veriyi gönderip sunucudan gelen
            //cevabı AkimOkuyucu ile alıp Mesaj la kullanıcıya gösteriyoruz
            //Tabi olası hatalara karşı, Sunucuya bağlanmada hata oluştu mesajı veriyoruz.
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Lütfen bir yazi giriniz", "Uyari");
                    textBox1.Focus();
                    return;
                }

                string yazi;
                myStreamWriter.WriteLine(textBox1.Text);
                myStreamWriter.Flush();
                yazi = myStreamReader.ReadLine();
                listBox1.Items.Add("Sunucu cevabı:  "+yazi );
            }

            catch
            {
                MessageBox.Show("Sunucuya baglanmada hata oldu...");
            }
            textBox1.Text = "";
        }

        private void TCPClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                myStreamWriter.Close();
                myStreamReader.Close();
                myNetworkStream.Close();
            }

            catch
            {
                MessageBox.Show("Düzgün kapatilamiyor");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] SendData = {0x43,0x42,0x41,0x42,0x43,0x30,0x30,0xff,0xff};
            string mesaj = ASCIIEncoding.GetEncoding(0).GetString(SendData);
            try
            {
                string yazi2;
                myStreamWriter.WriteLine(mesaj);
                myStreamWriter.Flush();
                yazi2 = myStreamReader.ReadLine();
                listBox1.Items.Add("Sunucu cevabı:  " + yazi2);
            }
            catch
            {
                MessageBox.Show("Sunucuya baglanmada hata oldu...");
            }
        }
    }
}
