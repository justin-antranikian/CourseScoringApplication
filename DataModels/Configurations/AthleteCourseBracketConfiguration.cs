using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Configurations;

public static class AthleteCourseBracketConfiguration
{
    public static void Configure(EntityTypeBuilder<AthleteCourseBracket> builder)
    {
        builder.ToTable("AthleteCourseBrackets");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AthleteCourseId).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();
        builder.Property(oo => oo.BracketId).IsRequired();

        builder.HasOne(oo => oo.AthleteCourse)
            .WithMany(oo => oo.AthleteCourseBrackets)
            .HasForeignKey(oo => oo.AthleteCourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Bracket)
            .WithMany(oo => oo.AthleteCourseBrackets)
            .HasForeignKey(oo => oo.BracketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.AthleteCourseBrackets)
            .HasForeignKey(oo => oo.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

