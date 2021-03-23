using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMT.DataAcces.Repository;
using MMT.Domain;
using MMT.Service.Configuration;
using MMT.Web.Model;

namespace MMT.Service
{
    public class OrderService : IOrderService
    {
        private readonly IDataConfiguration _dataConfiguration;
        private readonly ICustomerService _customerService;

        public OrderService(IDataConfiguration dataConfiguration, ICustomerService customerService)
        {
            _dataConfiguration = dataConfiguration;
            _customerService = customerService;
        }

        public Order GetMostRecentOrder(string customerId)
        {
            using (var context = new SSE_TestContext(_dataConfiguration.connectionString))
            {
                var orders = context.Orders.Include(p => p.Orderitems).ThenInclude(o => o.Product).Where(r => r.Customerid == customerId).OrderByDescending(o => o.Orderdate).ToList();
                return orders.FirstOrDefault();
            }
        }
        public MostRecentOrder GetMostRecentOrder(CustomerDetails customerDetails)
        {
            var order = GetMostRecentOrder(customerDetails.CustomerId);

            MostRecentOrder returnOrder = new MostRecentOrder();
            List<OrderItemModel> orderItems = new List<OrderItemModel>();

            string containsGift = null;

            returnOrder.Customer = new Customer
            {
                Name = customerDetails.FirstName,
                LastName = customerDetails.lastName
            };
            if (order != null)
            {
                if (order.Containsgift != null && order.Containsgift.Value)
                {
                    containsGift = "Gift";
                }

                foreach (var item in order.Orderitems)
                {
                    var product = new OrderItemModel
                    {
                        Product = containsGift ?? item.Product.Productname,
                        Quantity = item.Quantity,
                        PriceEach = item.Price
                    };
                    orderItems.Add(product);
                }

                returnOrder.Order = new OrderModel()
                {
                    OrderNumer = order.Orderid,
                    OrderDate = order.Orderdate?.ToString("dd-MMM-yyyy"),
                    DeliveryAddress = customerDetails.HouseNumber + " " + customerDetails.Street + ", " +
                                      customerDetails.Town + ", " + customerDetails.Postcode,
                    OrderItems = orderItems,
                    DeliveryExpected = order.Deliveryexpected?.ToString("dd-MMM-yyyy")
                };
            }

            return returnOrder;
        }
    }
}