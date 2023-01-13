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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=B:\GAD Project\Car-Rental-Portal-Using-PHP-and-MySQL-V2.4\GAD CW\carRental.mdf;Integrated Security = True; Connect Timeout = 30");
        private void populate()
        {
            con.Open();
            string query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            Rentdgv.DataSource = ds.Tables[0];
            con.Close();

        }
        private void populateReturn()
        {
            con.Open();
            string query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            Returndgv.DataSource = ds.Tables[0];
            con.Close();

        }
        private void Deleteonreturn()
        {
            Rentdgv.CurrentRow.Selected = true;
            int rentId;
            rentId =Convert.ToInt32 (Rentdgv.SelectedRows[0].Cells[0].Value.ToString());
            
            con.Open();
            string query = "delete from RentalTbl where RentId=" + rentId + ";";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Rental details deleted succesfully");
            con.Close();
            //UpdateonRentDelete();
            populate();
        }


        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populateReturn();
        }

       
        private void Rentdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Rentdgv.CurrentRow.Selected = true;

            CarRegTb.Text = Rentdgv.Rows[e.RowIndex].Cells["carReg"].Value.ToString();
            CustNameTb.Text = Rentdgv.Rows[e.RowIndex].Cells["CustName"].Value.ToString();
            ReturnDateTb.Text = Rentdgv.Rows[e.RowIndex].Cells["ReturnDate"].Value.ToString();
            Total.Text= Rentdgv.Rows[e.RowIndex].Cells["RentFee"].Value.ToString();
            DateTime d1 = ReturnDateTb.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2 - d1;
            int NoofDays = Convert.ToInt32(t.TotalDays);
            if (NoofDays <= 0)
            {
                DelayTb.Text = "No Delay";
                FineTb.Text = "No Fine";
                Total.Text = Total.Text;
            }
            else
            {
                DelayTb.Text = "" + NoofDays;
                FineTb.Text = "" + (NoofDays * 250);
                int num1, num2,x;
                num1 = Convert.ToInt32(Total.Text);
                num2 = Convert.ToInt32(FineTb.Text);
                x = num1 + num2;
                Total.Text = Convert.ToString(x);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (IDTb.Text == "" || CustNameTb.Text == "" || FineTb.Text==""||DelayTb.Text==""||DelayTb.Text=="")
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into ReturnTbl values ( '" + IDTb.Text + "','" + CarRegTb.Text + "' ,'" + CustNameTb.Text + "','" + ReturnDateTb.Text + "','" + DelayTb.Text + "','" + FineTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Returned");
                    con.Close();
                    //UpdateonRent();
                    populateReturn();
                    Deleteonreturn();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
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
                    string query = "delete from ReturnTbl where ReturnId=" + IDTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental details deleted succesfully");
                    con.Close();
                    populateReturn();
                   // UpdateonRentDelete();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);

                }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
    }


