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
    public partial class urun : Form
    {
        
        SqlConnection baglan = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        public urun()
        {
            InitializeComponent();
            goster();
        }
        
        public void goster()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From urun ",baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["barkod"].ToString();
                   ekle.SubItems.Add(oku["adi"].ToString().Trim());
                   ekle.SubItems.Add(oku["kategoriid"].ToString());
                   ekle.SubItems.Add(oku["alisfiyati"].ToString());
                   ekle.SubItems.Add(oku["satisfiyati"].ToString());
                  listView1.Items.Add(ekle);
                
            }
            baglan.Close();
        }
     

        //buton hareketleri
        //----------------------------------------------------------------------------

        
        //silme butonu hareketi
        private void button2_Click(object sender, EventArgs e)
        {
            silme silnesne = new silme();
            silnesne.Show();
            this.Hide();
        }
        //ekleme butonu hareketi
        private void button4_Click(object sender, EventArgs e)
        {
            ekle form2 = new ekle();
            form2.Show();
            this.Hide();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            home nesne = new home();
            nesne.Show();
            this.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
