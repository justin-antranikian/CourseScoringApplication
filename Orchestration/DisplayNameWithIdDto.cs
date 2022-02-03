namespace Orchestration;

/// <summary>
/// Utility class that has properties Id and DisplayName. Could be used in a dropdown, etc.
/// </summary>
public record DisplayNameWithIdDto(int Id, string DisplayName);
