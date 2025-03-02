using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.DataModels.Configurations;

public static class RaceSeriesConfiguration
{
    public static void Configure(EntityTypeBuilder<RaceSeries> builder)
    {
        builder.ToTable("RaceSeries");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AreaLocationId).IsRequired();
        builder.Property(oo => oo.CityLocationId).IsRequired();
        builder.Property(oo => oo.StateLocationId).IsRequired();

        builder.Property(oo => oo.AreaRank).IsRequired();
        builder.Property(oo => oo.CityRank).IsRequired();
        builder.Property(oo => oo.Location).HasColumnType("geography").IsRequired(false);
        builder.Property(oo => oo.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(oo => oo.OverallRank).IsRequired();

        builder.Property(oo => oo.RaceSeriesType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion<EnumToStringConverter<RaceSeriesType>>()
            .IsRequired();

        builder.Property(oo => oo.StateRank).IsRequired();

        builder.HasOne(oo => oo.AreaLocation)
            .WithMany(oo => oo.AreaRaceSeries)
            .HasForeignKey(oo => oo.AreaLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.CityLocation)
            .WithMany(oo => oo.CityRaceSeries)
            .HasForeignKey(oo => oo.CityLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.StateLocation)
            .WithMany(oo => oo.StateRaceSeries)
            .HasForeignKey(oo => oo.StateLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
