using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("AthleteWellnessEntries")]
public class AthleteWellnessEntry
{
    [Key]
    public int Id { get; set; }

    public int AthleteId { get; set; }

    public AthleteWellnessType AthleteWellnessType { get; set; }

    public string Description { get; set; }

    public AthleteWellnessEntry(AthleteWellnessType athleteWellnessType, string description)
    {
        AthleteWellnessType = athleteWellnessType;
        Description = description;
    }
}
