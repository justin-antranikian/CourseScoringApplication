import { LocationInfoWithRank } from "src/app/_orchestration/locationInfoWithRank";
import { DisplayNameWithIdDto } from "../../displayNameWithIdDto";
import { BracketType } from "../../../_core/enums/bracketType";
import { IIntervalType } from "../../../_common/IIntervalName";
import { IRaceSeriesType } from "../../../_common/IRaceSeriesType";
import { LeaderboardResultDto } from "../leaderboardResultDto";

export interface CourseLeaderboardDto extends IRaceSeriesType {
  raceName: string
  raceSeriesDescription: string
  locationInfoWithRank: LocationInfoWithRank
  courseName: string
  courseDate: string
  courseTime: string
  courseDistance: number
  courseMetadata: CourseMetadata
  leaderboards: CourseLeaderboardByIntervalDto[]
}

export interface CourseLeaderboardByIntervalDto extends IIntervalType {
  intervalName: string
  totalRacers: number
  results: LeaderboardResultDto[]
}

export interface CourseMetadata {
  courses: DisplayNameWithIdDto[]
  brackets: BracketMetadata[]
  intervals: DisplayNameWithIdDto[]
}

export interface BracketMetadata extends DisplayNameWithIdDto {
  bracketType: BracketType
}