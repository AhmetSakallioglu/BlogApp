using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities
{
	[Table("Posts")]

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

		public string CreatedBy { get; set; }

		public DateTime Date { get; set; } = DateTime.Now;

		public bool IsActive { get; set; } = false;

		public ICollection<PostImage> PostImage { get; set; }
		public ICollection<PostTag> PostTag { get; set; }

	}
}
