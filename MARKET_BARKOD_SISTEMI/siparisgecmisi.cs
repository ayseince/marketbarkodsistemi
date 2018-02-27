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
    public partial class siparisgecmisi : Form
    {
        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        public siparisgecmisi()
        {
            InitializeComponent();
            goster();

        }
        public void goster()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From siparis ", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["siparisverilenurunbarkodu"].ToString();
                ekle.SubItems.Add(oku["siparisverilenurunkategori"].ToString().Trim());
                ekle.SubItems.Add(oku["siparisverilenurunadedi"].ToString());
                ekle.SubItems.Add(oku["siparisverencalisanid"].ToString());
                ekle.SubItems.Add(oku["siparisverenmagazaid"].ToString());
                listView1.Items.Add(ekle);

            }
            baglan.Close();
        }
        private void siparisgecmisi_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            siparis nesne = new siparis();
            nesne.Show();
            this.Hide();
        }
    }
}
