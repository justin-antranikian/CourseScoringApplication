using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;

namespace Api.DataModels.Configurations;

public static class LocationConfiguration
{
    public static void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Locations");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.ParentLocationId).IsRequired(false);

        builder.Property(oo => oo.LocationType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion<EnumToStringConverter<LocationType>>()
            .IsRequired();

        builder.Property(oo => oo.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(oo => oo.Slug).HasColumnType("VARCHAR(500)").IsRequired();

        builder.HasOne(oo => oo.ParentLocation)
            .WithMany(oo => oo.ChildLocations)
            .HasForeignKey(oo => oo.ParentLocationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}