using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
//using CustomerSite.DataStore;
using CustomerSite.DataStore.NHibernate;
using NHibernate.Linq;
using System.Diagnostics;



namespace CustomerSite.BusinessLogic
{
    public class CustomerViewModel : ICustomerProvider
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime CustomerSince { get; set; }

        #region EF
        //public void DeleteCustomer(int customerId) {
        //    using (var context = new CustomerContext())
        //    {
        //        context.Customers.Remove(context.Customers.Where(c => c.Id == customerId).First());
        //        context.SaveChanges();
        //    }
        //}

        //public CustomerViewModel GetCustomer(int customerId)
        //{
        //    using (var context = new CustomerContext())
        //    {
        //        return context.Customers.Where(c => c.Id == customerId).Select(c => new CustomerViewModel
        //        {
        //            Id = c.Id,
        //            FirstName = c.FirstName,
        //            LastName = c.LastName,
        //            Address = c.Address,
        //            Phone = c.Phone,
        //            CreditLimit = c.CreditLimit,
        //            CustomerSince = c.CustomerSince
        //        }).First();
        //    }
        //}
        //public List<CustomerViewModel> GetCustomers()
        //{
        //    using (var context = new CustomerContext())
        //    {
        //        return context.Customers.Select(c => new CustomerViewModel()
        //        {
        //            Id = c.Id,
        //            FirstName = c.FirstName,
        //            LastName = c.LastName,
        //            Address = c.Address,
        //            Phone = c.Phone,
        //            CreditLimit = c.CreditLimit,
        //            CustomerSince = c.CustomerSince
        //        }).ToList();
        //    }
        //}
        //public bool AddCustomer(CustomerViewModel customer) {
        //    bool saved = false;
        //    try
        //    {
        //        using (var context = new CustomerContext())
        //        {
        //            context.Customers.Add(new Customer()
        //            {
        //                FirstName = customer.FirstName,
        //                LastName = customer.LastName,
        //                Address = customer.Address,
        //                Phone = customer.Phone,
        //                CreditLimit = customer.CreditLimit,
        //                CustomerSince = customer.CustomerSince
        //            });
        //            context.SaveChanges();
        //        }
        //        saved = true;
        //    }
        //    catch (Exception ex) {
        //        throw;
        //        //Log here
        //    }

        //    return saved;
        //}
        //public bool UpdateCustomer(CustomerViewModel customer)
        //{
        //    bool saved = false;
        //    try
        //    {
        //        using (var context = new CustomerContext())
        //        {
        //            var existingCustomer = context.Customers.Where(c => c.Id == customer.Id).First();
        //            existingCustomer.FirstName = customer.FirstName;
        //            existingCustomer.LastName = customer.LastName;
        //            existingCustomer.Address = customer.Address;
        //            existingCustomer.Phone = customer.Phone;
        //            existingCustomer.CreditLimit = customer.CreditLimit;
        //            existingCustomer.CustomerSince = customer.CustomerSince;
        //            context.SaveChanges();
        //        }
        //        saved = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //        //Log here
        //    }

        //    return saved;
        //}
        #endregion

        #region NHibernate
        public List<CustomerViewModel> GetCustomers()
        {
            using (var context = CustomerContext.OpenSession())
            {
                return context.Query<Customer>().Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    Phone = c.Phone,
                    CreditLimit = c.CreditLimit,
                    CustomerSince = c.CustomerSince
                }).ToList();
            }
        }

        public bool AddCustomer(CustomerViewModel customer)
        {
            bool saved = false;
            try
            {
                using (var context = CustomerContext.OpenSession())
                {
                    using (var transaction = context.BeginTransaction())
                    {
                        context.Save(new Customer()
                        {
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Address = customer.Address,
                            Phone = customer.Phone,
                            CreditLimit = customer.CreditLimit,
                            CustomerSince = customer.CustomerSince
                        });
                        transaction.Commit();
                    }
                }
                saved = true;
            }
            catch (Exception ex)
            {
                //Log here
                throw ex;   
            }
            return saved;
        }
        public CustomerViewModel GetCustomer(int customerId)
        {
            using (var context = CustomerContext.OpenSession())
            {
                return context.Query<Customer>().Where(c => c.Id == customerId).Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    Phone = c.Phone,
                    CreditLimit = c.CreditLimit,
                    CustomerSince = c.CustomerSince
                }).FirstOrDefault();
            }
        }

        public bool UpdateCustomer(CustomerViewModel customer)
        {
            bool saved = false;
            try
            {
                using (var context = CustomerContext.OpenSession())
                {
                    using (var transaction = context.BeginTransaction())
                    {
                        var existingCustomer = context.Query<Customer>().Where(c => c.Id == customer.Id).FirstOrDefault();
                        existingCustomer.FirstName = customer.FirstName;
                        existingCustomer.LastName = customer.LastName;
                        existingCustomer.Address = customer.Address;
                        existingCustomer.Phone = customer.Phone;
                        existingCustomer.CreditLimit = customer.CreditLimit;
                        existingCustomer.CustomerSince = customer.CustomerSince;
                        context.SaveOrUpdate(existingCustomer);
                        transaction.Commit();
                    }
                }
                saved = true;
            }
            catch (Exception ex)
            {
                //Log here
                throw ex;
            }
            return saved;
        }

        public void DeleteCustomer(int customerId)
        {
            using (var context = CustomerContext.OpenSession())
            {
                using (var transaction = context.BeginTransaction())
                {
                    var existingCustomer = context.Query<Customer>().Where(c => c.Id == customerId).FirstOrDefault();
                    context.Delete(existingCustomer);
                    transaction.Commit();
                }
            }
        }
        #endregion

    }
}
