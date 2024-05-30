using StandardLibrary.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.Category
{
	public class CategoryWithSubCategoriesDto
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
		public List<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>(); 
		public List<ProductDto> Products { get; set; } = new List<ProductDto>();
		public int? ParentId { get; set; }
	}
}
