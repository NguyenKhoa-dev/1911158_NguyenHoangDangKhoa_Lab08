using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BillDetailsDA
    {
        public List<BillDetails> GetAll()
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.BillDetails_GetAll;

            sqlConn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<BillDetails> list = new List<BillDetails>();
            while (reader.Read())
            {
                BillDetails bdetail = new BillDetails();
                bdetail.ID = Convert.ToInt32(reader["ID"].ToString());
                bdetail.BillID = Convert.ToInt32(reader["InvoiceID"].ToString());
                bdetail.FoodID = Convert.ToInt32(reader["FoodID"].ToString());
                bdetail.Quantity = Convert.ToInt32(reader["Quantity"].ToString());                
                list.Add(bdetail);
            }
            sqlConn.Close();
            return list;
        }

        public int InsertBillDetail(BillDetails billDetail)
        {
            int kq = 0;
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.BillDetails_Insert;

            cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = billDetail.BillID;
            cmd.Parameters.Add("@FoodID", SqlDbType.Int).Value = billDetail.FoodID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = billDetail.Quantity;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                kq = (int)cmd.Parameters["@ID"].Value;
            sqlConn.Close();

            return kq;
        }

        public DataTable ViewBillDetails(int BillID)
        {
            DataTable billInfo = new DataTable();
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.BillDetails_Info;

            cmd.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            sqlConn.Open();
            adapter.Fill(billInfo);
            sqlConn.Close();

            return billInfo;
        }

        public int Delete_BillDetails(BillDetails billDetails)
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.BillDetails_Delete;

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = billDetails.ID;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();            
            sqlConn.Close();
            return result;
        }
    }
}
