using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.Comment
{
	public class CreateCommentDto
	{
		public string? CommentText { get; set; }
		public string CommentTitle { get; set; }
		public decimal Rating { get; set; }
		public int ProductId { get; set; }
	}
}
