using Microsoft.AspNetCore.Mvc;
using StandardLibrary.Comment;
using WebMVCEcommerce.Services.Comment;

namespace WebMVCEcommerce.Controllers
{
	public class CommentController : Controller
	{
		private readonly ICommentApiClient _commentApiClient;

		public CommentController(ICommentApiClient commentApiClient)
		{
			_commentApiClient = commentApiClient;
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCommentDto commentDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var createdComment = await _commentApiClient.CreateCommentAsync(commentDto);

			if (createdComment != null)
			{
				return Redirect($"~/Product/Details/{commentDto.ProductId}");
			}

			return View("Error");
		}
	}
}
