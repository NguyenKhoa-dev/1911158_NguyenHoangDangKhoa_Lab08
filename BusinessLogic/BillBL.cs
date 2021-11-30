using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BillBL
    {
        BillDA billDA = new BillDA();
        public List<Bill> GetAll()
        {
            return billDA.GetAll();
        }

        public int CheckExistBillTable(int TableID)
        {
            List<Bill> billList = GetAll();
            foreach (var bill in billList)
            {
                if (bill.TableID == TableID && bill.Status == false)
                    return bill.ID;
            }
            return 0;
        }

        public Bill GetBillBy_ID(int BillID)
        {
            return GetAll().Find(x => x.ID == BillID);
        }

        public int Bill_Insert(Bill bill)
        {
            return billDA.Bill_Insert(bill);
        }

        public int BillUpdateAmount(int BillID, int TableID, string Amount)
        {
            return billDA.BillUpdateAmount(BillID, TableID, Amount);
        }

        public int Bills_Delete(Bill bill)
        {
            return billDA.Bills_Delete(bill);
        }
    }
}
