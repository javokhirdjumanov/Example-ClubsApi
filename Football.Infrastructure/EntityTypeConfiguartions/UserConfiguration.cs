using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Football.Domain.Entities;
using Football.Domain.Constants;

namespace Football.Infrastructure.EntityTypeConfiguartions
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable(TableNames.Users);

            builder.HasKey(user => user.Id);

            builder
                .Property(user => user.FirstName)
                .HasMaxLength(50)
                .IsRequired(true);

            builder
                .Property(user => user.LastName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder
                .Property(user => user.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired(true);

            builder
                .Property(user => user.Email)
                .HasMaxLength(256)
                .IsRequired(true);

            builder
                .Property(user => user.PasswordHash)
                .HasMaxLength(256)
                .IsRequired(true);

            builder
                .Property(user => user.Salt)
                .HasMaxLength(100)
                .IsRequired(true);

            builder
                .Property(user => user.CreateAt)
                .IsRequired(true);

            builder
                .Property(user => user.UpdateAt)
                .IsRequired(false);

            builder
                .HasOne(user => user.Clubs)
                .WithMany(club => club.Users)
                .HasForeignKey(user => user.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
