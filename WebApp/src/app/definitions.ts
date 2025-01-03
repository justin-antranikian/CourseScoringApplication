export enum RaceSeriesType {
  Running = "Running",
  Triathalon = "Triathalon",
  RoadBiking = "RoadBiking",
  MountainBiking = "MountainBiking",
  CrossCountrySkiing = "CrossCountrySkiing",
  Swim = "Swim",
}

export const RaceSeriesTypeNames: Record<RaceSeriesType, string> = {
  [RaceSeriesType.Running]: "Running",
  [RaceSeriesType.Triathalon]: "Triathalon",
  [RaceSeriesType.RoadBiking]: "Road Biking",
  [RaceSeriesType.MountainBiking]: "Mountain Biking",
  [RaceSeriesType.CrossCountrySkiing]: "Cross Country Skiing",
  [RaceSeriesType.Swim]: "Swimming",
} as const

export const RaceSeriesTypeForAthleteNames: Record<RaceSeriesType, string> = {
  [RaceSeriesType.Running]: "Runner",
  [RaceSeriesType.Triathalon]: "Triathlete",
  [RaceSeriesType.RoadBiking]: "Cyclist",
  [RaceSeriesType.MountainBiking]: "Mountain Biker",
  [RaceSeriesType.CrossCountrySkiing]: "Cross Country Skier",
  [RaceSeriesType.Swim]: "Swimmer",
}
