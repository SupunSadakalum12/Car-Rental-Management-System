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
namespace GAD_CW
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=B:\GAD Project\Car-Rental-Portal-Using-PHP-and-MySQL-V2.4\GAD CW\carRental.mdf;Integrated Security = True; Connect Timeout = 30");
        private void Dashboard_Load(object sender, EventArgs e)
        {
            string querycar = "select count (*) from CarTable";
            SqlDataAdapter sda = new SqlDataAdapter(querycar,con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Carlbl.Text = dt.Rows[0][0].ToString();

            string  querycust = "select count (*) from CustomerTbl";
            SqlDataAdapter sda1 = new SqlDataAdapter(querycust, con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            Custlbl.Text = dt1.Rows[0][0].ToString();

            string queryusers = "select count (*) from UserTbl";
            SqlDataAdapter sda2 = new SqlDataAdapter(queryusers, con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            userlbl.Text = dt2.Rows[0][0].ToString();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void userlbl_Click(object sender, EventArgs e)
        {

        }
    }
}
