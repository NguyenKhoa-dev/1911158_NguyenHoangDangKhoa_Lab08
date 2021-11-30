using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BillDetailsBL
    {
        BillDetailsDA billDetailDA = new BillDetailsDA();

        public List<BillDetails> GetAll()
        {
            return billDetailDA.GetAll();
        }
        
        public int BillDetails_Insert(BillDetails billDetail)
        {
            return billDetailDA.InsertBillDetail(billDetail);
        }

        public DataTable BillDetailsInfo(int BillID)
        {
            return billDetailDA.ViewBillDetails(BillID);
        }

        public BillDetails GetBillDetailBy_BillID_FoodID(int billID, int foodID)
        {
            List<BillDetails> bds = GetAll();
            BillDetails billDetails = bds.Find(x => x.BillID == billID && x.FoodID == foodID);
            return billDetails;
        }

        public int BillDetails_Delete(BillDetails billDetails)
        {
            return billDetailDA.Delete_BillDetails(billDetails);
        }
    }
}
