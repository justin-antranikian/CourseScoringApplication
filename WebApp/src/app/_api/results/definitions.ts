import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"
import { RaceSeriesType } from "@/app/definitions"

export interface Irp {
  athleteId: number
  bib: string
  bracketResults: IrpResultByBracketDto[]
  courseId: number
  courseName: string
  courseGoalDescription: string
  finishTime: string | null
  firstName: string
  fullName: string
  genderAbbreviated: string
  intervalResults: IrpResultByIntervalDto[]
  locationInfoWithRank: LocationInfoWithRank
  paceWithTimeCumulative: PaceWithTime
  personalGoalDescription: string
  raceAge: number
  tags: RaceSeriesType[]
  timeZoneAbbreviated: string
  trainingList: string[]
}

export interface IrpResultByBracketDto {
  name: string
  rank: number
  totalRacers: number
}

export interface IrpResultByIntervalDto {
  crossingTime: string | null
  genderCount: number
  genderIndicator: number
  genderRank: number | null
  intervalName: string
  isFullCourse: boolean
  overallCount: number
  overallIndicator: number
  overallRank: number | null
  paceWithTimeCumulative: PaceWithTime | null
  paceWithTimeIntervalOnly: PaceWithTime | null
  primaryDivisionCount: number
  primaryDivisionIndicator: number
  primaryDivisionRank: number | null
}

export interface ResultCompareDto {
  courseId: number
  athleteCourseId: number
  fullName: string
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  raceAge: number
  intervals: ResultCompareIntervalDto[]
}

export interface ResultCompareIntervalDto {
  intervalName: string
  paceWithTime: PaceWithTime | null
  crossingTime: string | null
  overallRank: number | null
  genderRank: number | null
  primaryDivisionRank: number | null
}

export interface IrpSearchRequest {
  courseId?: string | number
  raceId: string | number
  searchTerm: string
}

export interface IrpSearchResult {
  id: number
  athleteId: number
  courseId: number
  bib: string
  courseName: number
  firstName: string
  gender: string
  lastName: string
}
