using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class LinkPostConfiguration : IEntityTypeConfiguration<LinkPost>
    {
        public void Configure(EntityTypeBuilder<LinkPost> builder)
        {
            builder
                .Property(post => post.Url)
                .IsRequired();

            builder
                .Property(post => post.Type)
                .HasDefaultValue(PostType.Link);
        }
    }
}
