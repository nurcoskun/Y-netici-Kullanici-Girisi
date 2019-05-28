using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoneticiKullaniciGirisleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Veritabanı bağlantısı için provider ve dosya yolu
        SqlConnection con = new SqlConnection("Data Source=BILGPROG-37\\SQLEXPRESS; Initial Catalog=personel; Persist Security Info=False; User ID=sa; Password=1");
        //Formlar arası veri aktarımıında kullanılacak değişkenler
        public static string tcNo, adi, soyadi, yetki;
        //Bu formda geçerli olacak değişkenler
        int hak = 3; bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi";
            this.AcceptButton = btnGiris;
            this.CancelButton = btnCikis;
            btnGiris.Enabled = true;
            lblGirisHak.Text = hak.ToString();
            rbYonetici.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;//form ekranın merkezinde gelsin
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            //con.Open();
            //MessageBox.Show("Bağlantı Sağlandı");
            //con.Close();
            if (hak != 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from kullanicilar", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())//içeride veri olduğu sürece dön
                {
                    if (rbYonetici.Checked == true)
                    {
                        if (dr["kullaniciAdi"].ToString() == txtKullaniciAdi.Text && dr["parola"].ToString() == txtParola.Text && dr["yetki"].ToString() == "Yönetici")
                        {
                            //Yönetici için şartlar sağlandı
                            durum = true;
                            tcNo = dr.GetValue(0).ToString();
                            adi = dr.GetValue(1).ToString();
                            soyadi = dr.GetValue(2).ToString();
                            yetki = dr.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;
                        }
                    }
                    if (rbKullanici.Checked == true)
                    {
                        if (dr["kullaniciAdi"].ToString() == txtKullaniciAdi.Text && dr["parola"].ToString() == txtParola.Text && dr["yetki"].ToString() == "Kullanıcı")
                        {
                            //Kullanıcı için şartlar sağlandı
                            durum = true;
                            tcNo = dr.GetValue(0).ToString();
                            adi = dr.GetValue(1).ToString();
                            soyadi = dr.GetValue(2).ToString();
                            yetki = dr.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break;
                        }
                    }
                }
                if (durum == false)
                {
                    hak--;
                }
                con.Close();
            }
            lblGirisHak.Text = hak.ToString();
            if (hak == 0)
            {
                btnGiris.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
