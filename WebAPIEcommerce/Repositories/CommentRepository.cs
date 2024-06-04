using Microsoft.EntityFrameworkCore;
using StandardLibrary.Comment;
using WebAPIEcommerce.Data.DataContext;
using WebAPIEcommerce.Interfaces;
using WebAPIEcommerce.Models.Entities;

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
		public async Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto, string userId)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null)
			{
				throw new KeyNotFoundException("User not found");
			}

			var comment = new Comment
			{
				UserId = user.Id,
				CommentText = createCommentDto.CommentText,
				CommentTitle = createCommentDto.CommentTitle,
				rating = createCommentDto.Rating,
				ProductId = createCommentDto.ProductId
			};

			_context.Comments.Add(comment);
			await _context.SaveChangesAsync();

			return new CommentDto
			{
				CommentId = comment.CommentId,
				UserName = user.UserName,
				CommentText = comment.CommentText,
				CommentTitle = comment.CommentTitle,
				Rating = comment.rating,
				ProductId = comment.ProductId
			};
		}
	}
}
