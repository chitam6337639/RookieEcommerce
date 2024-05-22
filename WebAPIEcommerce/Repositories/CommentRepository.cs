using Microsoft.EntityFrameworkCore;
using StandardLibrary.Comment;
using WebAPIEcommerce.Data.DataContext;
using WebAPIEcommerce.Interfaces;

namespace WebAPIEcommerce.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly ApplicationDbContext _context;

		public CommentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<CommentDto> GetCommentDetails(int productId)
		{
			return _context.Comments
				.Where(c => c.ProductId == productId)
				.Include(c => c.User)
				.Select(c => new CommentDto
				{
					UserName = c.User.UserName,
					CommentText = c.CommentText,
					CommentTitle = c.CommentTitle,
					Rating = c.rating
				}).ToList();
		}
	}
}
