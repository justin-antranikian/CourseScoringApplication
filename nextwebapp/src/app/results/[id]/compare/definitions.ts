import { PaceWithTime } from "@/app/_components/IntervalTime"

export interface CompareIrpsAthleteInfoDto {
  athleteId: number
  fullName: string
  raceAge: number
  genderAbbreviated: string
  bib: string
  compareIrpsRank: any
  city: string
  state: string
  athleteCourseId: number
  finishInfo?: FinishInfo
  compareIrpsIntervalDtos: CompareIrpsIntervalDto[]
}

interface FinishInfo {
  finishTime: string
  paceWithTimeCumulative: PaceWithTime
}

interface CompareIrpsIntervalDto {
  intervalName: string
  paceWithTime: PaceWithTime | null
  crossingTime: string | null
  overallRank: number | null
  genderRank: number | null
  primaryDivisionRank: number | null
}
