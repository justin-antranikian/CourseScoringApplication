import { LocationInfoWithRank } from "../locationInfoWithRank";
import { DisplayNameWithIdDto } from "../displayNameWithIdDto";
import { PaceWithTime } from "../../_core/enums/paceWithTime";
import { IRaceSeriesType } from "../../_common/IRaceSeriesType";

export interface ArpDto {
  fullName: string
  firstName: string
  age: number
  genderAbbreviated: string
  locationInfoWithRank: LocationInfoWithRank
  results: ArpResultDto[]
  tags: string[]
  rivals: DisplayNameWithIdDto[]
  followings: DisplayNameWithIdDto[]
  allEventsGoal: ArpGoalDto
  goals: ArpGoalDto[]
  wellnessTrainingAndDiet: AthleteWellnessEntry[]
  wellnessGoals: AthleteWellnessEntry[]
  wellnessGearList: AthleteWellnessEntry[]
  wellnessMotivationalList: AthleteWellnessEntry[]
}

export interface ArpGoalDto {
  raceSeriesTypeName: string
  goalTotal: number
  actualTotal: number
  totalDistance: number
  percentComplete: number
  courses: CourseGoalArpDto[]
}

export interface AthleteWellnessEntry {
  athleteId: number
  athleteWellnessType: AthleteWellnessType
  description: string
}

export interface ArpResultDto extends IRaceSeriesType {
  athleteCourseId: number
  overallRank: number
  overallCount: number
  genderRank: number
  genderCount: number
  primaryDivisionRank: number
  primaryDivisionCount: number
  paceWithTimeCumulative: PaceWithTime
  state: string
  city: string
  raceId: number
  raceName: string
  courseId: number
  courseName: string
}

export interface CourseGoalArpDto {
  courseId: number
  courseName: string
  raceId: number
  raceName: string
  raceSeriesState: string
  raceSeriesCity: string
  raceSeriesDescription: string
}

export enum AthleteWellnessType {
  Goal,
  Training,
  Gear,
  Diet,
  Motivational
}