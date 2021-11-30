using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class AccountBL
    {
        AccountDA accountDA = new AccountDA();

        public List<Account> GetAll()
        {
            return accountDA.GetAll();
        }

        public bool CheckRoleID(string AccountName)
        {
            return accountDA.GetRoleID(AccountName);
        }

        public bool CheckPassword(string AccoutName, string Password)
        {
            List<Account> ListAcc = GetAll();
            foreach (var acc in ListAcc)
            {
                if (acc.AccountName == AccoutName && acc.Password == Password)
                    return true;
            }
            return false;
        }

        public Account GetAccountByAccountName(string AccountName)
        {
            List<Account> ListAcc = GetAll();
            foreach (var item in ListAcc)
            {
                if (item.AccountName == AccountName)
                    return item;
            }
            return null;
        }

        public int Account_Insert(Account account)
        {
            return accountDA.Account_Insert(account);
        }

        public int Account_Update(Account account)
        {
            return accountDA.Account_Update(account);
        }
    }
}
