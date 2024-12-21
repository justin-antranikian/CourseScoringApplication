namespace Api.Orchestration.GetAwardsPodium;

public record PodiumEntryDto
(
    string BracketName,
    AwardWinnerDto? FirstPlaceAthlete,
    AwardWinnerDto? SecondPlaceAthlete,
    AwardWinnerDto? ThirdPlaceAthlete
);
