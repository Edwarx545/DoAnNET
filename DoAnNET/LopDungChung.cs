using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnNET
{
    class LopDungChung
    {
        string diachi = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Baitap\Quanlinhanvien\DoAnNET\DoAnNET\SchoolManager.mdf;Integrated Security=True";
        SqlConnection conn;

        public LopDungChung()
        {
            conn = new SqlConnection(diachi);
        }

        public int ThemXoaSua(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            int kq = comm.ExecuteNonQuery();
            //conn.Close();
            return kq;
        }

        public DataTable LoadDL(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;
        }

        public DataTable LoadCB(string sql)
        {
            SqlDataAdapter daCB = new SqlDataAdapter(sql, conn);
            DataTable dtCB = new DataTable();
            daCB.Fill(dtCB);
            return dtCB;

        }
        public object Scalar(string sql)
        {
            SqlCommand com = new SqlCommand(sql, conn);
            conn.Open();
            int ketqua = (int)com.ExecuteScalar();
            conn.Close();
            return ketqua;
        }
    }
}
