namespace Orchestration;

public class MetadataGetTotalHelper
{
    private readonly List<BracketMetadata> _metadataEntries;

    private readonly List<Bracket> _brackets;

    public MetadataGetTotalHelper(List<BracketMetadata> metadataEntries, List<Bracket> brackets)
    {
        _metadataEntries = metadataEntries;
        _brackets = brackets;
    }

    public int GetOverallTotal() => GetTotal(BracketType.Overall);

    public int GetGenderTotal() => GetTotal(BracketType.Gender);

    public int GetPrimaryDivisionTotal() => GetTotal(BracketType.PrimaryDivision);

    private int GetTotal(BracketType bracketType)
    {
        var bracketId = _brackets.Single(oo => oo.BracketType == bracketType).Id;
        return _metadataEntries.Single(oo => oo.BracketId == bracketId).TotalRacers;
    }
}
