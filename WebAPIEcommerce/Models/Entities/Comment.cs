namespace WebAPIEcommerce.Models.Entities
{
	public class Comment
	{
		public int CommentId { get; set; }
		public string? CommentText { get; set; }
		public string? CommentTitle { get; set; } = string.Empty;
		public decimal rating { get; set; }
		public string? UserId { get; set; }
		public User? User { get; set; }
		public int ProductId { get; set; }
		public Product? Product { get; set; }

	}
}
