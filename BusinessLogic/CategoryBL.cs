using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CategoryBL
    {
        CategoryDA categoryDA = new CategoryDA();

        public List<Category> GetAll()
        {
            return categoryDA.GetAll();
        }

        public int Insert(Category cat)
        {
            return categoryDA.Insert_Update_Delete(cat, 0);
        }

        public int Update(Category cat)
        {
            return categoryDA.Insert_Update_Delete(cat, 1);
        }

        public int Delete(Category cat)
        {
            return categoryDA.Insert_Update_Delete(cat, 2);
        }
    }
}
