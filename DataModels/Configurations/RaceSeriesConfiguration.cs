using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataModels.Configurations;

public static class RaceSeriesConfiguration
{
    public static void Configure(EntityTypeBuilder<RaceSeries> builder)
    {
        builder.ToTable("RaceSeries");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(oo => oo.Description).HasColumnType("VARCHAR(250)").IsRequired();

        builder.Property(oo => oo.RaceSeriesType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion<EnumToStringConverter<RaceSeriesType>>()
            .IsRequired();

        builder.Property(oo => oo.State).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(oo => oo.Area).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(oo => oo.City).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(oo => oo.OverallRank).IsRequired();
        builder.Property(oo => oo.StateRank).IsRequired();
        builder.Property(oo => oo.AreaRank).IsRequired();
        builder.Property(oo => oo.CityRank).IsRequired();
        builder.Property(oo => oo.IsUpcoming).IsRequired();
        builder.Property(oo => oo.Rating).IsRequired();
    }
}
