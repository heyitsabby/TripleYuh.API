using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class TextPostConfiguration : IEntityTypeConfiguration<TextPost>
    {
        public void Configure(EntityTypeBuilder<TextPost> builder)
        {
            builder
                .Property(post => post.Body)
                .HasMaxLength(PostRules.BodyMaximumLength);

            builder
                .Property(post => post.Type)
                .HasDefaultValue(PostType.Text);
        }
    }
}
