using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;

namespace CustomerSite.DataStore.EF
{
    public class CustomerContext : DbContext
    {
        public CustomerContext() : base(ConfigurationManager.ConnectionStrings["DBCustomerEntities"].ConnectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            MapCustomers(modelBuilder);
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<CustomerContext>(null);
            
        }

        public DbSet<Customer> Customers { get; set; }

        internal void MapCustomers(DbModelBuilder binder) {
            binder.Entity<Customer>().ToTable("dbo.Customers").HasKey(k => k.Id);
        }
    }
}
