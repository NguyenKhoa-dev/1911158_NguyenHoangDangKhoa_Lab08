using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BillDA
    {
        public List<Bill> GetAll()
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Bills_GetAll;

            sqlConn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Bill> list = new List<Bill>();
            while (reader.Read())
            {
                Bill bill = new Bill();
                bill.ID = Convert.ToInt32(reader["ID"].ToString());
                bill.Name = reader["Name"].ToString();
                bill.TableID = Convert.ToInt32(reader["TableID"].ToString());
                bill.Amount = Convert.ToInt32(reader["Amount"].ToString());
                bill.Discount = Double.Parse(reader["Discount"].ToString());
                bill.Tax = Double.Parse(reader["Tax"].ToString());
                bill.Status = (reader["Status"].ToString() == "True") ? true : false;
                if (string.IsNullOrWhiteSpace(reader["CheckoutDate"].ToString()))
                    bill.CheckoutDate = new DateTime();
                else
                    bill.CheckoutDate = DateTime.Parse(reader["CheckoutDate"].ToString());
                list.Add(bill);
            }
            sqlConn.Close();
            return list;
        }

        public int Bill_Insert(Bill bill)
        {
            int kq = 0;
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Bills_Insert;

            cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = bill.Name;
            cmd.Parameters.Add("@TableID", SqlDbType.Int).Value = bill.TableID;
            cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = bill.Amount;
            cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = bill.Discount;
            cmd.Parameters.Add("@Tax", SqlDbType.Float).Value = bill.Tax;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = (bill.Status == false) ? 0 : 1;
            cmd.Parameters.Add("@CheckoutDate", SqlDbType.SmallDateTime).Value = bill.CheckoutDate;
            cmd.Parameters.Add("@Account", SqlDbType.NVarChar).Value = bill.AccountName;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                kq = (int)cmd.Parameters["@ID"].Value;
            sqlConn.Close();

            return kq;
        }

        public int BillUpdateAmount(int BillID, int TableID, string Amount)
        {
            int kq = 0;
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Bill_Update_Amount;

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = BillID;
            cmd.Parameters.Add("@TableID", SqlDbType.Int).Value = TableID;
            cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = Amount;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                kq = result;
            sqlConn.Close();
            return kq;
        }

        public int Bills_Delete(Bill bill)
        {
            int kq = 0;
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Bills_Delete;

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = bill.ID;
            cmd.Parameters.Add("@TableID", SqlDbType.Int).Value = bill.TableID;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                kq = result;
            sqlConn.Close();
            return kq;
        }
    }
}
