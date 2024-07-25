using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Post> Post { get; set; }
		public DbSet<UserPost> UserPost { get; set; }
	}
}
