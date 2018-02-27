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

namespace MARKET_BARKOD_SISTEMI
{
    public partial class silme : Form
    {
        public silme()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        public void sil()
        {
           
            String silmeSorgusu2 = "Delete From envanterdepo where envanterdepobarkod=@envanterdepobarkod";
             SqlCommand silKomutu2 = new SqlCommand(silmeSorgusu2, baglan);
            
            silKomutu2.Parameters.AddWithValue("@envantermagazabarkod", textBox1.Text);
            sil2();
            silKomutu2.ExecuteNonQuery();
            
           
        }
        public void sil2()
        {

           
            String silmeSorgusu3 = "Delete From envantermagaza where envantermagazabarkod=@envantermagazabarkod";
            SqlCommand silKomutu3 = new SqlCommand(silmeSorgusu3, baglan);
            
            silKomutu3.Parameters.AddWithValue("@envantermagazabarkod", textBox1.Text);
            sil3();
            silKomutu3.ExecuteNonQuery();
        }
        public void sil3()
        {


            String silmeSorgusu4 = "Delete From siparis where siparisverilenurunbarkodu=@siparisverilenurunbarkodu";
            SqlCommand silKomutu4 = new SqlCommand(silmeSorgusu4, baglan);

            silKomutu4.Parameters.AddWithValue("@siparisverilenurunbarkodu", textBox1.Text);
            silKomutu4.ExecuteNonQuery();
        }

        //çıkış
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT * From urun where barkod=@barkod", baglan);
                komut.Parameters.AddWithValue("@barkod", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    String barkodumuz = dr["barkod"].ToString();
                    String adimiz = dr["adi"].ToString().Trim();
                    dr.Close();

                    DialogResult durum = MessageBox.Show(barkodumuz + " barkodlu, " + adimiz + " isimli ürünü siliyorsunuz!", "Silme Onayı", MessageBoxButtons.YesNo,MessageBoxIcon.Hand);

                    if (DialogResult.Yes == durum)
                    {
                        String silmeSorgusu = "Delete From urun where barkod=@barkod";
                        String silmeSorgusu2 = "Delete From envanterdepo where envanterdepobarkod=@envanterdepobarkod";
                        String silmeSorgusu3 = "Delete From envantermagaza where envantermagazabarkod=@envanterdepobarkod";

                        SqlCommand silKomutu = new SqlCommand(silmeSorgusu, baglan);
                        SqlCommand silKomutu2 = new SqlCommand(silmeSorgusu2, baglan);
                        SqlCommand silKomutu3 = new SqlCommand(silmeSorgusu3, baglan);
                        silKomutu.Parameters.AddWithValue("@barkod", textBox1.Text);
                        sil();
                        silKomutu.ExecuteNonQuery();
                        
                        
                        MessageBox.Show("Ürün veritabanından silindi! ", " Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Ürün veritabanında bulunamadı! ", " Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                }
                baglan.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Hata! ", " Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }
    }
}
