using Microsoft.AspNetCore.Identity;

namespace WebAPIEcommerce.Models.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName {  get; set; }
        public string? ShippingAddress { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
