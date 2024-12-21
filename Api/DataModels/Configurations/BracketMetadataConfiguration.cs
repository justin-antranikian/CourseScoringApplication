using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataModels.Configurations;

public static class BracketMetadataConfiguration
{
    public static void Configure(EntityTypeBuilder<BracketMetadata> builder)
    {
        builder.ToTable("BracketMetadatas");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();
        builder.Property(oo => oo.IntervalId).IsRequired(false);

        builder.Property(oo => oo.TotalRacers).IsRequired(true);

        builder.HasOne(oo => oo.Bracket)
            .WithMany(oo => oo.BracketMetadatas)
            .HasForeignKey(oo => oo.BracketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.BracketMetadatas)
            .HasForeignKey(oo => oo.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Interval)
            .WithMany(oo => oo.BracketMetadatas)
            .HasForeignKey(oo => oo.IntervalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
