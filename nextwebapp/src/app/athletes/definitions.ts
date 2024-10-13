import { LocationInfoWithRank } from "../_components/LocationInfoRankings"

export interface AthleteSearchResultDto {
  id: number
  fullName: string
  age: number
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  tags: string[]
}
