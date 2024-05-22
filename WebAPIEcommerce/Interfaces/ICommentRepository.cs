using StandardLibrary.Comment;

namespace WebAPIEcommerce.Interfaces
{
	public interface ICommentRepository
	{
		List<CommentDto> GetCommentDetails(int productId);
	}
}
