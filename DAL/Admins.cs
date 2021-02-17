using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class Admins
    {
        public void InsertAdmin(Admin a)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    ctx.Admin.Add(a);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public IEnumerable<Admin> GetAdmin()
        {
            try
            {
                List<Admin> result = new List<Admin>();
                using (var ctx = new mediDB())
                {
                    foreach (var admin in ctx.Admin)
                    {
                        result.Add(admin);
                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public Admin returnAdmin(User user)
        {
            IEnumerable<Admin> result = GetAdmin();
            Admin a = result.FirstOrDefault(ad => ad.UserName == user.UserName && ad.Password == user.Password);
            return a;
        }
    }
}