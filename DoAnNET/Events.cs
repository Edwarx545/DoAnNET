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
    public partial class Events : Form
    {
        public Events()
        {
            InitializeComponent();
            DisplayEvent(); 
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True");
        private void DisplayEvent()
        {
            con.Open();
            string Query = "Select * from Event";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void BtAdd_Click(object sender, EventArgs e)
        {
            if (TxtEv.Text == "" || TxtDur.Text == "")
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Event(EvDesc, EvDate, EvDur) values (@Ev,@EDate,@EDur)", con);
                    cmd.Parameters.AddWithValue("@Ev", TxtEv.Text);
                    cmd.Parameters.AddWithValue("@EDate", DateEv.Value.Date);
                    cmd.Parameters.AddWithValue("@EDur", TxtDur.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Thêm sự kiện.");
                    con.Close();
                    DisplayEvent();
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
            TxtEv.Text = "";
            TxtDur.Text = "";
        }

        private void BtEdit_Click(object sender, EventArgs e)
        {
            if (TxtEv.Text == "" || TxtDur.Text == "")
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Event set EvDesc=@Ev, EvDate=@EDate, EvDur=@EDur where EvId = @EvKey", con);
                    cmd.Parameters.AddWithValue("EvKey", Key);
                    cmd.Parameters.AddWithValue("@Ev", TxtEv.Text);
                    cmd.Parameters.AddWithValue("@EDate", DateEv.Value.Date);
                    cmd.Parameters.AddWithValue("@EDur", TxtDur.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Đã Sửa sự kiện.");
                    con.Close();
                    DisplayEvent();
                    Reset();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }
        int Key = 0;
        private void BtDel_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chọn Sự kiện.");
            }
            else
            {
                try
                {                    
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Event where EvId = @EvKey", con);
                    cmd.Parameters.AddWithValue("EvKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa thành công");
                    con.Close();
                    DisplayEvent();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtEv.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            DateEv.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            TxtDur.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();            
            if (TxtEv.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
