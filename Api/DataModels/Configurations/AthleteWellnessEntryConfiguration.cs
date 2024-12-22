using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.DataModels.Configurations;

public static class AthleteWellnessEntryConfiguration
{
    public static void Configure(EntityTypeBuilder<AthleteWellnessEntry> builder)
    {
        builder.ToTable("AthleteWellnessEntries");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AthleteId).IsRequired();

        builder.Property(oo => oo.AthleteWellnessType)
            .HasConversion<EnumToStringConverter<AthleteWellnessType>>()
            .HasColumnType("VARCHAR(50)").IsRequired();

        builder.Property(oo => oo.Description).HasColumnType("VARCHAR(200)").IsRequired();

        builder.HasOne(oo => oo.Athlete)
            .WithMany(oo => oo.AthleteWellnessEntries)
            .HasForeignKey(oo => oo.AthleteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

