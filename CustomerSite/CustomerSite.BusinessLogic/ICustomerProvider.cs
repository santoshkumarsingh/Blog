using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSite.BusinessLogic
{
    public interface ICustomerProvider
    {
        List<CustomerViewModel> GetCustomers();
        CustomerViewModel GetCustomer(int customerId);
        bool AddCustomer(CustomerViewModel customer);
        void DeleteCustomer(int customerId);
        bool UpdateCustomer(CustomerViewModel customer);
    }
}
