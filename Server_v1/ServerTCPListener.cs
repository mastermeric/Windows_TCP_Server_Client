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
    public partial class ServerTCPListener : Form
    {
        public ServerTCPListener()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        Socket Soket1;
        TcpListener TcpListener1;
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 1000);
        private void ServerTCPListener_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;  // gecici cozum
            //Bilgi alisverisi için bilgi almak istedigimiz port numarasini TcpListener sinifi ile gerçeklestiriyoruz
            TcpListener1 = new TcpListener(endpoint);
            TcpListener1.Start();
            listBox1.Items.Add("Sunucu baslatildi...");
            //Soket baglantimizi yapiyoruz.Bunu TcpListener sinifinin AcceptSocket metodu ile yaptigimiza dikkat edin
            Thread thread = new Thread(new ThreadStart(islemler));
            thread.Start();
        }

        void islemler()
        {
            Soket1 = TcpListener1.AcceptSocket();
            // Baglantının olup olmadığını kontrol ediyoruz
            if (!Soket1.Connected)
            {
                listBox1.Items.Add("Sunucu baslatilamiyor...");
            }
            else
            {
                //Sonsuz döngü sayesinde AgAkimini sürekli okuyoruz
                while (true)
                {
                    listBox1.Items.Add("Istemci baglantisi saglandi...");
                    //IstemciSoketi verilerini NetworkStream sinifi türünden nesneye aktariyoruz.
                    NetworkStream AgAkimi = new NetworkStream(Soket1);
                    //Soketteki bilgilerle islem yapabilmek için StreamReader ve StreamWriter siniflarini kullaniyoruz
                    StreamWriter AkimYazici = new StreamWriter(AgAkimi);
                    StreamReader AkimOkuyucu = new StreamReader(AgAkimi);


                    //StreamReader ile String veri tipine aktarma islemi önceden bir hata olursa bunu handle etmek gerek
                    try
                    {
                        string IstemciString = AkimOkuyucu.ReadLine();
                        listBox1.Items.Add(Soket1.RemoteEndPoint.ToString() + " Adresinden Gelen Bilgi:" + IstemciString );
                        //Istemciden gelen bilginin uzunlugu hesaplaniyor
                        int uzunluk = IstemciString.Length;
                        //AgAkimina, AkimYazını ile IstemciString inin uzunluğunu yazıyoruz
                        AkimYazici.WriteLine("Length: "+uzunluk.ToString());
                        AkimYazici.Flush();
                    }
                    catch
                    {
                        listBox1.Items.Add("Sunucu kapatiliyor...");
                        return;
                    }
                }
            }
            Soket1.Close();
            listBox1.Items.Add("Sunucu Kapatiliyor...");

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

    }
}
