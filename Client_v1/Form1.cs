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


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private TcpListener dinleyici1 = new TcpListener(1000);
        // private NetworkStream networkakimi1;
        private Socket soket1;
        private void button1_Click(object sender, EventArgs e)
        {          
            //okunan byteler  stringe dönüşmeli...  aşağıdaki gibi...
            soket1 = dinleyici1.AcceptSocket();  //
            byte[] okunan = new byte[1024];
            int boyut = soket1.Receive(okunan, okunan.Length, 0);
            string okunanveri = System.Text.Encoding.ASCII.GetString(okunan);
            listBox1.Items.Add(okunanveri);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            // gonderilecek veri byte dizilerine dönüştürülmeli... aşağıdaki gibi
            string gidenveri = textBox1.Text;
            byte[] gidenbyte = System.Text.Encoding.ASCII.GetBytes(gidenveri.ToCharArray());
            soket1.Send(gidenbyte, gidenbyte.Length, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dinleyici1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dinleyici1.Start();
        }
    }
}
