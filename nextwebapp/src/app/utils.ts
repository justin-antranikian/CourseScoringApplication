export const getImage = (raceSeriesType: string) => {
  console.log(raceSeriesType)
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
