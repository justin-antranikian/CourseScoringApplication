using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Configurations;

public static class AthleteConfiguration
{
    public static void ConfigureAthleteEntity(EntityTypeBuilder<Athlete> builder)
    {
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
    }
}

