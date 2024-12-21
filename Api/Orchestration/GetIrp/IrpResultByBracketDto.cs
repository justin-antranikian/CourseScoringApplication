using Api.DataModels;

namespace Api.Orchestration.GetIrp;

public static class IrpResultByBracketDtoMapper
{
    public static IrpResultByBracketDto GetIrpResultByBracketDto(Bracket bracket, Result result, int totalRacers)
    {
        var rank = result.Rank;

        var irpResultByBracketDto = new IrpResultByBracketDto
        (
            bracket.Id,
            bracket.Name,
            rank,
            totalRacers
        );

        return irpResultByBracketDto;
    }
}

public record IrpResultByBracketDto
(
    int Id,
    string Name,
    int Rank,
    int TotalRacers
);
