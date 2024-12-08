import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"

export interface CourseLeaderboardDto {
  courseDate: string
  courseDistance: number
  courseName: string
  courseTime: string
  leaderboards: CourseLeaderboardByIntervalDto[]
  locationInfoWithRank: LocationInfoWithRank
  raceId: number
  raceName: string
  raceSeriesId: number
  raceSeriesDescription: string
  raceSeriesType: number
  raceSeriesTypeName: string
}

export interface CourseLeaderboardByIntervalDto {
  intervalName: string
  intervalType: any
  results: LeaderboardResultDto[]
  totalRacers: number
}

// incorrect place for this.
export interface LeaderboardResultDto {
  athleteCourseId: number
  athleteId: number
  bib: string
  divisionRank: number
  genderRank: number
  genderAbbreviated: string
  fullName: string
  overallRank: number
  raceAge: number
  paceWithTimeCumulative: PaceWithTime
}
