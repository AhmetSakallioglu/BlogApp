using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities
{
	[Table("Images")]

	public class Image
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string ImageUrl { get; set; }

		public ICollection<PostImage> PostImages { get; set; }

	}
}
