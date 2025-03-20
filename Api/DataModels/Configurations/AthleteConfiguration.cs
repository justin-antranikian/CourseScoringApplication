using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.DataModels.Configurations;

public static class AthleteConfiguration
{
    public static void Configure(EntityTypeBuilder<Athlete> builder)
    {
        builder.ToTable("Athletes");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AreaLocationId).IsRequired();
        builder.Property(oo => oo.CityLocationId).IsRequired();
        builder.Property(oo => oo.StateLocationId).IsRequired();

        builder.Property(oo => oo.AreaRank).IsRequired();
        builder.Property(oo => oo.CityRank).IsRequired();
        builder.Property(oo => oo.DateOfBirth).IsRequired();
        builder.Property(oo => oo.Location).HasColumnType("geometry").IsRequired();

        builder.Property(oo => oo.Gender)
            .HasConversion<EnumToStringConverter<Gender>>()
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder.Property(oo => oo.FirstName).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(oo => oo.FullName).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(oo => oo.LastName).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(oo => oo.OverallRank).IsRequired();
        builder.Property(oo => oo.StateRank).IsRequired();

        builder.HasOne(oo => oo.AreaLocation)
            .WithMany(oo => oo.AreaAthletes)
            .HasForeignKey(oo => oo.AreaLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.CityLocation)
            .WithMany(oo => oo.CityAthletes)
            .HasForeignKey(oo => oo.CityLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.StateLocation)
            .WithMany(oo => oo.StateAthletes)
            .HasForeignKey(oo => oo.StateLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
