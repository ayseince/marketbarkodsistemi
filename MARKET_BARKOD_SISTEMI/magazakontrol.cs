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
    public partial class magazakontrol : Form
    {
        public magazakontrol()
        {
            InitializeComponent();
            magkontrol();
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";

        }

        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        SqlCommand komut;
        DataSet ds = new DataSet();
        public void magkontrol()
        {
            comboBox1.Items.Clear();
            baglan.Open();
            komut = new SqlCommand("Select * From magaza ", baglan);

            SqlDataReader oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            while (oku.Read())
            {
               
                comboBox1.Items.Add(oku["magazaadi"].ToString().Trim());

            }
            baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label9.Text = "";
            depoadogren();
            baglan.Open();
            try
            {
                

                komut = new SqlCommand("Select * From magaza where magazaadi='" +comboBox1.Text+"'", baglan);
                
                SqlDataReader oku = komut.ExecuteReader();
                
                while (oku.Read())
                {
                    String bolge = (oku["magazabolge"].ToString().Trim());
                    label6.Text = bolge;
                    String personalsayisi= (oku["magazacalisansayisi"].ToString().Trim());
                    label8.Text = personalsayisi;

                    Decimal ad = (Decimal)(oku["gunlukciro"]);
                    

                    label7.Text = String.Format(ad.ToString());

                }
                

            }
            catch (Exception)
            {

                MessageBox.Show("hata");
            }
            
                baglan.Close();
        }
        public void depoadogren()
        {
            label9.Text = "";
            baglan.Open();
            try
            {


                komut = new SqlCommand("Select depoadi From depo where depoid IN (Select baglioldugudepoid From magaza where magazaadi='" + comboBox1.Text + "')", baglan);

                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    String bolge = (oku["depoadi"].ToString().Trim());
                    label9.Text = bolge;
                }


            }
            catch (Exception)
            {

                MessageBox.Show("hata");
            }

            baglan.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }
    }
}
