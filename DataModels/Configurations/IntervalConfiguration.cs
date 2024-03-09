using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataModels.Configurations;

public static class IntervalConfiguration
{
    public static void Configure(EntityTypeBuilder<Interval> builder)
    {
        builder.ToTable("Intervals");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();

        builder.Property(oo => oo.Distance).IsRequired();
        builder.Property(oo => oo.DistanceFromStart).IsRequired();
        builder.Property(oo => oo.Description).HasColumnType("VARCHAR(250)").IsRequired(false);

        builder.Property(oo => oo.IntervalType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion<EnumToStringConverter<IntervalType>>()
            .IsRequired();

        builder.Property(oo => oo.IsFullCourse).IsRequired();
        builder.Property(oo => oo.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(oo => oo.Order).IsRequired();

        builder.Property(oo => oo.PaceType)
            .HasColumnType("VARCHAR(50)")
            .HasConversion<EnumToStringConverter<PaceType>>()
            .IsRequired();

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.Intervals)
            .HasForeignKey(oo => oo.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
