using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class TableDA
    {
        public List<Table> GetAll()
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Table_GetAll;

            sqlConn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Table> list = new List<Table>();
            while (reader.Read())
            {
                Table table = new Table();
                table.ID = Convert.ToInt32(reader["ID"]);
                table.Name = reader["Name"].ToString();
                table.Status = Convert.ToInt32(reader["Status"]) == 0 ? false : true;
                table.Capacity = Convert.ToInt32(reader["Capacity"]);
                list.Add(table);
            }
            sqlConn.Close();
            return list;
        }
    }
}
