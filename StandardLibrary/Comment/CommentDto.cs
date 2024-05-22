using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.Comment
{
	public class CommentDto
	{
		public string UserName { get; set; }
		public string? CommentText { get; set; }
		public string CommentTitle { get; set; }
		public decimal Rating { get; set; }
	}
}
