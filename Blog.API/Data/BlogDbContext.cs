using Blog.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        //Create new table in the database if it doesnt exists
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
