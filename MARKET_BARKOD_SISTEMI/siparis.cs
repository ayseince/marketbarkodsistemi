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
    public partial class siparis : Form
    {
        String magazaid, envanterdepobarkod, calisanadi;
        int tbadet=0, adedimiz=0;
        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        SqlCommand komut;
        public siparis()
        {
            InitializeComponent();
            magazacombo();
            label7.Text = "";
        }

        public void magazacombo()
        {
            baglan.Open();
            komut = new SqlCommand("Select magazaid From magaza", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                magazaid = oku["magazaid"].ToString();
                comboBox1.Items.Add(magazaid);
            }
            baglan.Close();



        }

        public void magazabarkodcombo()
        {
            baglan.Open();
            komut = new SqlCommand("select envanterdepobarkod from envanterdepo where envanterdepoid IN(select depoid from depo where depoid IN(select baglioldugudepoid from magaza where magazaid = '" + comboBox1.SelectedItem + "'))", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                envanterdepobarkod = oku["envanterdepobarkod"].ToString();
                comboBox2.Items.Add(envanterdepobarkod);
            }
            baglan.Close();



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = "";
            adetoneri();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adethesap();
            baglan.Close();
        }

        public void magazacalisancombo()
        {
            baglan.Open();
            komut = new SqlCommand("select calisanadi from calisan where calistigimagazaid IN(select magazaid from magaza where magazaid='" + comboBox1.SelectedItem + "')", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                calisanadi = oku["calisanadi"].ToString();
                comboBox3.Items.Add(calisanadi);
            }
            baglan.Close();
            magazabarkodcombo();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox3.Items.Clear();
            //comboBox2.Items.Clear();
            label7.Text = "";
            magazacalisancombo();
        }

        public void adetoneri()
        {
            label7.Text = "";
            baglan.Open();
            komut = new SqlCommand("select envanterdepoadet from envanterdepo where envanterdepobarkod='" + comboBox2.Text + "' and envanterdepoid IN (select envanterdepoid from envanterdepo where envanterdepoid IN(select depoid from depo where depoid IN(select baglioldugudepoid from magaza where magazaid='" + comboBox1.Text + "')))", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                String adet = oku["envanterdepoadet"].ToString();
                label7.Text = "0 - " + adet;
            }
            baglan.Close();
        }

        public void adethesap()
        {
            baglan.Open();
            try
            {
                komut = new SqlCommand("select envanterdepoadet from envanterdepo where envanterdepobarkod='" + comboBox2.Text + "' and envanterdepoid IN (select envanterdepoid from envanterdepo where envanterdepoid IN(select depoid from depo where depoid IN(select baglioldugudepoid from magaza where magazaid='" + comboBox1.Text + "')))", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    String adet = oku["envanterdepoadet"].ToString();
                    adedimiz = Convert.ToInt32(adet);
                    String a = textBox1.Text.ToString();
                    tbadet = Convert.ToInt32(a);

                    if (tbadet <= adedimiz)
                    {
                        adedimiz = adedimiz - tbadet;
                        Console.Write(adedimiz);
                        baglan.Close();
                        satisdepoguncelle(adedimiz);
                        //satiskaydet();
                        satiskaydet();
                        baglan.Open();
                    }
                    else
                    {
                        baglan.Close();
                        MessageBox.Show("fazla sayıda adet girdiniz");
                        textBox1.Text = "";


                        baglan.Open();
                        //break;
                    }

                    //baglan.Open();
                }
            }
            catch (Exception)
            {

                //MessageBox.Show("hata"); 
            }
            
            //baglan.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            urun nesne = new urun();
            nesne.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            siparisgecmisi nesne = new siparisgecmisi();
            nesne.Show();
            this.Hide();
        }

       
        private void button8_Click(object sender, EventArgs e)
        {
            adethesap();
            baglan.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }

        public void satiskaydet()
        {
            //baglan.Close();
            baglan.Open();
            String de = "(select calisanid from calisan where calisanadi='"+comboBox3.Text.Trim()+"')";
            komut = new SqlCommand("Insert into siparis(siparisverilenurunbarkodu,siparisverilenurunkategori,siparisverilenurunadedi,siparisverencalisanid,siparisverenmagazaid) Values(' " + comboBox2.Text.ToString() + " ', ' " + textBox2.Text + " '," + textBox1.Text + " ,  " + de + " , ' " + comboBox1.Text + " ')", baglan);
            komut.ExecuteReader();
            baglan.Close();
            //MessageBox.Show("satis kaydedildi");
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            label7.Text = "";
        }

        public void satisdepoguncelle( int adedimiz)
        {
            baglan.Open();
            komut = new SqlCommand("Update envanterdepo Set envanterdepoadet=" + adedimiz + " where envanterdepobarkod='" + comboBox2.Text.ToString() + "' and envanterdepoid IN (Select envanterdepoid From envanterdepo where envanterdepoid IN (select baglioldugudepoid from magaza where magazaid='" + comboBox1.Text + "')) ", baglan);
            komut.ExecuteReader();
            baglan.Close();
            magazaguncelle();
            //MessageBox.Show("depo güncellendi");

            //adedimiz = 0;
        }

        public void magazaguncelle()
        {
            baglan.Open();
            komut = new SqlCommand("Update envantermagaza Set envantermagazaadet=envantermagazaadet+" + tbadet + " where envantermagazabarkod='" + comboBox2.Text.ToString() + "' and envantermagazaid IN (Select envantermagazaid From envantermagaza where envantermagazaid IN (select baglioldugudepoid from magaza where magazaid='" + comboBox1.Text + "')) ", baglan);
            komut.ExecuteReader();
            baglan.Close();
            MessageBox.Show("Sipariş tamamlandı!");
        }
    }
}
