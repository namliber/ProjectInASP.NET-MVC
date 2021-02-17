using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AdminsLogic
    {

        DAL.Admins dal = new DAL.Admins();
        public void InsertDoctors(Admin a)
        {
            dal.InsertAdmin(a);
        }
        public IEnumerable<Admin> GetAdmin()
        {
            return dal.GetAdmin();
        }
        public Admin returnAdmin(User user)
        {
            return dal.returnAdmin(user);
        }
    }
}
