import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"
import { RaceSeriesType } from "@/app/definitions"

export interface AthleteSearchResultDto {
  id: number
  age: number
  fullName: string
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
}

export interface ArpDto {
  age: number
  firstName: string
  fullName: string
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  results: ArpResultDto[]
  wellnessGoals: string[]
  wellnessMotivationalList: string[]
  wellnessTrainingAndDiet: string[]
}

export interface ArpResultDto {
  athleteCourseId: number
  courseId: number
  courseName: string
  genderCount: number
  genderRank: number
  overallCount: number
  overallRank: number
  paceWithTimeCumulative: PaceWithTime
  primaryDivisionCount: number
  primaryDivisionRank: number
  raceId: number
  raceName: string
}

export interface AthleteCompareDto {
  id: number
  age: number
  fullName: string
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  stats: AthleteCompareStatDto[]
}

export interface AthleteCompareStatDto {
  raceSeriesType: RaceSeriesType
  actualTotal: number
  goalTotal: number | null
}
