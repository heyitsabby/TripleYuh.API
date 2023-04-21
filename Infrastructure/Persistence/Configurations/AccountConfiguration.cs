using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .Property(account => account.Username)
                .IsRequired()
                .HasMaxLength(AccountRules.MaxUsernameLength);

            builder
               .Property(account => account.Reputation)
               .IsRequired()
               .HasDefaultValue(AccountRules.DefaultReputation);

            builder
                .Property(account => account.Role)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}
