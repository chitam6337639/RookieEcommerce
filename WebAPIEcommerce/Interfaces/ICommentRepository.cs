using StandardLibrary.Comment;
using StandardLibrary.Product;

namespace WebAPIEcommerce.Interfaces
{
	public interface ICommentRepository
	{
		List<CommentDto> GetCommentDetails(int productId);
		Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto, string userId);
	}
}
