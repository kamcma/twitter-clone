using Microsoft.EntityFrameworkCore;
using TwitterClone.Data.Models;

namespace TwitterClone.Data
{
    public class TwitterCloneContext : DbContext
    {
        public TwitterCloneContext(DbContextOptions<TwitterCloneContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get;set;}
    }
}