using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MARKET_BARKOD_SISTEMI
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            urun nesne = new urun();
            nesne.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            giris nesne = new giris();
            nesne.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            magazakontrol nesne = new magazakontrol();
            nesne.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            siparis nesne = new siparis();
            nesne.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
