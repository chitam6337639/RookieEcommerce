using StandardLibrary.Comment;
using StandardLibrary.Product;

namespace WebMVCEcommerce.Models
{
	public class ProductCommentViewModel
	{
		public ProductDto Product { get; set; }
		public List<CommentDto> Comments { get; set; }
	}
}
