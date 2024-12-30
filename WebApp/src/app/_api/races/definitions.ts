import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"
import { LeaderboardResultDto } from "@/app/_api/courses/definitions"
import { RaceSeriesType } from "@/app/definitions"

export interface RaceLeaderboardDto {
  leaderboards: RaceLeaderboardByCourseDto[]
  locationInfoWithRank: LocationInfoWithRank
  raceKickOffDate: string
  raceName: string
  raceSeriesDescription: string
  raceSeriesType: RaceSeriesType
}

export interface RaceLeaderboardByCourseDto {
  courseId: number
  courseName: string
  results: LeaderboardResultDto[]
}

export interface DisplayNameWithId {
  id: number
  displayName: string
}

export interface EventSearchResultDto {
  id: number
  locationInfoWithRank: LocationInfoWithRank
  name: string
  raceKickOffDate: string
  raceSeriesType: RaceSeriesType
  upcomingRaceId: number
  courses: DisplayNameWithId[]
}
