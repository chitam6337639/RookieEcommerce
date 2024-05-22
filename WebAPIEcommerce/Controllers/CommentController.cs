using Microsoft.AspNetCore.Mvc;
using WebAPIEcommerce.Interfaces;

namespace WebAPIEcommerce.Controllers
{
	[ApiController]
	[Route("api/comment")]
	public class CommentController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;

		public CommentController(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}

		[HttpGet("{productId}")]
		public IActionResult GetProductComments(int productId)
		{
			var commentDetails = _commentRepository.GetCommentDetails(productId);
			return Ok(commentDetails);
		}
	}
}
