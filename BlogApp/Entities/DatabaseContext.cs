using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> User { get; set; }
		public DbSet<Post> Post { get; set; }
		public DbSet<PostUser> PostUser { get; set; }
		public DbSet<Tag> Tag { get; set; }
		public DbSet<PostTag> PostTag { get; set; }
		public DbSet<Image> Image { get; set; }
		public DbSet<PostImage> PostImage { get; set; }
	}
}
