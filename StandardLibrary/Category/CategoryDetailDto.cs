using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardLibrary.Product;

namespace StandardLibrary.Category
{
    public class CategoryDetailDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
