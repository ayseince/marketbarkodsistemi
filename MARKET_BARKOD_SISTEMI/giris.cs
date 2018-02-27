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
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
           
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LENOVO-PC;Initial Catalog=MARKET_BARKOD_SISTEMI;Integrated Security=True");
        public void girisyap()
        {
           try
            {
                baglan.Open();
                String sql = "Select * From giris where kullaniciadi=@adi AND sifre=@sifresi";
                SqlParameter prm1 = new SqlParameter("adi",textBox1.Text.Trim());
                SqlParameter prm2 = new SqlParameter("sifresi", textBox2.Text.Trim());
                SqlCommand komut = new SqlCommand(sql,baglan);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                if (dt.Rows.Count >0)
                {
                    //MessageBox.Show("Giriş yapıldı ", " Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    satis nesne = new satis();
                    nesne.Show();
                    this.Hide();
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    MessageBox.Show("Hatalı kullanıcı adı ve şifre ", " Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                

            }
            catch
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("Hatalı kullanıcı adı ve şifre ", " Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglan.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home ur = new home();
            ur.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            girisyap();
        }
    }
}
