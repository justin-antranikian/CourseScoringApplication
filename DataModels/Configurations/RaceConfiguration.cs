using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataModels.Configurations;

public static class RaceConfiguration
{
    public static void Configure(EntityTypeBuilder<Race> builder)
    {
        builder.ToTable("Races");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(oo => oo.KickOffDate).IsRequired();
        builder.Property(oo => oo.TimeZoneId).IsRequired();

        builder.HasOne(oo => oo.RaceSeries)
            .WithMany(oo => oo.Races)
            .HasForeignKey(oo => oo.RaceSeriesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
