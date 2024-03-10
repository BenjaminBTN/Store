namespace Store.Web.Models
{
    public class CartModel
    {
        public int OrderId { get; }

        public int TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public CartModel(int orderId)
        {
            OrderId = orderId;
            TotalCount = 0;
            TotalPrice = 0m;
        }
    }
}
