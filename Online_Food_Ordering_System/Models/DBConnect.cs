using Online_Food_Ordering_System.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models
{
    public class DBConnect : DbContext
    {
        public DBConnect() : base("name=myConnection")
        {
            Database.SetInitializer<DBConnect>(new DropCreateDatabaseIfModelChanges<DBConnect>());
        }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.OrderDetails> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.Branch> Branches { get; set; }

        public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.Entity.FoodOffer> FoodOffers { get; set; }
        //public System.Data.Entity.DbSet<Online_Food_Ordering_System.Models.HomeModel> HomeModels { get; set; }
    }
}