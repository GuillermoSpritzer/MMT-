using System.Threading.Tasks;
using MMT.Domain;

namespace MMT.Service
{
    public interface ICustomerService
    {
        Task<CustomerDetails> GetCustomerInformationAsync(string ApiKey, string email, string customerId);

    }
}