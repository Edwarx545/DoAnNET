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
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
            DisplayFee();
            FillStuId();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True");
        private void DisplayFee()
        {
            con.Open();
            string Query = "Select * from Fee";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void FillStuId()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select StId from Student", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StId", typeof(int));
            dt.Load(rdr);
            StuId.ValueMember = "StId";
            StuId.DataSource = dt;
            con.Close();
        }
        private void GetStuName()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student where StId=@SID", con);
            cmd.Parameters.AddWithValue("@SID", StuId.SelectedValue.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TxtStuName.Text = dr["StName"].ToString();
            }
            con.Close();
        }
        private void BtAdd_Click(object sender, EventArgs e)
        {
            if (TxtStuName.Text == "" || TxtAmt.Text == "")
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else{
                string paymentperiode;
                paymentperiode = DatePay.Value.Month.ToString() + "/" + DatePay.Value.Year.ToString();
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Fee where StId = '" + StuId.SelectedValue.ToString() + "' and Month= '" + paymentperiode.ToString() + "'",con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("there is no due for this month");
                }
                else
                {                                          
                        SqlCommand cmd = new SqlCommand("insert into Fee(StId, StName, Month, Amt) values (@Stid,@Stname,@month,@amt)", con);
                        cmd.Parameters.AddWithValue("@Stid", StuId.Text);
                        cmd.Parameters.AddWithValue("@Stname", TxtStuName.Text);
                        cmd.Parameters.AddWithValue("@month", paymentperiode);
                        cmd.Parameters.AddWithValue("@amt", TxtAmt.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" Đã Thêm phí thành công.");
                        con.Close();
                        DisplayFee();
                        Reset();                    
                } 
            }
        }

        private void Reset()
        {
            StuId.Text = "";
            TxtStuName.Text = "";
            TxtAmt.Text = "";
        }    

        private void Fees_Load(object sender, EventArgs e)
        {

        }

        private void BtBack_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StuId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStuName();
        }
    }
}
