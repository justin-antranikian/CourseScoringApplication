import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"
import { LeaderboardResultDto } from "@/app/_api/courses/definitions"

export interface RaceLeaderboardDto {
  leaderboards: RaceLeaderboardByCourseDto[]
  locationInfoWithRank: LocationInfoWithRank
  raceKickOffDate: string
  raceName: string
  raceSeriesDescription: string
  raceSeriesTypeName: string
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
