using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("posts");

            builder
                .Property(post => post.Title)
                .IsRequired()
                .HasMaxLength(PostRules.TitleMaximumLength);

            builder
                .Property(post => post.Reputation)
                .HasDefaultValue(PostRules.DefaultReputation);

            builder
                .Property(post => post.Type)
                .IsRequired()
                .HasConversion<string>();

            builder
                .HasDiscriminator(post => post.Type)
                .HasValue<TextPost>(PostType.Text)
                .HasValue<LinkPost>(PostType.Link);
        }
    }

}
