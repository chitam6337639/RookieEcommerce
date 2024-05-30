using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIEcommerce.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
		public string? CategoryDescription { get; set; }
		public int? ParentId { get; set; }
        [NotMapped]
        public Category? Parent { get; set; }
        public List<Category> SubCategories { get; set; } = new(); 
        public List<Product> Products { get; set; } = new();

    }
}
