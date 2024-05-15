namespace WebAPIEcommerce.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int QuanlityTotal { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ShippingDate { get; set; }

        public string? ShippingAddress { get; set; }
        public User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        

    }
}
