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
    public partial class satiskontrol : Form
    {
        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        public satiskontrol()
        {
            InitializeComponent();
            goster();
        }

        

        public void goster()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From satis ", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["satilanurunbarkod"].ToString();
                ekle.SubItems.Add(oku["urunsatisfiyati"].ToString());
                ekle.SubItems.Add(oku["satisyapilanurunadi"].ToString().Trim());
                ekle.SubItems.Add(oku["satisyapanmagazaid"].ToString());
                ekle.SubItems.Add(oku["satisyapankasiyerid"].ToString());
                
                listView1.Items.Add(ekle);

            }
            baglan.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }
        public void met()
        {
            try 
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT satilanurunbarkod From satis where satilanurunbarkod=@satilanurunbarkod", baglan);
                komut.Parameters.AddWithValue("@satilanurunbarkod", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    String barkodumuz = dr["satilanurunbarkod"].ToString();
                    //String adimiz = dr["adi"].ToString().Trim();
                    dr.Close();

                    DialogResult durum = MessageBox.Show(barkodumuz + " isimli ürünü siliyorsunuz!", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

                    if (DialogResult.Yes == durum)
                    {
                        String silmeSorgusu = "Delete From satis where satilanurunbarkod=@satilanurunbarkod";

                        SqlCommand silKomutu = new SqlCommand(silmeSorgusu, baglan);
                        silKomutu.Parameters.AddWithValue("@satilanurunbarkod", textBox1.Text);
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

        private void button4_Click(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            met();
            goster();
        }
    }
}
