import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"

export interface CompareAthletesAthleteInfoDto {
  locationInfoWithRank: LocationInfoWithRank
  fullName: string
  age: number
  genderAbbreviated: string
  stats: CompareAthletesStat[]
}

export interface CompareAthletesResult {
  athleteCourseId: number
  raceId: number
  raceName: string
  courseId: number
  courseName: string
  paceWithTime: PaceWithTime
  overallRank: number
  genderRank: number
  divisionRank: number
}

export interface CompareAthletesStat {
  raceSeriesTypeName: string
  actualTotal: number
}
