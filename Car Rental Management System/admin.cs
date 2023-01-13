using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAD_CW
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(UnameAD.Text=="admin"&& PassAD.Text=="password")
            {
                Users use1 = new Users();
                use1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Password Or Username");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            UnameAD.Text = "";
            PassAD.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
