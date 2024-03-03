namespace Store.Web.Models
{
    public class CartViewModel
    {
        public IDictionary<int, int> Items { get; set; } = new Dictionary<int, int>();

        public decimal Amount { get; set; }
    }
}
