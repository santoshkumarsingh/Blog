using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;


namespace CustomerSite.DataStore.NHibernate
{
    public class CustomerContext
    {
        private static ISessionFactory sessionFactory;
        private static ISession session;
        private static ISessionFactory SessionFactory {
            get {
                if (sessionFactory == null)
                    SetSessionFactory();

                return sessionFactory;
            }
        }
        private static void SetSessionFactory() {
            sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"data source=(LocalDB)\v11.0;attachdbfilename=|DataDirectory|Customers.mdf;integrated security=true;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Customer>())
                .ExposeConfiguration(c => new SchemaExport(c))
                .BuildSessionFactory();
        }

        public static ISession OpenSession() {
            return SessionFactory.OpenSession();
        }
    }

    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(c => c.Id);
            Map(c => c.FirstName);
            Map(c => c.LastName);
            Map(c => c.Address);
            Map(c => c.Phone);
            Map(c => c.CreditLimit);
            Map(c => c.CustomerSince);
            Table("dbo.Customers");
        }
    }
}
