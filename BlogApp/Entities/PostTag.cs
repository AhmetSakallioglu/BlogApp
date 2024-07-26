using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities
{
	[Table("PostTags")]

	public class PostTag
	{
		[Key]
		public int Id { get; set; }
		public int PostId { get; set; }
		public int TagId { get; set; }


		[ForeignKey(nameof(PostId))]
		public Post Post { get; set; }
		[ForeignKey(nameof(TagId))]
		public Tag Tag { get; set; }
	}
}
