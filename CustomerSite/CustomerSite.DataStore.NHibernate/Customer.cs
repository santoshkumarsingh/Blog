using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSite.DataStore.NHibernate
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address { get; set; }
        public virtual string Phone { get; set; }
        public virtual decimal CreditLimit { get; set; }
        public virtual DateTime CustomerSince { get; set; }
    }
}
