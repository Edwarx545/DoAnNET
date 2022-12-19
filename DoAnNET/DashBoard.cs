using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnNET
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Baitap\Quanlinhanvien\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True");
        private void CountStudent()
        {
            con.Open();          
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Student", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StuNum.Text = dt.Rows[0][0].ToString();  
            con.Close();
        }
        private void CountTeacher()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Teacher", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TeNum.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountEvent()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Event", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            EvNum.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void SumFee()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(Amt) from Fee", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CoinL.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            CountStudent();
            CountTeacher();
            CountEvent();
            SumFee();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TeNum_Click(object sender, EventArgs e)
        {

        }
    }
}
