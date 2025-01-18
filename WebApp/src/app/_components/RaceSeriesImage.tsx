import React from "react"
import { RaceSeriesType } from "../definitions"
import Image from "next/image"

const RaceSeriesImageMapping: Record<RaceSeriesType, string> = {
  [RaceSeriesType.Running]: "/RunningClipArt.png",
  [RaceSeriesType.Triathalon]: "/Triathalon.jpg",
  [RaceSeriesType.RoadBiking]: "/RoadBiking.jpg",
  [RaceSeriesType.MountainBiking]: "/MountainBike.jpg",
  [RaceSeriesType.CrossCountrySkiing]: "/CrossCountrySkiing.png",
  [RaceSeriesType.Swim]: "/SwimmingClipArt.png",
}

export default function RaceSeriesImage({
  raceSeriesType,
  width = "100%",
  height = 125,
}: {
  raceSeriesType: RaceSeriesType
  width?: string
  height?: number
}) {
  return <img style={{ width, height }} src={RaceSeriesImageMapping[raceSeriesType]} alt={"Rece Series Image"} />
}
