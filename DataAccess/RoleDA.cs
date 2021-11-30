using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDA
    {
        public List<Role> GetAll()
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Role_GetAll;

            sqlConn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Role> list = new List<Role>();
            while (reader.Read())
            {
                Role role = new Role();
                role.ID = Convert.ToInt32(reader["ID"].ToString());
                role.RoleName = reader["RoleName"].ToString();
                role.Path = reader["Path"].ToString();
                role.Notes = reader["Notes"].ToString();
                list.Add(role);
            }
            sqlConn.Close();

            return list;          
        }
    }
}
