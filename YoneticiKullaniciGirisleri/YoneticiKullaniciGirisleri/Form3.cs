using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace YoneticiKullaniciGirisleri
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=BILGPROG-37\\SQLEXPRESS; Initial Catalog=personel; Persist Security Info=False; User ID=sa; Password=1");

        private void Form3_Load(object sender, EventArgs e)
        {

            lblTcNo.Text = Form1.tcNo;
            lblAdi.Text = Form1.adi;
            lblSoyadi.Text = Form1.soyadi;
            lblYetki.Text = Form1.yetki;

            kullanicilari_goster();

            //picturebox işlemleri
            pictureBox1.Width = 150;
            pictureBox1.Height = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciResimler\\" + Form1.tcNo + ".jpg");
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciResimler\\resimYok.jpg");
            }

            //kullanıcı işlemleri sekmesi

            this.Text = "Yönetici İşlemleri";
            label12.ForeColor = Color.DarkBlue;
            label13.Text = Form1.adi + " " + Form1.soyadi;
            txttc.MaxLength = 11;
            txtkadi.MaxLength = 8;
            rbyonetici.Checked = true;
            txtparola.MaxLength = 10;
            txtparolatkr.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            txtadi.CharacterCasing = CharacterCasing.Upper;
            txtsoyadi.CharacterCasing = CharacterCasing.Upper;
            toolTip1.SetToolTip(this.txttc, "Tc Kimlik No 11 Karakter Olmalı.");

        }

        private void kullanicilari_goster()
        {
            try
            {

                con.Open();

                SqlCommand cmd = new SqlCommand("select tcNo as [TC],ad as[AD], soyad as[SOYAD],yetki as[YETKİ],kullaniciAdi as[KULLANICI ADI], parola as[PAROLA] from kullanicilar", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adp.Fill(table);
                dataGridView1.DataSource = table;
                con.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }

        }

        private void txttc_TextChanged(object sender, EventArgs e)
        {
            if (txttc.Text.Length < 11)
            {
                errorProvider1.SetError(txttc, "tc no 11 karakter olmalı");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txttc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayı ve boşluk girilsin

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtkadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtkadi_TextChanged(object sender, EventArgs e)
        {
            if (txtkadi.Text.Length != 8)
            {
                errorProvider1.SetError(txttc, "kullanıcı adı 8 karakter olmalı");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtparolatkr_TextChanged(object sender, EventArgs e)
        {
            if (txtparolatkr.Text != txtparola.Text)
            {
                errorProvider1.SetError(txttc, "parola tekrarı eşleşmiyor");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            bool kayitkontrol = false;

            con.Open();
            SqlCommand cmd = new SqlCommand("select*from kullanicilar where tcNo='" + txttc.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read()) //girilen kullanıcı var mı
            {
                kayitkontrol = true;
                break;
            }
            con.Close();
            if (kayitkontrol == false)//böyle bir kullanıcı yoksa
            {
                //tc kimlik kontrol
                if (txttc.Text.Length < 11 || txttc.Text == "")
                {
                    label1.ForeColor = Color.Red;
                }
                else
                {
                    label1.ForeColor = Color.Black;

                }
                if (txtadi.Text.Length > 3 || txtadi.Text == "")
                {
                    label2.ForeColor = Color.Red;
                }
                else
                {
                    label2.ForeColor = Color.Black;

                }
                if (txtsoyadi.Text.Length > 3 || txtsoyadi.Text == "")
                {
                    label3.ForeColor = Color.Red;
                }
                else
                {
                    label3.ForeColor = Color.Black;

                }
                if (txtkadi.Text.Length != 8 || txtkadi.Text == "")
                {
                    label5.ForeColor = Color.Red;
                }
                else
                {
                    label5.ForeColor = Color.Black;

                }
                if (txtparolatkr.Text == txtparola.Text || txtparolatkr.Text == "")
                {
                    label7.ForeColor = Color.Red;
                }
                else
                {
                    label7.ForeColor = Color.Black;

                }


                //kayıt için hertürlü şart sağlanmıştır

                if (txttc.Text.Length==11 && txttc.Text!="" && txtadi.Text!="" && txtadi.Text.Length>3 && txtsoyadi.Text!="" && txtsoyadi.Text.Length>3 && txtkadi.Text.Length==8 && txtkadi.Text!="" && txtparola.Text==txtparolatkr.Text && txtparola.Text!=""&&txtparolatkr.Text!="" )
                {

                }
                else
                {
                    MessageBox.Show("yazı rengi kırmızı olanları gözden geçirin");
                }
            }
            else
            {
                MessageBox.Show("girilen tc no daha önceden kayıtlıdır...");
            }

        }
    }
}
