export const getImage = (raceSeriesType: string) => {
  switch (raceSeriesType) {
    case "Running":
      return "/RunningClipArt.png"
    case "Swimming":
      return "/SwimmingClipArt.png"
    case "Triathalon":
      return "/Triathalon.jpg"
    case "Mountain Biking":
      return "/MountainBike.jpg"
    case "Road Biking":
      return "/RoadBiking.jpg"
    case "Cross Country Skiing":
      return "/CrossCountrySkiing.png"
    default:
      return ""
  }
}

export const getImageNonFormatted = (raceSeriesType: string) => {
  console.log(raceSeriesType)
  switch (raceSeriesType) {
    case "Running":
      return "/RunningClipArt.png"
    case "Swimming":
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
