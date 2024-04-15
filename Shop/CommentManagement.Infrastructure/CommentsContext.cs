using CommentManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure
{
    public class CommentsContext : DbContext
    {
        public DbSet<Comments> Comments { get; set; }
        public CommentsContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
