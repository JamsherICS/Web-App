
using _08_MAY_MVC_DB_First_Test.Models;
using System.Collections.Generic;

namespace _08_MAY_MVC_DB_First_Test.Repository
{
   
        public interface IOrderRepository
        {
            IEnumerable<Order> GetAllOrders();
            Order GetOrderById(int orderId);
            void PlaceOrder(Order order);

            Customer GetCustomerWithHighestOrder();
    }
    
}
