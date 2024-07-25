using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities
{
	public class Post
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		[StringLength(255)]
		public string ContentUrl { get; set; }

		[Required]
		[StringLength(255)]
		public string ImageUrl { get; set; }

		public bool IsActive { get; set; } = false;

		public ICollection<UserPost> UserPost { get; set; }

	}
}
