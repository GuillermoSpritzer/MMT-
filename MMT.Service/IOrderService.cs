using MMT.Domain;
using MMT.Web.Model;

namespace MMT.Service
{
    public interface IOrderService
    {
        MostRecentOrder GetMostRecentOrder(CustomerDetails customerDetails);

    }
}