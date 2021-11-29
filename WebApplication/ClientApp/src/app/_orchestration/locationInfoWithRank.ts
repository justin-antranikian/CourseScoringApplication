import { LocationInfoWithUrl } from "./locationInfoWithUrl";

export interface LocationInfoWithRank extends LocationInfoWithUrl {
  overallRank: number,
  stateRank: number
  areaRank: number
  cityRank: number
}