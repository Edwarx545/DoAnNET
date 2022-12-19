using System.Data;
using System.Data.SqlClient;

namespace DoAnNET
{
    public partial class Form1 : Form
    {
        LopDungChung lopchung;
        public Form1()
        {
            InitializeComponent();
            lopchung = new LopDungChung();
       
        }
        int number = 0;
        
        private void label5_Click(object sender, EventArgs e)
        {
            
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int dem = 0;
        private void BtLogin_Click(object sender, EventArgs e)
        {
           
                string sqlDangNhap = "Select count (*) from TAIKHOAN Where TenDangNhap = '" +
                TxtUser.Text.Trim() + "' and MatKhau = '" + TxtPass.Text.Trim() + "' ";
                int ketqua = (int)lopchung.Scalar(sqlDangNhap);
                if (ketqua >= 1)
                {
                    MainMenu SV = new MainMenu();
                    this.Hide();
                    SV.Show();
                }
                else
                {
                    dem++;
                    MessageBox.Show("Nhập sai tài khoản or mật khẩu!!!");
                    if (dem == 3)
                    {

                        MessageBox.Show("Bạn đã nhập sai quá 3 lần");
                        BtLogin.Enabled = false;
                        Application.ExitThread();
                    }
                }
            }
           
        }
   }
