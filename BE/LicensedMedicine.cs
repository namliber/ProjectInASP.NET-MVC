using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class LicensedMedicine
    {
        public int Id { get; set; }
        public string NDC { get; set; }

        public LicensedMedicine() { }
        public LicensedMedicine(string ndc)
        {
            NDC = ndc;
        }
        public LicensedMedicine(LicensedMedicine other)
        {
            NDC = other.NDC;
        }
    }
}
