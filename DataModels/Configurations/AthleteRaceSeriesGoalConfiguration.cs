using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataModels.Configurations;

public static class AthleteRaceSeriesGoalConfiguration
{
    public static void Configure(EntityTypeBuilder<AthleteRaceSeriesGoal> builder)
    {
        builder.ToTable("AthleteRaceSeriesGoals");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.AthleteId).IsRequired();

        builder.Property(oo => oo.RaceSeriesType)
            .HasConversion<EnumToStringConverter<RaceSeriesType>>()
            .HasColumnType("VARCHAR(50)").IsRequired();

        builder.Property(oo => oo.TotalEvents).IsRequired();

        builder.HasOne(oo => oo.Athlete)
            .WithMany(oo => oo.AthleteRaceSeriesGoals)
            .HasForeignKey(oo => oo.AthleteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
