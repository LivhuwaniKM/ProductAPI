namespace ProductAPI.Models
{
    public class Checkout
    {
        //[Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public List<CheckoutItem> Items { get; set; } = [];
    }
}
