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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=B:\GAD Project\Car-Rental-Portal-Using-PHP-and-MySQL-V2.4\GAD CW\carRental.mdf;Integrated Security = True; Connect Timeout = 30");
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            userdgv.DataSource = ds.Tables[0];
            con.Close();

        }
       
        private void Users_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btn1_Click_1(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into UserTbl values ( " + Uid.Text + ",'" + Uname.Text + "',  '" + Upass.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Added");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }

        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if(Uid.Text=="")
            {
                MessageBox.Show("missing information");
            }
            else
                try
                {
                    con.Open();
                    string query = "delete from UserTbl where Id=" + Uid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted succesfully");
                    con.Close();
                    populate();
                }catch(Exception Myex)
                {
                    MessageBox.Show(Myex.Message);

                }
        }

        private void userdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Uid.Text = userdgv.SelectedRows[0].Cells[0].Value.ToString();
            Uname.Text = userdgv.SelectedRows[0].Cells[1].Value.ToString();
            Upass.Text = userdgv.SelectedRows[0].Cells[2].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update userTbl set Uname='" + Uname.Text + "',Upass='" + Upass.Text + "'where Id=" + Uid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Updated");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();

        }
    }
}
