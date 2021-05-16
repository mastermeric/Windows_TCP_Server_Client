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
    public partial class MesajGonder : Form
    {
        public MesajGonder()
        {
            InitializeComponent();
        }

        TcpListener dinleyici1 = new TcpListener(1000);
        // private NetworkStream networkakimi1;
        Socket soket1;

        private void button1_Click(object sender, EventArgs e)
        {
            // gonderilecek veri byte dizilerine dönüştürülmeli... aşağıdaki gibi
            string gidenveri = "DENEMEEEEEE";
            byte[] gidenbyte = Encoding.ASCII.GetBytes(gidenveri.ToCharArray());
            soket1.Send(gidenbyte, gidenbyte.Length, 0);
        }
    }
}