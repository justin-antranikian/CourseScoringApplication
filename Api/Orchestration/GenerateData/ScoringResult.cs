using Api.DataModels;

namespace Api.Orchestration.GenerateData;

public record ScoringResult(List<BracketMetadata> MetadataResults, List<Result> Results);
