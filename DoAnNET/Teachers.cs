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
    public partial class Teachers : Form
    {
        public Teachers()
        {
            InitializeComponent();
            DisplayTeacher();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True");
        private void DisplayTeacher()
        {
            con.Open();
            string Query = "Select * from Teacher";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            if (TxtAdd.Text == "" || TxtName.Text == "" || TxtPhone.Text == "" || Gen.SelectedIndex == -1 || Subj.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Teacher(TeName, TeGen, TePhone, TeSub, TeAdd, TeDoB) values (@name,@gen,@phone,@sub,@add,@dob)", con);
                    cmd.Parameters.AddWithValue("@name", TxtName.Text);
                    cmd.Parameters.AddWithValue("@gen", Gen.Text);
                    cmd.Parameters.AddWithValue("@phone", TxtPhone.Text);
                    cmd.Parameters.AddWithValue("@sub", Subj.Text);
                    cmd.Parameters.AddWithValue("@add", TxtAdd.Text);
                    cmd.Parameters.AddWithValue("@dob", DoB.Value.Date);                                       
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Thêm giáo viên.");
                    con.Close();
                    DisplayTeacher();
                    Reset();

                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }
        int Key = 0;
        private void Reset()
        {
            Key = 0;
            TxtName.Text = "";
            TxtAdd.Text = "";
            Gen.Text = "";
            Subj.Text = "";
            TxtPhone.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Gen.SelectedItem = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            TxtPhone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Subj.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            TxtAdd.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            DoB.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
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
                MessageBox.Show("Chọn giáo viên.");
            }
            else
            {
                try
                {
                    
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Teacher where TeId = @TeId", con);
                    cmd.Parameters.AddWithValue("TeId", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa thành công");
                    con.Close();
                    DisplayTeacher();
                    Reset();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
        }

        private void BtEdit_Click(object sender, EventArgs e)
        {
            if (TxtAdd.Text == "" || TxtName.Text == "" || TxtPhone.Text == "" || Gen.SelectedIndex == -1 || Subj.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Teacher set TeName=@name, TeGen=@gen, TePhone=@phone, TeSub=@sub, TeAdd=@add, TeDoB=@dob where TeId = @TeId", con);
                    cmd.Parameters.AddWithValue("TeId", Key);
                    cmd.Parameters.AddWithValue("@name", TxtName.Text);
                    cmd.Parameters.AddWithValue("@gen", Gen.Text);
                    cmd.Parameters.AddWithValue("@phone", TxtPhone.Text);
                    cmd.Parameters.AddWithValue("@sub", Subj.Text);
                    cmd.Parameters.AddWithValue("@add", TxtAdd.Text);
                    cmd.Parameters.AddWithValue("@dob", DoB.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Sửa giáo viên.");
                    con.Close();
                    DisplayTeacher();
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
