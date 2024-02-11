
export enum RaceSeriesType {
  Running,
  Triathalon,
  RoadBiking,
  MountainBiking,
  CrossCountrySkiing,
  Swim
}

export class RaceSeriesTypeToImageUrl {

  /**
  * A helper method that takes a RaceSeriesType and returns the corresponding image path.
  * @param raceSeriesType
  * @returns image path
  */
  public static getImageUrl(raceSeriesType: RaceSeriesType): string {
    switch (raceSeriesType) {
      case RaceSeriesType.Running: {
        return '/assets/img/RunningClipArt.png'
      }
      case RaceSeriesType.Triathalon: {
        return '/assets/img/Triathalon.jpg'
      }
      case RaceSeriesType.RoadBiking: {
        return '/assets/img/RoadBiking.jpg'
      }
      case RaceSeriesType.MountainBiking: {
        return '/assets/img/MountainBike.jpg'
      }
      case RaceSeriesType.CrossCountrySkiing: {
        return '/assets/img/CrossCountrySkiing.png'
      }
      case RaceSeriesType.Swim: {
        return '/assets/img/SwimmingClipArt.png'
      }
    }
  }
}