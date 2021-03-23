using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.Web.Model
{
    public class OrderModel
    {
        public int OrderNumer { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public string DeliveryExpected { get; set; }
    }

    public class OrderItemModel
    {
        public string Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceEach { get; set; }

    }
}
