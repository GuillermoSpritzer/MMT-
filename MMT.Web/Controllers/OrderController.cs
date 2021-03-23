using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMT.DataAcces.Repository;
using MMT.Service;
using MMT.Service.Configuration;
using MMT.Service.Exceptions;
using MMT.Web.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MMT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IDataConfiguration _dataConfiguration;

        public OrderController(ICustomerService customerService, IOrderService orderService, IDataConfiguration dataConfiguration)
        {
            _customerService = customerService;
            _orderService = orderService;
            _dataConfiguration = dataConfiguration;
        }

        // POST api/<OrderController>
        [HttpPost("GetMostRecentOrder")]
        public async Task<ActionResult> GetMostRecentOrder([FromBody] UserModel userModel)
        {
            try
            {
                var customerDetails = await _customerService.GetCustomerInformationAsync(_dataConfiguration.apiKey, userModel.User , userModel.CustomerId);
                var mostRecentOrder = _orderService.GetMostRecentOrder(customerDetails);
                return Ok(mostRecentOrder);
            }
            catch (RequestException re)
            {
                if (re.statusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(re.Message);
                }
                else if (re.statusCode == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(re.Message);
                }
                else
                {
                    return BadRequest(re.Message);
                }
            }

        }

    }
}
