using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerSite.BusinessLogic;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteCustomer(int customerId) {
            ICustomerProvider provider = new CustomerViewModel();
            provider.DeleteCustomer(customerId);
            return PartialView("CustomerList", provider.GetCustomers());
        }

        public ActionResult CustomerList() {
            ICustomerProvider provider = new CustomerViewModel();
            return PartialView(provider.GetCustomers());
        }

        public ActionResult SaveCustomer([Bind(Exclude="Id")]CustomerViewModel customer) {
            ICustomerProvider provider = new CustomerViewModel();
            bool saved = provider.AddCustomer(customer);
            return Json(new { saved = saved }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateCustomer([Bind]CustomerViewModel customer)
        {
            ICustomerProvider provider = new CustomerViewModel();
            bool saved = provider.UpdateCustomer(customer);
            return Json(new { saved = saved }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomer(int customerId)
        {
            ICustomerProvider provider = new CustomerViewModel();
            return Json(new { customer = provider.GetCustomer(customerId) }, JsonRequestBehavior.AllowGet);
        }
	}
}