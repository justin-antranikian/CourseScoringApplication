import { IIntervalType } from "../../../_common/IIntervalName";
import { IRaceSeriesType } from "../../../_common/IRaceSeriesType";
import { LocationInfoWithRank } from "../../locationInfoWithRank";
import { LeaderboardResultDto } from "../leaderboardResultDto";

export interface RaceLeaderboardDto extends IRaceSeriesType {
  raceName: string
  raceSeriesDescription: string
  raceKickOffDate: string
  locationInfoWithRank: LocationInfoWithRank
  leaderboards: RaceLeaderboardByCourseDto[]
}

export interface RaceLeaderboardByCourseDto extends IIntervalType {
  courseId: number
  courseName: string
  sortOrder: number
  highestIntervalName: string
  results: LeaderboardResultDto[]
}