using Football.Domain.Constants;
using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.EntityTypeConfiguartions
{
    public sealed class ClubConfiguration : IEntityTypeConfiguration<Clubs>
    {
        public void Configure(EntityTypeBuilder<Clubs> builder)
        {
            builder.ToTable(TableNames.Clubs);

            builder.HasKey(c => c.Id);

            builder
                .Property(club => club.ClubName)
                .HasMaxLength(100)
                .IsRequired(true);

            builder
                .Property(club => club.ClubCountry)
                .HasMaxLength(100)
                .IsRequired(true);

            builder
                .Property(club => club.ClubCity)
                .HasMaxLength(100)
                .IsRequired(false);
        }
    }
}
