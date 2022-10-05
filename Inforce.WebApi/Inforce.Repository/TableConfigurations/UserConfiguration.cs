
using Inforce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inforce.Repository.TableConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Urls)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(u => u.Role)
            .HasConversion<string>();
        }
    }
}
