using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Configurations;

public static class AthleteCourseConfiguration
{
    public static void Configure(EntityTypeBuilder<AthleteCourse> builder)
    {
        builder.ToTable("AthleteCourses");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AthleteId).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();
        builder.Property(oo => oo.Bib).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(oo => oo.CourseGoalDescription).HasColumnType("VARCHAR(500)").IsRequired(false);
        builder.Property(oo => oo.PersonalGoalDescription).HasColumnType("VARCHAR(500)").IsRequired(false);

        builder.HasOne(oo => oo.Athlete)
            .WithMany(oo => oo.AthleteCourses)
            .HasForeignKey(oo => oo.AthleteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.AthleteCourses)
            .HasForeignKey(oo => oo.AthleteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

