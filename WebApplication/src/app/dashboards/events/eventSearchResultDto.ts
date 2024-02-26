import { IRaceSeriesType } from "../../_common/IRaceSeriesType";

export interface EventSearchResultDto extends IRaceSeriesType {
  courses: any[]
  description: string
  kickOffDate: string
  kickOffTime: string
  id: number
  locationInfoWithRank: any
  name: string
  raceSeriesTypeName: string
  rating: number
  upcomingRaceId: number
}