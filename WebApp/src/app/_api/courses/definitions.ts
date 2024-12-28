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
  intervalType: number
  results: LeaderboardResultDto[]
  totalRacers: number
}

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

export interface AwardsDto {
  bracketName: string
  firstPlaceAthlete: AwardWinnerDto | null
  secondPlaceAthlete: AwardWinnerDto | null
  thirdPlaceAthlete: AwardWinnerDto | null
}

export interface AwardWinnerDto {
  athleteId: number
  athleteCourseId: number
  fullName: string
  finishTime: string
  paceWithTime: PaceWithTime
}

export interface CourseDetailsDto {
  courseId: number
  courseName: string
  raceId: number
  raceName: string
  courseType: string
  distance: number
  name: string
  paceType: string
  preferredMetric: string
  sortOrder: number
  locationInfoWithRank: LocationInfoWithRank
}
