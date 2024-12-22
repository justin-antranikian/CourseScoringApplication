using Api.DataModels;
using Api.Orchestration.Results.GetDetails;

namespace ApiTests.Orchestration.GetIrp;

public class IrpResultByBracketDtoTests
{
    [Fact]
    public void MapsAllFeilds()
    {
        var bracket = new Bracket
        {
            Name = "NA",
            BracketType = BracketType.Overall
        };

        var result = new Result
        {
            OverallRank = 4,
            GenderRank = 3,
            DivisionRank = 2,
            Rank = 2,
            IsHighestIntervalCompleted = false,
            AthleteCourseId = 0,
            BracketId = 0,
            CourseId = 0,
            IntervalId = 0,
            TimeOnInterval = 0,
            TimeOnCourse = 0
        };

        var bracketDto = IrpResultByBracketDtoMapper.GetIrpResultByBracketDto(bracket, result, 10);

        Assert.Equal("NA", bracketDto.Name);
        Assert.Equal(2, bracketDto.Rank);
        Assert.Equal(10, bracketDto.TotalRacers);
    }
}
