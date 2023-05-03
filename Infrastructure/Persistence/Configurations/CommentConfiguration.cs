using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");

            builder
                .Property(comment => comment.Body)
                .IsRequired()
                .HasMaxLength(CommentRules.BodyMaximumLength);

            builder
                .Property(comment => comment.Reputation)
                .HasDefaultValue(CommentRules.DefaultReputation);
        }
    }
}
