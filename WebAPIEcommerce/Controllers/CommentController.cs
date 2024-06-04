using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StandardLibrary.Comment;
using System.Security.Claims;
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

		[Authorize]
		[HttpPost("create")]
		public async Task<IActionResult> PostComment([FromBody] CreateCommentDto createCommentDto)
		{
			if (createCommentDto == null)
			{
				return BadRequest();
			}

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var createdComment = await _commentRepository.CreateCommentAsync(createCommentDto, userId);

			return CreatedAtAction(nameof(PostComment), new { id = createdComment.CommentId }, createdComment);
		}
	}
}
