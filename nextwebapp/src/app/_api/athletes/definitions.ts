import { PaceWithTime } from "@/app/_components/IntervalTime"
import { LocationInfoWithRank } from "@/app/_components/LocationInfoRankings"

export interface AthleteSearchResultDto {
  id: number
  fullName: string
  age: number
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  tags: string[]
}

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
  goalTotal: number | null
}

export interface ArpDto {
  age: number
  allEventsGoal: any
  firstName: string
  fullName: string
  genderAbbreviated: string
  goals: any[]
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
