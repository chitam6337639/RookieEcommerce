using StandardLibrary.Comment;

namespace WebMVCEcommerce.Services.Comment
{
	public interface ICommentApiClient
	{
		Task<CommentDto> CreateCommentAsync(CreateCommentDto commentDto);
	}
}
