namespace Api.Orchestration;

public record PaceWithTime(string TimeFormatted, bool HasPace, string? PaceValue = null, string PaceLabel = null);
