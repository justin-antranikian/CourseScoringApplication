using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.DataModels.Configurations;

public static class CourseInformationEntryConfiguration
{
    public static void Configure(EntityTypeBuilder<CourseInformationEntry> builder)
    {
        builder.ToTable("CourseInformationEntries");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();

        builder.Property(oo => oo.CourseInformationType)
            .HasConversion<EnumToStringConverter<CourseInformationType>>()
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder.Property(oo => oo.Description).HasColumnType("VARCHAR(100)").IsRequired();

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.CourseInformationEntries)
            .HasForeignKey(oo => oo.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
