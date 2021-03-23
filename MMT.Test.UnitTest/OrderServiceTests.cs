using System.Linq;
using System.Threading.Tasks;
using MMT.DataAcces.Repository;
using MMT.Domain;
using MMT.Service;
using MMT.Service.Configuration;
using MMT.Web.Configuration;
using MMT.Web.Model;
using NUnit.Framework;

namespace MMT.Test.UnitTest
{
    [TestFixture]
    public class OrderServiceTests
    {
        private DataConfiguration dataconfig;
        private OrderService orderService;

        [SetUp]
        public void Setup( )
        {
            dataconfig = new DataConfiguration() { apiKey = "uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==", connectionString = "Server=tcp:mmt-sse-test.database.windows.net,1433;Initial Catalog=SSE_Test;Persist Security Info=False;User ID=mmt-sse-test;Password=database-user-01;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" };
            orderService = new OrderService(dataconfig, new CustomerService());
        }



        [Test]
        [TestCase("cat.owner@mmtdigital.co.uk", "C34454")]
        [TestCase("santa@north-pole.lp.com", "XM45001")]
        [TestCase("sneeze@fake-customer.com", "A99001")]
        [TestCase("dog.owner@fake-customer.com", "R34788")]
        public async Task TestGiftOrdersReturnProductsAsGifts(string email, string userID)
        {
            var customerDetails = new UserModel() { CustomerId = userID, User = email };
            var order = orderService.GetMostRecentOrder(userID);
            var service = new CustomerService();
            var recentOrder =
                orderService.GetMostRecentOrder(
                    await service.GetCustomerInformationAsync(dataconfig.apiKey, email, userID));
            if (order.Containsgift != null)
            {
                if (order.Containsgift.Value)
                {
                    Assert.AreEqual(recentOrder.Order.OrderItems.FirstOrDefault()?.Product, "Gift");
                }
                else
                {
                    Assert.AreNotEqual(recentOrder.Order.OrderItems.FirstOrDefault()?.Product, "Gift");
                }
            }
        }
    }
}



/*
 cat.owner@mmtdigital.co.uk (ID= C34454)
dog.owner@fake-customer.com (ID= R34788)
sneeze@fake-customer.com (ID= A99001)
santa@north-pole.lp.com (ID= XM45001)

{
    "user": "dog.owner@fake-customer.com",
    "customerId": "R34788"
}

{ 
	"user": "sneeze@fake-customer.com",
	"customerId": "A99001"
}


{ 
	"user": "santa@north-pole.lp.com",
	"customerId": "XM45001"
}


*/