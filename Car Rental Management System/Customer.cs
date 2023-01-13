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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=B:\GAD Project\Car-Rental-Portal-Using-PHP-and-MySQL-V2.4\GAD CW\carRental.mdf;Integrated Security = True; Connect Timeout = 30");
        private void populate()
        {
            con.Open();
            string query = "select * from CustomerTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            Customerdgv.DataSource = ds.Tables[0];
            con.Close();

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

        private void btn1_Click(object sender, EventArgs e)
        {
            if (IDTb.Text == "" || NameTb.Text == "" || AddressTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into CustomerTbl values ( " + IDTb.Text + ",'" + NameTb.Text + "' ,  '" + AddressTb.Text + "','" + PhoneTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Successfully Added");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (IDTb.Text == "")
            {
                MessageBox.Show("missing information");
            }
            else
                try
                {
                    con.Open();
                    string query = "delete from CustomerTbl where CustId=" + IDTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer deleted succesfully");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);

                }

        }

        private void Customerdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDTb.Text = Customerdgv.SelectedRows[0].Cells[0].Value.ToString();
            NameTb.Text = Customerdgv.SelectedRows[0].Cells[1].Value.ToString();
            AddressTb.Text = Customerdgv.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = Customerdgv.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IDTb.Text == "" || NameTb.Text == "" || AddressTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update CustomerTbl set CustName='" + NameTb.Text + "',CustAdd='" + AddressTb.Text + "',Phone='" + PhoneTb.Text + "'where Custid='" + IDTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Successfully Updated");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
    }
}
