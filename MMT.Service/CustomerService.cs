using Microsoft.AspNetCore.WebUtilities;
using MMT.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MMT.Service.Exceptions;

namespace MMT.Service
{
    public class CustomerService : ICustomerService
    {
        static readonly string baseUri = "https://customer-details.azurewebsites.net/api/GetUserDetails";
        static readonly HttpClient client = new HttpClient();

        public CustomerService()
        {

        }

        public async Task<CustomerDetails> GetCustomerInformationAsync(string apiKey, string email, string customerId)
        {
            CustomerDetails customerDetails;
            var param = new Dictionary<string, string>() { { "code", apiKey }, { "email", email } };
            var newUrl = new Uri(QueryHelpers.AddQueryString(baseUri, param));
            var httpResponse = await client.GetAsync(newUrl);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new RequestException("Problem Getting Customer", httpResponse.StatusCode);
            }
            else
            {
                var response = await httpResponse.Content.ReadAsStringAsync();
                customerDetails = JsonConvert.DeserializeObject<CustomerDetails>(response);
                if (customerDetails.CustomerId != customerId)
                    throw new RequestException("Problem Getting Customer", HttpStatusCode.BadRequest);
            }
            return customerDetails;

        }
    }
}
