import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"

export interface Irp {
  athleteId: number
  bib: string
  bracketResults: IrpResultByBracketDto[]
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
  intervalType: any
  intervalFinished: boolean
  paceWithTimeCumulative: PaceWithTime
  paceWithTimeIntervalOnly: PaceWithTime
  overallRank: number | null
  genderRank: number | null
  primaryDivisionRank: number | null
  overallCount: number
  genderCount: number
  primaryDivisionCount: number
  overallIndicator: any
  genderIndicator: any
  primaryDivisionIndicator: any
  crossingTime: string | null
  isFullCourse: boolean
  intervalDescription: string
  percentile: string | null
  intervalDistance: number
  cumulativeDistance: number
}

export interface CompareIrpsAthleteInfoDto {
  athleteCourseId: number
  athleteId: number
  bib: string
  city: string
  compareIrpsRank: any
  finishTime: string | null
  fullName: string
  genderAbbreviated: string
  paceWithTimeCumulative: PaceWithTime | null
  raceAge: number
  state: string
  compareIrpsIntervalDtos: CompareIrpsIntervalDto[]
}

interface FinishInfo {
  finishTime: string
  paceWithTimeCumulative: PaceWithTime
}

export interface CompareIrpsIntervalDto {
  intervalName: string
  paceWithTime: PaceWithTime | null
  crossingTime: string | null
  overallRank: number | null
  genderRank: number | null
  primaryDivisionRank: number | null
}
