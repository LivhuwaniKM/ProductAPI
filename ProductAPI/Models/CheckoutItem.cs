namespace ProductAPI.Models
{
    public class CheckoutItem
    {
        //[Key]
        public int Id { get; set; }
        public int CheckoutId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
