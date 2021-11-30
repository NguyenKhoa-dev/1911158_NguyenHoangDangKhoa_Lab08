using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccess
{
    public class Ultilities
    {
        private static string StrName = "KetNoiDB";
        public static string ConnectionString = ConfigurationManager.ConnectionStrings[StrName].ConnectionString;

        public static string Food_GetAll = "Food_GetAll";
        public static string Food_InsertUpdateDelete = "Food_InsertUpdateDelete";

        public static string Category_GetAll = "Category_GetAll";
        public static string Category_InsertUpdateDelete = "Category_InsertUpdateDelete";

        public static string Account_GetAll = "Account_GetAll";
        public static string Account_GetRoleID = "GetRoleID";
        public static string Account_Insert = "Account_Insert";
        public static string Account_Update = "Account_Update";

        public static string Table_GetAll = "Table_GetAll";

        public static string Bills_GetAll = "Bills_GetAll";
        public static string Bills_Insert = "Bills_Insert";
        public static string Bill_Update_Amount = "Bills_Update_Amount";
        public static string Bills_Delete = "Bills_Delete";

        public static string BillDetails_GetAll = "BillDetails_GetAll";
        public static string BillDetails_Insert = "BillDetails_Insert";
        public static string BillDetails_Info = "BillDetailsInfo";
        public static string BillDetails_Delete = "BillDetails_Delete";

        public static string Role_GetAll = "Role_GetAll";

        public static string RoleAccount_Insert = "RoleAccount_Insert";
    }
}
