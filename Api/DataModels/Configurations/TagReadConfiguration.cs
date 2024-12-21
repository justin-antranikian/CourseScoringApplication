using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataModels.Configurations;

public static class TagReadConfiguration
{
    public static void Configure(EntityTypeBuilder<TagRead> builder)
    {
        builder.ToTable("TagReads");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AthleteCourseId).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();
        builder.Property(oo => oo.IntervalId).IsRequired();

        builder.Property(oo => oo.TimeOnCourse).IsRequired();
        builder.Property(oo => oo.TimeOnInterval).IsRequired();

        builder.HasOne(oo => oo.AthleteCourse)
            .WithMany(oo => oo.TagReads)
            .HasForeignKey(oo => oo.AthleteCourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.TagReads)
            .HasForeignKey(oo => oo.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Interval)
            .WithMany(oo => oo.TagReads)
            .HasForeignKey(oo => oo.IntervalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
