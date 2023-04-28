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
                .Property(post => post.Body)
                .IsRequired()
                .HasMaxLength(CommentRules.BodyMaximumLength);

            builder
                .Property(post => post.Reputation)
                .HasDefaultValue(CommentRules.DefaultReputation);
        }
    }
}
