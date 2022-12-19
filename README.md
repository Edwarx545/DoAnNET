# DoAnNET 

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
    public partial class KhachHang : Form
    { 
        lopdungchung ldc;
        public KhachHang()
        {
            InitializeComponent();

            ldc = new lopdungchung();
        } 
        private void KhachHang_Load(object sender, EventArgs e)
        {
            DisplayKH();
        }

        private void DisplayKH()
        {
            string sqlLoadGrid = "select * from KhachHang";
            dataGridView1.DataSource = ldc.LoadData(sqlLoadGrid);
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            if (TxtAdd.Text == "" || TxtName.Text == "" || TxtPhone.Text == "" || Gen.SelectedIndex == -1 || Sub.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {
                    
                    string sqlThem = "insert into KhachHang values(N'"+TxtName.Text +"','"+ Gen.SelectedValue + "','"+TxtPhone.Text +"',' " + Sub.SelectedValue + " ', '"+ TxtAdd.Text+ "',Convert(DateTime,'" + DoB.Value + "',103))";
                    ldc.ThemXoaSua(sqlThem);
                    MessageBox.Show(" Đã Thêm khách hàng.");
                    DisplayKH();

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
            Sub.Text = "";
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
            Sub.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
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
            
            DialogResult dial = MessageBox.Show("Bạn thật sự muốn Xóa!", "Thông báo", MessageBoxButtons.YesNo);
            if (dial == DialogResult.Yes)
            {                              
                string sqlXoa = "delete KhachHang where KhId = '" + TxtId.Text + "'";
                ldc.ThemXoaSua(sqlXoa);
                DisplayKH();
            }
        }

        private void BtEdit_Click(object sender, EventArgs e)
        {
            if (TxtAdd.Text == "" || TxtName.Text == "" || TxtPhone.Text == "" || Gen.SelectedIndex == -1 || Sub.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin");
            }
            else
            {
                try
                {

                    string sqlSua = "update KhachHang set KhName = '" + TxtName.Text +"', KhGen = '"+ Gen.SelectedValue + "', KhPhone = '"+ TxtPhone.Text+"', KhSub = '"+Sub.SelectedValue + "' , KhAdd= '"+TxtAdd.Text +"', KhDob = Convert(DateTime,'" + DoB.Value + "',103)" ;
                    ldc.ThemXoaSua(sqlSua);
                    DisplayKH();

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




Duoi day la code Lop chung


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnNET
{
    internal class lopdungchung
    {
        string chuoikn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\download\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True";
        SqlConnection conn;

        public lopdungchung()
        {
            conn = new SqlConnection(chuoikn);
        }

        public int ThemXoaSua(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            int kq = comm.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public DataTable LoadDL(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;
        }


        public DataTable LoadData(string sqlData)
        {
            SqlDataAdapter da = new SqlDataAdapter(sqlData, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
