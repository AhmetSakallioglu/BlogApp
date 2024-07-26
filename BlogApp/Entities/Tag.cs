using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities
{
	[Table("Tags")]

	public class Tag
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<PostTag> PostTags { get; set; }

	}
}
