import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"
import { RaceSeriesType } from "@/app/definitions"

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
  id: number
  courseDate: string
  courseTime: string
  distance: number
  locationInfoWithRank: LocationInfoWithRank
  name: string
  raceId: number
  raceName: string
  raceSeriesType: RaceSeriesType
}
