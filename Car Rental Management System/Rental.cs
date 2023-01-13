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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=B:\GAD Project\Car-Rental-Portal-Using-PHP-and-MySQL-V2.4\GAD CW\carRental.mdf;Integrated Security = True; Connect Timeout = 30");
        private void fillcombo()
        {
            con.Open();
            String query = "select  RegNum from CarTable where Availiable='"+"Yes"+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr ;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(string));
            dt.Load(rdr);
            CarRegcb.ValueMember = "RegNum";
            CarRegcb.DataSource = dt;

            con.Close();

        }
        private void fillCustomer()
        {
            con.Open();
            String query = "select  Custid from CustomerTbl";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Custid", typeof(int));
            dt.Load(rdr);
            Custcb.ValueMember = "Custid";
            Custcb.DataSource = dt;

            con.Close();

        }
        private void fetchCustName()
        {
            con.Open();
            string query = "select * from CustomerTbl where Custid=" + Custcb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            con.Close();



        }
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
        private void UpdateonRent()
        {
            con.Open();
            string query = "update CarTable set Availiable='" +"NO"+ "' where RegNum='" + CarRegcb.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
           // MessageBox.Show("User Successfully Updated");
            con.Close();
        }
        private void UpdateonRentDelete()
        {
            con.Open();
            string query = "update CarTable set Availiable='" + "Yes" + "' where RegNum='" + CarRegcb.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            // MessageBox.Show("User Successfully Updated");
            con.Close();
        }
        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustomer();
            populate();
        }

        private void CarRegcb_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void Custcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (IDTb.Text == "" || CustNameTb.Text == "" || FeesTb.Text == ""  )
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into RentalTbl values ( '" +IDTb.Text+ "','" +CarRegcb.SelectedValue.ToString()+ "' ,'" + CustNameTb.Text + "','" + RentDateTb.Text + "','"+ReturnDateTb.Text+"','"+ FeesTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Rented");
                    con.Close();
                    UpdateonRent();
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
                    string query = "delete from RentalTbl where RentId=" + IDTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental details deleted succesfully");
                    con.Close();
                    populate();
                    UpdateonRentDelete();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);

                }
        }

        private void Rentdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDTb.Text = Rentdgv.SelectedRows[0].Cells[0].Value.ToString();
            CarRegcb.SelectedValue = Rentdgv.SelectedRows[0].Cells[1].Value.ToString();
            CustNameTb.Text = Rentdgv.SelectedRows[0].Cells[3].Value.ToString();
            FeesTb.Text= Rentdgv.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
