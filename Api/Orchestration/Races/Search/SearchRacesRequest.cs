using System.ComponentModel.DataAnnotations;

namespace Api.Orchestration.Races.Search;

public record SearchRacesRequest : IValidatableObject
{
    public required double? Latitude { get; init; }
    public required double? Longitude { get; init; }
    public required int? LocationId { get; init; }
    public required string? LocationType { get; init; }
    public required string? SearchTerm { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Latitude.HasValue && !Longitude.HasValue)
        {
            yield return new ValidationResult($"{nameof(Longitude)} is required if {nameof(Latitude)} is set.", [nameof(Longitude)]);
        }

        if (Longitude.HasValue && !Latitude.HasValue)
        {
            yield return new ValidationResult($"{nameof(Latitude)} is required if {nameof(Longitude)} is set.", [nameof(Latitude)]);
        }
    }
}