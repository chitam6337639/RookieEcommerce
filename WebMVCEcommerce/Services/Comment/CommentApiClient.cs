using Newtonsoft.Json;
using StandardLibrary.Comment;
using System.Text;

namespace WebMVCEcommerce.Services.Comment
{
	public class CommentApiClient : ICommentApiClient
	{
		private readonly HttpClient _httpClient;

		public CommentApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<CommentDto> CreateCommentAsync(CreateCommentDto commentDto)
		{
			commentDto.CommentTitle = "title";
			var json = JsonConvert.SerializeObject(commentDto);
			var data = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("/api/comment/create", data);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<CommentDto>();
				return result;
			}
			return null;
		}
	}

}
