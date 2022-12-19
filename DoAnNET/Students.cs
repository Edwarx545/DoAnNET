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


namespace DoAnNET
{
    public partial class Students : Form
    {
        LopDungChung lopchung;
        public Students()
        {
            InitializeComponent();
            lopchung = new LopDungChung();
            DisplayStudent();

       
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Baitap\Quanlinhanvien\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True");
        private void DisplayStudent()
        {
            con.Open();
            string Query = "Select * from Student";
            SqlDataAdapter sda = new SqlDataAdapter(Query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void BtAdd_Click(object sender, EventArgs e)
        {
            if(TxtAdd.Text == "" || TxtName.Text == "" || Gen.SelectedIndex == -1 || Class.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Student(StName, StGen, StDoB, StClass, StAdd) values (@name,@gen,@dob,@class,@add)", con);
                    cmd.Parameters.AddWithValue("@name", TxtName.Text);
                    cmd.Parameters.AddWithValue("@gen", Gen.Text);
                    cmd.Parameters.AddWithValue("@dob", DoB.Value.Date);
                    cmd.Parameters.AddWithValue("@class", Class.Text);
                    cmd.Parameters.AddWithValue("@add", TxtAdd.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Thêm học sinh.");
                    con.Close();
                    DisplayStudent();
                    Reset();

                }
                catch(Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
                
            }
        }
        private void Reset()
        {
            Key = 0;
            TxtName.Text = "";
            TxtAdd.Text = "";
            Gen.Text = "";
            Class.Text = "";
        }
        int Key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Gen.SelectedItem = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            DoB.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Class.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            TxtAdd.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            if (TxtName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        
        private void BtDel_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chọn học sinh.");
            }
            else
            {
                try
                {
                    SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Baitap\Quanlinhanvien\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True");
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("delete from Student where StId = @StId", connect);
                    cmd.Parameters.AddWithValue("StId", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa thành công");
                    connect.Close();
                    DisplayStudent();
                    Reset();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtEdit_Click(object sender, EventArgs e)
        {
            if (TxtAdd.Text == "" || TxtName.Text == "" || Gen.SelectedIndex == -1 || Class.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Student set StName=@name, StGen=@gen, StDoB=@dob, StClass=@class, StAdd=@add where StId = @StId", con);
                    cmd.Parameters.AddWithValue("StId", Key);
                    cmd.Parameters.AddWithValue("@name", TxtName.Text);
                    cmd.Parameters.AddWithValue("@gen", Gen.Text);
                    cmd.Parameters.AddWithValue("@dob", DoB.Value.Date);
                    cmd.Parameters.AddWithValue("@class", Class.Text);
                    cmd.Parameters.AddWithValue("@add", TxtAdd.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Sửa học sinh.");
                    con.Close();
                    DisplayStudent();
                    Reset();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

        private void BtBack_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }
    }
}
