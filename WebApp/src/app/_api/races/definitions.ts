import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"
import { LeaderboardResultDto } from "@/app/_api/courses/definitions"
import { RaceSeriesType } from "@/app/definitions"

export interface RaceLeaderboardDto {
  latitude: number
  longitude: number
  locationInfoWithRank: LocationInfoWithRank
  raceKickOffDate: string
  raceName: string
  raceSeriesType: RaceSeriesType
  leaderboards: RaceLeaderboardByCourseDto[]
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

export interface RaceSearchResultDto {
  id: number
  distance: number | null
  locationInfoWithRank: LocationInfoWithRank
  name: string
  raceKickOffDate: string
  raceSeriesType: RaceSeriesType
  upcomingRaceId: number
  courses: DisplayNameWithId[]
}
