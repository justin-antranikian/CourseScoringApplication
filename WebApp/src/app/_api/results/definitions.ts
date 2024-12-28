import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"

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
  raceId: number
  raceName: string
  raceSeriesLocationInfoWithRank: LocationInfoWithRank
  tags: string[]
  timeZoneAbbreviated: string
  trainingList: string[]
}

export interface IrpResultByBracketDto {
  id: number
  name: string
  rank: number
  totalRacers: number
}

export interface IrpResultByIntervalDto {
  intervalName: string
  paceWithTimeCumulative: PaceWithTime | null
  paceWithTimeIntervalOnly: PaceWithTime | null
  overallRank: number | null
  genderRank: number | null
  primaryDivisionRank: number | null
  overallCount: number
  genderCount: number
  primaryDivisionCount: number
  overallIndicator: number
  genderIndicator: number
  primaryDivisionIndicator: number
  crossingTime: string | null
  isFullCourse: boolean
}

export interface CompareIrpsAthleteInfoDto {
  courseId: number
  athleteCourseId: number
  city: string
  fullName: string
  genderAbbreviated: string
  raceAge: number
  state: string
  compareIrpsIntervalDtos: CompareIrpsIntervalDto[]
}

export interface CompareIrpsIntervalDto {
  intervalName: string
  paceWithTime: PaceWithTime | null
  crossingTime: string | null
  overallRank: number | null
  genderRank: number | null
  primaryDivisionRank: number | null
}
