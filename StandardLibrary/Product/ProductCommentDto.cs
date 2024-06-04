using StandardLibrary.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.Product
{
	public class ProductCommentDto
	{
		public ProductDto Product { get; set; }
		public List<CommentDto> Comments { get; set; }
		public CreateCommentDto Input { get; set; } = new CreateCommentDto();

	}
}
