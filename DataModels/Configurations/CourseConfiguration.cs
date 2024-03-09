using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataModels.Configurations;

public static class CourseConfiguration
{
    public static void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.RaceId).IsRequired();

        builder.Property(oo => oo.CourseType)
            .HasConversion<EnumToStringConverter<CourseType>>()
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder.Property(oo => oo.Distance).IsRequired();
        builder.Property(oo => oo.Name).HasColumnType("VARCHAR(100)").IsRequired();

        builder.Property(oo => oo.PaceType)
            .HasConversion<EnumToStringConverter<PaceType>>()
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder.Property(oo => oo.PreferedMetric)
            .HasConversion<EnumToStringConverter<PreferedMetric>>()
            .HasColumnType("VARCHAR(25)")
            .IsRequired();

        builder.Property(oo => oo.SortOrder).IsRequired();
        builder.Property(oo => oo.StartDate).IsRequired();

        builder.HasOne(oo => oo.Race)
            .WithMany(oo => oo.Courses)
            .HasForeignKey(oo => oo.RaceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
