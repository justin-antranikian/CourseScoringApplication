using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Configurations;

public static class ResultConfiguration
{
    public static void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.ToTable("Results");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AthleteCourseId).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();
        builder.Property(oo => oo.IntervalId).IsRequired();
        builder.Property(oo => oo.BracketId).IsRequired();

        builder.Property(oo => oo.TimeOnInterval).IsRequired();
        builder.Property(oo => oo.TimeOnCourse).IsRequired();
        builder.Property(oo => oo.Rank).IsRequired();

        builder.HasOne(oo => oo.AthleteCourse)
            .WithMany(oo => oo.Results)
            .HasForeignKey(oo => oo.AthleteCourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Bracket)
            .WithMany(oo => oo.Results)
            .HasForeignKey(oo => oo.BracketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.Results)
            .HasForeignKey(oo => oo.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Interval)
            .WithMany(oo => oo.Results)
            .HasForeignKey(oo => oo.IntervalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
