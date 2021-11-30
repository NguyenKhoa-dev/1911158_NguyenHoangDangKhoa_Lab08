using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccess
{
    public class CategoryDA
    {
        public List<Category> GetAll()
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Category_GetAll;

            sqlConn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Category> list = new List<Category>();
            while (reader.Read())
            {
                Category cat = new Category();
                cat.ID = Convert.ToInt32(reader["ID"]);
                cat.Name = reader["Name"].ToString();
                cat.Type = Convert.ToInt32(reader["Type"]);
                list.Add(cat);
            }
            sqlConn.Close();
            return list;
        }

        public int Insert_Update_Delete(Category cat, int action)
        {
            SqlConnection sqlConn = new SqlConnection(Ultilities.ConnectionString);

            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Category_InsertUpdateDelete;

            SqlParameter IDPara = new SqlParameter("@ID", SqlDbType.Int);
            IDPara.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(IDPara).Value = cat.ID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = cat.Name;            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = cat.Type;
            cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

            sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                return (int)cmd.Parameters["@ID"].Value;
            return 0;
        }
    }
}
