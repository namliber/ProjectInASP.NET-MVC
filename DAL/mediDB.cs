using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class mediDB : DbContext
    {
        public mediDB() : base("medDB")//Name of the DB
        {
        }
        public DbSet<Medicine> Drugs { set; get; }
        public DbSet<LicensedMedicine> LicensedMedicine { set; get; }
        public DbSet<Patient> Patients { set; get; }
        public DbSet<Doctor> Doctors { set; get; }
        public DbSet<Admin> Admin { set; get; }
        public DbSet<Prescription> Prescriptions { set; get; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)//remove the 's' from table name
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
////public class DrugsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<mediDB>
//{
//    protected override void Seed(mediDB context)
//{
//    var Drugs = new List<Medicine>
//            {
//                new Medicine
//                {
//                    CommercialName = "Acamol",
//                    GenericName = "Acamol",
//                    Producer = "Teva",
//                    ActiveIngredients = "blabla",
//                    DoseCharacteristic = "blabla",
//                    ImageUrl = "acamol.jpf"
//                },
//                new Medicine
//                {
//                    CommercialName = "Advil",
//                    GenericName = "Advil",
//                    Producer = "Teva",
//                    ActiveIngredients = "blabla",
//                    DoseCharacteristic = "blabla",
//                    ImageUrl = "advil.jpf"
//                }
//            };
//    Drugs.ForEach(d => context.Drugs.Add(d));
//    context.SaveChanges();
//}
//}
