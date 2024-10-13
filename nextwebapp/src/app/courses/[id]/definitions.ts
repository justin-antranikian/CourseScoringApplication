import { PaceWithTime } from "@/app/_components/IntervalTime"

export interface CourseLeaderboardDto {
  courseDate: string
  courseDistance: number
  courseName: string
  courseTime: string
  leaderboards: CourseLeaderboardByIntervalDto[]
  locationInfoWithRank: any
  raceId: number
  raceName: string
  raceSeriesId: number
  raceSeriesDescription: string
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
