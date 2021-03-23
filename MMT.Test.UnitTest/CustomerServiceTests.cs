using System;
using System.Net;
using System.Threading.Tasks;
using MMT.DataAcces.Repository;
using MMT.Domain;
using MMT.Service;
using MMT.Service.Configuration;
using MMT.Service.Exceptions;
using MMT.Web.Configuration;
using NUnit.Framework;

namespace MMT.Test.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        [TestCase("uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==", "cat.asdasd@badEmail.co.uk", "C34454", HttpStatusCode.NotFound)]
        [TestCase("badApiKey", "cat.owner@mmtdigital.co.uk", "C34454", HttpStatusCode.Unauthorized)]
        [TestCase("uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==", "cat.owner@mmtdigital.co.uk", "badcode", HttpStatusCode.BadRequest)]

        public async Task TestErrorStatusCodes(string apiKey, string email, string customerId , HttpStatusCode expectedStatusCode)
        {
            var service = new CustomerService();
            try
            {
                var ret = await service.GetCustomerInformationAsync(apiKey, email,customerId);
            }
            catch (RequestException re)
            {
                Assert.AreEqual(re.statusCode, expectedStatusCode);
            }
        }


        [Test]
        [TestCase("uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==", "cat.owner@mmtdigital.co.uk", "C34454")]
        [TestCase("uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==", "dog.owner@fake-customer.com", "R34788")]
        [TestCase("uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==", "sneeze@fake-customer.com", "A99001")]
        [TestCase("uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==", "santa@north-pole.lp.com", "XM45001")]
        public async Task TestValidEmailReturnsCustomerDetails(string apiKey, string email, string customerId)
        {
            var service = new CustomerService();
            var ret = await service.GetCustomerInformationAsync(apiKey, email , customerId);
            Assert.AreEqual(ret.GetType(), typeof(CustomerDetails));
        }


    }
}


