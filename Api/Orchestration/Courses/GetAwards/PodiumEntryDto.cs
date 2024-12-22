namespace Api.Orchestration.Courses.GetAwards;

public record PodiumEntryDto
(
    string BracketName,
    AwardWinnerDto? FirstPlaceAthlete,
    AwardWinnerDto? SecondPlaceAthlete,
    AwardWinnerDto? ThirdPlaceAthlete
);
