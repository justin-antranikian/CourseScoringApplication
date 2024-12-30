export const getImageNonFormatted = (raceSeriesType: string) => {
  switch (raceSeriesType) {
    case "Running":
      return "/RunningClipArt.png"
    case "Swim":
      return "/SwimmingClipArt.png"
    case "Triathalon":
      return "/Triathalon.jpg"
    case "MountainBiking":
      return "/MountainBike.jpg"
    case "RoadBiking":
      return "/RoadBiking.jpg"
    case "CrossCountrySkiing":
      return "/CrossCountrySkiing.png"
    default:
      return ""
  }
}
