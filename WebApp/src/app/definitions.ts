export enum RaceSeriesType {
  Running = "Running",
  Triathalon = "Triathalon",
  RoadBiking = "RoadBiking",
  MountainBiking = "MountainBiking",
  CrossCountrySkiing = "CrossCountrySkiing",
  Swim = "Swim",
}

export const raceSeriesTypeNameMap = {
  [RaceSeriesType.Running]: "Running",
  [RaceSeriesType.Triathalon]: "Triathalon",
  [RaceSeriesType.RoadBiking]: "Road Biking",
  [RaceSeriesType.MountainBiking]: "Mountain Biking",
  [RaceSeriesType.CrossCountrySkiing]: "Cross Country Skiing",
  [RaceSeriesType.Swim]: "Swimming",
} as const
