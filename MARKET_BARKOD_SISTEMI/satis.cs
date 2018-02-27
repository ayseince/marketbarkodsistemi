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
    public partial class satis : Form
    {
        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        public satis()
        {
            InitializeComponent();
            combo();
            combo2();
            combo3();
            
        }
        SqlCommand komut;
        public void combo()
        {
            label9.Text = "";
            label6.Text = "";
            label7.Text = "";
            baglan.Open();
            komut = new SqlCommand("Select barkod From urun", baglan);
            

            SqlDataReader oku = komut.ExecuteReader();
           
            while (oku.Read())
            {
                comboBox2.Items.Add(oku["barkod"].ToString());
               

            }
            baglan.Close();
        }
        public void combo2()
        {
            baglan.Open();
            komut = new SqlCommand("Select kasaid From kasa", baglan);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                comboBox1.Items.Add(oku["kasaid"].ToString());

            }
            baglan.Close();
        }
        public void combo3()
        {
            baglan.Open();
            komut = new SqlCommand("Select magazaadi From magaza", baglan);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                comboBox3.Items.Add(oku["magazaadi"].ToString().Trim());

            }
            baglan.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglan.Open();
            try
            {
               

                komut = new SqlCommand("Select satisfiyati,adi From urun where barkod='" + comboBox2.SelectedItem.ToString() + "'", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    String adi = (oku["adi"].ToString().Trim());
                    label9.Text = adi;

                    String fiyati = (oku["satisfiyati"].ToString().Trim());
                    label6.Text = fiyati;

                }

            }
            catch (Exception)
            {

                MessageBox.Show("hata");
            }

            baglan.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglan.Open();
            try
            {
                String deger;
                deger = comboBox1.SelectedItem.ToString();
                //MessageBox.Show(deger);

                komut = new SqlCommand("Select kasadacalisanadi From kasa where kasaid='" + comboBox1.Text + "'", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    String adi = (oku["kasadacalisanadi"].ToString().Trim());
                    label7.Text = adi;

                   
                }

            }
            catch (Exception)
            {

                MessageBox.Show("hata");
            }

            baglan.Close();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void satislarikaydet()
        {
            baglan.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO satis(satilanurunbarkod,urunsatisfiyati,satisyapilanurunadi,satisyapanmagazaid,satisyapankasiyerid) VALUES ((Select barkod from urun where barkod = '" + comboBox2.SelectedItem.ToString() + "'),(Select satisfiyati from urun where barkod = '" + comboBox2.SelectedItem.ToString() + "'),(Select adi from urun where barkod = '" + comboBox2.SelectedItem.ToString().Trim() + "'),(Select magazaid from magaza where magazaadi = '" + comboBox3.SelectedItem.ToString() + "'),(Select kasadacalisanid from kasa where kasaid = '" + comboBox1.Text + "'))", baglan);
            komut2.ExecuteReader();
            baglan.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            satiskontrol nesne = new satiskontrol();
            nesne.Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            baglan.Open();
            String deg = "(select satisfiyati from urun where barkod = '" + comboBox2.Text.ToString() + "')";
            DialogResult durum = MessageBox.Show("Satışı onaylıyor musunuz?", "Satış Onay!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == durum)
            {
                komut = new SqlCommand("Update magaza Set gunlukciro=gunlukciro+ " + deg + " where magazaadi= '" + comboBox3.Text.ToString() + "'", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                baglan.Close();
                satislarikaydet();
                label6.Text = "";
                label7.Text = "";
                label9.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
            else
            {
                label6.Text = "";
                label7.Text = "";
                label9.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }
    }
}
