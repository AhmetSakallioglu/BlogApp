using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities
{
	[Table("PostImages")]

	public class PostImage
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public int ImageId { get; set; }



		[ForeignKey(nameof(PostId))]
		public Post Post { get; set; }

		[ForeignKey(nameof(ImageId))]
		public Image Image { get; set; }

	}
}
