using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Configurations;

public static class AthleteCourseTrainingConfiguration
{
    public static void Configure(EntityTypeBuilder<AthleteCourseTraining> builder)
    {
        builder.ToTable("AthleteCourseTrainings");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AthleteCourseId).IsRequired();
        builder.Property(oo => oo.Description).HasColumnType("VARCHAR(100)").IsRequired();

        builder.HasOne(oo => oo.AthleteCourse)
            .WithMany(oo => oo.AthleteCourseTrainings)
            .HasForeignKey(oo => oo.AthleteCourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

