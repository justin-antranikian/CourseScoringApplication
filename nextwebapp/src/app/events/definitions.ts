import { LocationInfoWithRank } from "../_components/LocationInfoRankings"

export interface EventSearchResultDto {
  courses: any[]
  description: string
  kickOffDate: string
  kickOffTime: string
  id: number
  locationInfoWithRank: LocationInfoWithRank
  name: string
  raceSeriesTypeName: string
  rating: number
  upcomingRaceId: number
}
