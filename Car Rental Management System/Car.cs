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
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=B:\GAD Project\Car-Rental-Portal-Using-PHP-and-MySQL-V2.4\GAD CW\carRental.mdf;Integrated Security = True; Connect Timeout = 30");
        private void populate()
        {
            con.Open();
            string query = "select * from CarTable";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            carsdgv.DataSource = ds.Tables[0];
            con.Close();

        }


        private void btn1_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == ""||PriceTb.Text=="")
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into CarTable values ( '" + RegNumTb.Text + "','" + BrandTb.Text + "' ,  '" + ModelTb.Text + "','"+Avaliablecb.SelectedItem.ToString()+"','"+PriceTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Added");
                    con.Close();
                   populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
        
       
        private void Car_Load(object sender, EventArgs e)
        {
            populate();
           
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "")
            {
                MessageBox.Show("missing information");
            }
            else
                try
                {
                    con.Open();
                    string query = "delete from CarTable where RegNum='" + RegNumTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car deleted succesfully");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);

                }
        }

        private void carsdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RegNumTb.Text = carsdgv.SelectedRows[0].Cells[0].Value.ToString();
            BrandTb.Text = carsdgv.SelectedRows[0].Cells[1].Value.ToString();
            ModelTb.Text = carsdgv.SelectedRows[0].Cells[2].Value.ToString();
            Avaliablecb.SelectedItem = carsdgv.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = carsdgv.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Sorry Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update CarTable set Brand='" + BrandTb.Text + "',Model='" + ModelTb.Text + "',Availiable='" + Avaliablecb.SelectedItem.ToString() + "',Price=" + PriceTb.Text + "where RegNum='" + RegNumTb.Text + "';";
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
       }    }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void Search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";
            if(Search.SelectedItem.ToString()== "Availiable")
            {
                flag = "Yes";
            }
            else
            {
                flag = "NO";
            }
            con.Open();
            string query = "select * from CarTable where Availiable='"+flag+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            carsdgv.DataSource = ds.Tables[0];
            con.Close();

        }
    }
}
