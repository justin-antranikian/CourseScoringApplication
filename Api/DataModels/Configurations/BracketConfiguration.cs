using Api.DataModels.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.DataModels.Configurations;

public static class BracketConfiguration
{
    public static void Configure(EntityTypeBuilder<Bracket> builder)
    {
        builder.ToTable("Brackets");

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id).IsRequired();
        builder.Property(oo => oo.CourseId).IsRequired();

        builder.Property(oo => oo.BracketType)
            .HasConversion<EnumToStringConverter<BracketType>>()
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder.Property(oo => oo.Name).HasColumnType("VARCHAR(100)").IsRequired();

        builder.HasOne(oo => oo.Course)
            .WithMany(oo => oo.Brackets)
            .HasForeignKey(oo => oo.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
