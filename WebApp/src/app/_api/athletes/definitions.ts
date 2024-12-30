import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"
import { RaceSeriesType } from "@/app/definitions"

export interface AthleteSearchResultDto {
  id: number
  fullName: string
  age: number
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
}

export interface CompareAthletesAthleteInfoDto {
  id: number
  age: number
  fullName: string
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  stats: CompareAthletesStat[]
}

export interface CompareAthletesStat {
  raceSeriesType: RaceSeriesType
  actualTotal: number
  goalTotal: number | null
}

export interface ArpDto {
  age: number
  firstName: string
  fullName: string
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  results: ArpResultDto[]
  tags: string[]
  wellnessTrainingAndDiet: AthleteWellnessEntryDto[]
  wellnessGoals: AthleteWellnessEntryDto[]
  wellnessGearList: AthleteWellnessEntryDto[]
  wellnessMotivationalList: AthleteWellnessEntryDto[]
}

export interface ArpResultDto {
  athleteCourseId: number
  raceId: number
  raceName: string
  raceSeriesType: any
  courseId: number
  courseName: string
  state: string
  city: string
  overallRank: number
  genderRank: number
  primaryDivisionRank: number
  overallCount: number
  genderCount: number
  primaryDivisionCount: number
  paceWithTimeCumulative: PaceWithTime
}

export interface AthleteWellnessEntryDto {
  athleteWellnessType: any
  description: string
}
