import { PaceWithTime } from "@/app/_components/IntervalTime"

export interface ArpDto {
  age: number
  allEventsGoal: any
  firstName: string
  fullName: string
  genderAbbreviated: string
  goals: any[]
  locationInfoWithRank: any
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
