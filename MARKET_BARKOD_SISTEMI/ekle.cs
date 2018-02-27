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
namespace MARKET_BARKOD_SISTEMI
{

    public partial class ekle : Form
    {
        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        //SqlConnection baglan = new SqlConnection("Data Source=GENC4Y10;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        
        public ekle()
        {
            InitializeComponent();
            combo();
        }
        SqlCommand komut;
        urun nesne = new urun();
        public void combo()
        {

            baglan.Open();
            komut = new SqlCommand("Select depoid From depo", baglan);


            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                comboBox1.Items.Add(oku["depoid"].ToString());


            }
            baglan.Close();

        }

        public void kontrolet()
        {
            baglan.Open();

            try
            {
                
                String sql = "Select barkod From urun where barkod=@barkd";
                SqlParameter prm1 = new SqlParameter("barkd", textBox1.Text.Trim());
                SqlCommand komut = new SqlCommand(sql, baglan);
                komut.Parameters.Add(prm1);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DialogResult durum = MessageBox.Show("Ürün mevcut, güncellemek ister misiniz?", "Ekleme Onay!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == durum)
                    {
                        guncelle();
                        depoguncelle();


                    }
                    else
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                       
                        textBox6.Clear();
                        textBox7.Clear();
                    }


                        
                    //baglan.Close();
                }
                else
                    urunekle();
                

            }
            catch (Exception)
            {
                urunekle();
                MessageBox.Show("hataaaaa! ", " Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                //baglan.Close();
        }

        public void kontrol2()
        {
            baglan.Open();
            try
            {

                String sql = "Select envanterdepoid From envanterdepo where envanterdepoid=@envanterdepoid";
                SqlParameter prm1 = new SqlParameter("envanterdepoid", comboBox1.Text.ToString());
                SqlCommand komut2 = new SqlCommand(sql, baglan);
                komut2.Parameters.Add(prm1);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut2);
                da.Fill(dt);
                komut2.ExecuteReader();
                if (dt.Rows.Count > 0)
                {

                    
                }
                    


            }

            catch (Exception)
            {
                //urunekle();
                MessageBox.Show("hata! ", " Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglan.Close();
        }

        public void depoguncelle()
        {
            komut = new SqlCommand("Update envanterdepo Set envanterdepoadet=" + textBox4.Text + " where envanterdepoid='" + comboBox1.Text.ToString() + "' and envanterdepobarkod IN (Select envanterdepobarkod From envanterdepo where envanterdepobarkod='" + textBox1.Text + "') ", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            baglan.Close();
            comboBox1.Text = "";
            textBox4.Clear();
            //komut.ExecuteReader();
            MessageBox.Show("envanterdepo güncellendi");
            baglan.Close();

        }

        private void guncelle()
        {
         
            String kayit = "Update urun Set adi=@adi,kategoriid=@kategoriid,alisfiyati=@alisfiyati, satisfiyati=@satisfiyati where barkod=@barkod";
            SqlCommand komut = new SqlCommand(kayit, baglan);
            komut.Parameters.AddWithValue("@barkod", textBox1.Text);
            komut.Parameters.AddWithValue("@adi", textBox2.Text);
            komut.Parameters.AddWithValue("@kategoriid", textBox3.Text);
            komut.Parameters.AddWithValue("@alisfiyati", textBox6.Text);
            komut.Parameters.AddWithValue("@satisfiyati", textBox7.Text);
            //depoadetguncelle();
            MessageBox.Show("Güncelleme yapıldı.", " Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            
            textBox6.Clear();
            textBox7.Clear();
            //baglan.Close();
            komut.ExecuteNonQuery();
            //baglan.Close();



        }

        public void urunekle()
        {
                      
                    //baglan.Open();
                    SqlCommand komut = new SqlCommand("Insert into urun(barkod,adi,kategoriid,alisfiyati,satisfiyati) Values('" + textBox1.Text + " ', ' " + textBox2.Text.ToString() + " ', ' " + textBox3.Text + " ', ' " + textBox6.Text + " ', ' " + textBox7.Text + " ')", baglan);
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    nesne.goster();
                    MessageBox.Show("Ürün veritabanına eklendi! ", " Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox1.Text= "";
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox4.Clear();


            baglan.Open();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            urun form1 = new urun();
            form1.Show();
            this.Hide();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           kontrolet();
           //kontrol2();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            kontrolet();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }
    }
    

}
