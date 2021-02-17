using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LicensedMedicines
    {
        public IEnumerable<LicensedMedicine> GetMedicineByNdc(string ndc)
        {
            try
            {
                List<LicensedMedicine> result = new List<LicensedMedicine>();
                using (var ctx = new mediDB())
                {
                    foreach (var LMedicine in ctx.LicensedMedicine)
                    {
                        if (LMedicine.NDC == ndc)
                            result.Add(LMedicine);

                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
    }
}
