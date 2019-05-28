using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoneticiKullaniciGirisleri
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblTcNo.Text = Form1.tcNo;
            lblAdi.Text = Form1.adi;
            lblSoyadi.Text = Form1.soyadi;
            lblYetki.Text = Form1.yetki;
        }
    }
}
