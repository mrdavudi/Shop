using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Email).HasMaxLength(500);
            builder.Property(x => x.Website).HasMaxLength(500);
            builder.Property(x => x.Message).HasMaxLength(1000);
        }
    }
}
