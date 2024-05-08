using Microsoft.AspNetCore.Mvc;
using _08_MAY_MVC_DB_First_Test.Models;
using _08_MAY_MVC_DB_First_Test.Repository;
using System;

namespace _08_MAY_MVC_DB_First_Test.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult CustomerWithHighestOrder()
        {
            // Call the repository method to get the customer with the highest total order amount
            Customer customerWithHighestOrder = _orderRepository.GetCustomerWithHighestOrder();

            return View(customerWithHighestOrder); // Pass the customer object to the view for display
        }
        public IActionResult CustomerDetailsByOrderDate(DateTime orderDate)
        {
            // Call the repository method to fetch customer details by order date
            IEnumerable<Customer> customers = _customerRepository.GetCustomersByOrderDate(orderDate);

            return View(customers); // Pass the customer list to the view for display
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {

            return View();
        }


        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.PlaceOrder(order);
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }


        public IActionResult DisplayBill(int orderId)
        {
            // Retrieve order details by order ID
            var order = _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound(); // Handle the case where order with the specified ID is not found
            }

            // Fetch customer details
            order.Customer = _customerRepository.GetCustomerById(order.CustomerId);

            // Calculate the total bill amount (if needed)

            return View(order); // Pass the order object to the view for display
        }


       
    }
}
