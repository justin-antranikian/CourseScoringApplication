using Api.DataModels;
using Api.DataModels.Enums;

namespace Api.Orchestration;

public class MetadataGetTotalHelper(List<BracketMetadata> metadataEntries, List<Bracket> brackets)
{
    public int GetOverallTotal() => GetTotal(BracketType.Overall);
    public int GetGenderTotal() => GetTotal(BracketType.Gender);
    public int GetPrimaryDivisionTotal() => GetTotal(BracketType.PrimaryDivision);

    private int GetTotal(BracketType bracketType)
    {
        var bracketId = brackets.Single(oo => oo.BracketType == bracketType).Id;
        return metadataEntries.Single(oo => oo.BracketId == bracketId).TotalRacers;
    }
}
