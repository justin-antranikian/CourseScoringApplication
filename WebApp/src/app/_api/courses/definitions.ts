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
  genderAbbreviated: string
  genderRank: number
  fullName: string
  overallRank: number
  paceWithTimeCumulative: PaceWithTime
  raceAge: number
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
