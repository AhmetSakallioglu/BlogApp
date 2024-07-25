using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities
{
	public class UserPost
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public int PostId { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		[ForeignKey(nameof(PostId))]
		public Post Post { get; set; }
	}
}
