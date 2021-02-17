using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class LicensedMedicineLogic
    {
        DAL.LicensedMedicines dal = new DAL.LicensedMedicines();
        public IEnumerable<LicensedMedicine> GetMedicineByNdc(string ndc)
        {
            return dal.GetMedicineByNdc(ndc);
        }
    }
}
