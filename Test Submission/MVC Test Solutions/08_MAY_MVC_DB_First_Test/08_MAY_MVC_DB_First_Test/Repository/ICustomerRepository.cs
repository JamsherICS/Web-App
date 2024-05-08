using _08_MAY_MVC_DB_First_Test.Models;

namespace _08_MAY_MVC_DB_First_Test.Repository
{
    
        public interface ICustomerRepository
        {
            IEnumerable<Customer> GetAllCustomers();
            Customer GetCustomerById(string customerId);
            IEnumerable<Customer> GetCustomersByOrderDate(DateTime orderDate);
    }
    
}
