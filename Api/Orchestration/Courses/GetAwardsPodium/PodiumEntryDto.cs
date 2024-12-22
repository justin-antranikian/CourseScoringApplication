namespace Api.Orchestration.Courses.GetAwardsPodium;

public record PodiumEntryDto
(
    string BracketName,
    AwardWinnerDto? FirstPlaceAthlete,
    AwardWinnerDto? SecondPlaceAthlete,
    AwardWinnerDto? ThirdPlaceAthlete
);
