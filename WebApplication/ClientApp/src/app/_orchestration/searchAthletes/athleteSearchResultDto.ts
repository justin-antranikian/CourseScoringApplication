import { LocationInfoWithRank } from "../locationInfoWithRank";

export interface AthleteSearchResultDto {
  id: number
  fullName: string
  age: number
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  tags: string[]
}