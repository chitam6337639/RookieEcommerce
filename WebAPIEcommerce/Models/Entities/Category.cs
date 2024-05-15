using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIEcommerce.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? ParentId { get; set; }
        [NotMapped]
        public Category? Parent { get; set; } 
        public ICollection<Category>? SubCategories { get; set; }
        public ICollection<Product>? Products { get; set;}

    }
}
