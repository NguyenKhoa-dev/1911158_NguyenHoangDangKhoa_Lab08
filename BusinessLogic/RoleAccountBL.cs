using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RoleAccountBL
    {
        RoleAccountDA roleAccountDA = new RoleAccountDA();

        public int RoleAccount_Insert(RoleAccount roleAccount)
        {
            return roleAccountDA.RoleAccount_Insert(roleAccount);
        }
    }
}
