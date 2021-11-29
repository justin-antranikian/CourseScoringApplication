using System.Collections.Generic;
using DataModels;

namespace Orchestration.ScoreCourses
{
	public record ScoringResult(List<BracketMetadata> MetadataResults, List<Result> Results);
}
