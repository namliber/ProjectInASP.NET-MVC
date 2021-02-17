using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class DrugContent
    {
        public Idgroup idGroup { get; set; }

        public class Idgroup
        {
            public string idType { get; set; }
            public string id { get; set; }
            public string[] rxnormId { get; set; }

            //public Idgroup(string it, string id, string[] r)
            //{
            //    idType = it;
            //    this.id = id;
            //    rxnormId = r;
            //}

            //public Idgroup(Idgroup other)
            //{
            //    this.idType = other.idType;
            //    this.id = other.id;
            //    this.rxnormId = other.rxnormId;
            //}
        }

        //public DrugContent() { }
        //public DrugContent(Idgroup ig)
        //{
        //    idGroup = ig;
        //}
    }
}
