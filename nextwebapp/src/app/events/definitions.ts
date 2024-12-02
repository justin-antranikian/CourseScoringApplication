import { LocationInfoWithRank } from "../_components/LocationInfoRankings"

export interface DisplayNameWithId {
  id: number
  displayName: string
}

export interface EventSearchResultDto {
  courses: DisplayNameWithId[]
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
