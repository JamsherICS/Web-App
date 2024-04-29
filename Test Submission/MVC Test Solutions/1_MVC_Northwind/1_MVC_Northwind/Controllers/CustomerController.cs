using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _1_MVC_Northwind.Models;

namespace _1_MVC_Northwind.Controllers
{
    public class CustomerController : Controller
    {
         northwindEntities db = new northwindEntities(); // Replace NorthwindEntities with your data context name

        public ActionResult CustomersInGermany()
        {
            var customersInGermany = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(customersInGermany);
        }

        public ActionResult CustomerDetailsWithOrder()
        {
            var customerWithOrder = db.Customers.Where(c => c.Orders.Any(o => o.OrderID == 10248)).ToList();
            return View(customerWithOrder);
        }
    }
}