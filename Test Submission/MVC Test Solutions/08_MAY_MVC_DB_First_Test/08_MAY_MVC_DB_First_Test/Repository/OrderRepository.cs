using _08_MAY_MVC_DB_First_Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace _08_MAY_MVC_DB_First_Test.Repository
{
   
        public class OrderRepository : IOrderRepository
        {
            private readonly NorthwindContext _context;

            public OrderRepository(NorthwindContext context)
            {
                _context = context;
            }

            public IEnumerable<Order> GetAllOrders()
            {
                return _context.Orders.ToList();
            }

            public Order GetOrderById(int orderId)
            {
                return _context.Orders.Find(orderId);
            }

            public void PlaceOrder(Order order)
            {
                // Add new order to the context and save changes
                _context.Orders.Add(order);
                _context.SaveChanges();
            }


        public Customer GetCustomerWithHighestOrder()
        {
            var customerWithHighestOrder = _context.Orders
                .AsEnumerable() // Switch to client evaluation
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalOrderAmount = g.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity))
                })
                .OrderByDescending(x => x.TotalOrderAmount)
                .FirstOrDefault();

            if (customerWithHighestOrder != null)
            {
                return _context.Customers.Find(customerWithHighestOrder.CustomerId);
            }

            return null; // Return null if no customer found
        }



    }
}
