// import { LocationInfoWithRank } from "../locationInfoWithRank";
// import { DisplayNameWithIdDto } from "../displayNameWithIdDto";
import { IRaceSeriesType } from "../../_common/IRaceSeriesType";

export interface EventSearchResultDto extends IRaceSeriesType {
  id: number
  name: string
  raceSeriesTypeName: string
  upcomingRaceId: number
  kickOffDate: string
  kickOffTime: string
  description: string
  courses: any[]
  locationInfoWithRank: any
  rating: number
}
