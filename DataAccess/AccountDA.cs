using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDA
    {
        public List<Account> GetAll()
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Account_GetAll;

            sqlConn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Account> list = new List<Account>();
            while (reader.Read())
            {
                Account acc = new Account();
                acc.AccountName = reader["AccountName"].ToString();
                acc.Password = reader["Password"].ToString();
                acc.FullName = reader["FullName"].ToString();
                acc.Email = reader["Email"].ToString();
                acc.Tell = reader["Tell"].ToString();
                if (string.IsNullOrWhiteSpace(reader["DateCreated"].ToString()))
                    acc.DateCreate = DateTime.Now;
                else
                    acc.DateCreate = DateTime.Parse(reader["DateCreated"].ToString().Split(' ')[0].Replace('-', '/'));
                list.Add(acc);
            }
            sqlConn.Close();
            return list;
        }

        public bool GetRoleID(string AccountName)
        {
            bool kq = true;
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Account_GetRoleID;

            cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar);
            cmd.Parameters["@AccountName"].Value = AccountName;

            sqlConn.Open();
            var roleID = cmd.ExecuteScalar();
            if (roleID == null)
                kq = false;
            sqlConn.Close();
            return kq;
        }

        public int Account_Insert(Account account)
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Account_Insert;

            cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar).Value = account.AccountName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = account.Password;
            cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = account.FullName;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = account.Email;
            cmd.Parameters.Add("@Tell", SqlDbType.NVarChar).Value = account.Tell;
            cmd.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime).Value = account.DateCreate;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                return result;
            return 0;
        }

        public int Account_Update(Account account)
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Account_Update;

            cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar).Value = account.AccountName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = account.Password;
            cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = account.FullName;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = account.Email;
            cmd.Parameters.Add("@Tell", SqlDbType.NVarChar).Value = account.Tell;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                return result;
            return 0; 
        }
    }
}
