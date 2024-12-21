using Api.DataModels;

namespace Api.Orchestration.ScoreCourses;

public record ScoringResult(List<BracketMetadata> MetadataResults, List<Result> Results);
