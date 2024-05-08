using _08_MAY_MVC_DB_First_Test.Models;
using System.Linq;

namespace _08_MAY_MVC_DB_First_Test.Repository
{
    
        public class CustomerRepository : ICustomerRepository
        {
            private readonly NorthwindContext _context;

            public CustomerRepository(NorthwindContext context)
            {
                _context = context;
            }

            public IEnumerable<Customer> GetAllCustomers()
            {
                return _context.Customers.ToList();
            }

            public Customer GetCustomerById(string customerId)
            {
                return _context.Customers.Find(customerId);
            }

        public IEnumerable<Customer> GetCustomersByOrderDate(DateTime orderDate)
        {
            // Query to fetch customers based on order date
            var customers = _context.Orders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == orderDate.Date)
                .Select(o => o.Customer)
                .Distinct() // Ensure unique customers
                .ToList();

            return customers;
        }
}
    
}
