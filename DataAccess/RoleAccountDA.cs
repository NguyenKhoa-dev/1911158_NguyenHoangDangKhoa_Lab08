using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleAccountDA
    {
        public int RoleAccount_Insert(RoleAccount roleAccount)
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.RoleAccount_Insert;

            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleAccount.RoleID;
            cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar).Value = roleAccount.AccountName;
            cmd.Parameters.Add("@Actived", SqlDbType.Bit).Value = roleAccount.Actived == true ? 1 : 0;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = roleAccount.Notes;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                return result;
            return 0;
        }
    }
}
