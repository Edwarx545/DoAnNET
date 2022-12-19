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
    public partial class Attendances : Form
    {
        public Attendances()
        {
            InitializeComponent();
            DisplayAttendance();
            FillStuId();
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
            foreach(DataRow dr in dt.Rows)
            {
                TxtName.Text = dr["StName"].ToString();
            }
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Baitap\Quanlinhanvien\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True");
        private void DisplayAttendance()
        {
            con.Open();
            string Query = "Select * from Attendance";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void BtAdd_Click(object sender, EventArgs e)
        {
            if (TxtName.Text == "" || Status.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Attendance(AttStId, AttStName, AttDate, AttStatus) values (@StId,@StName,@AttDate,@AttSta)", con);
                    cmd.Parameters.AddWithValue("@StId", StuId.Text);
                    cmd.Parameters.AddWithValue("@StName", TxtName.Text);
                    cmd.Parameters.AddWithValue("@AttDate", DatePicker.Value.Date);
                    cmd.Parameters.AddWithValue("@AttSta", Status.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Thêm Attendance.");
                    con.Close();
                    DisplayAttendance();
                    Reset();

                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

        private void Reset()
        {
            Status.Text = "";
            TxtName.Text = "";
            StuId.Text = "";
        }


        private void BtEdit_Click(object sender, EventArgs e)
        {
            if (TxtName.Text == "" || Status.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Attendance set AttStId=@StId, AttStName=@StName, AttDate=@AttDate, AttStatus=@AttSta where AttNum=@AttNum", con);
                    cmd.Parameters.AddWithValue("@AttNum", Key);
                    cmd.Parameters.AddWithValue("@StId", StuId.Text);
                    cmd.Parameters.AddWithValue("@StName", TxtName.Text);
                    cmd.Parameters.AddWithValue("@AttDate", DatePicker.Value.Date);
                    cmd.Parameters.AddWithValue("@AttSta", Status.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Sửa Attendance.");
                    con.Close();
                    DisplayAttendance();
                    Reset();

                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

        private void BtDel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void BtBack_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StuId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStuName();
        }
        int Key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StuId.SelectedValue = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            TxtName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            DatePicker.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();           
            Status.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            
            
            if (TxtName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
