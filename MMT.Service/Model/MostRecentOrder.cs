namespace MMT.Web.Model
{
    public class MostRecentOrder
    {
        public Customer Customer { get; set; }
        public OrderModel Order { get; set; }
        public MostRecentOrder(Customer customer, OrderModel order)
        {
            Customer = customer;
            Order = order;
        }

        public MostRecentOrder()
        {
        }
    }

    


}