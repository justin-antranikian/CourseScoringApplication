import { LocationInfoWithRank } from "../locationInfoWithRank";
import { CourseInformationEntry } from "./courseInformationEntry";
import { DisplayNameWithIdDto } from "../displayNameWithIdDto";
import { IRaceSeriesType } from "../../_common/IRaceSeriesType";

export interface RaceSeriesDashboardDto extends IRaceSeriesType {
  name: string
  description: string
  kickOffDate: string
  races: PastRaceDto[]
  courses: RaceSeriesDashboardCourseDto[]
  locationInfoWithRank: LocationInfoWithRank
  upcomingRaceId: number
  firstCourseId: number
}

export interface RaceSeriesDashboardCourseDto extends DisplayNameWithIdDto {
  descriptionEntries: CourseInformationEntry[]
  promotionalEntries: CourseInformationEntry[]
  howToPrepareEntries: CourseInformationEntry[]
  participants: RaceSeriesDashboardParticipantDto[]
}

export interface RaceSeriesDashboardParticipantDto {
  athleteId: number
  athleteCourseId: number
  fullName: string
  firstName: string
  bib: string
  state: string
  city: string
  raceAge: number
  genderAbbreviated: string
  courseGoalDescription: string
  personalGoalDescription: string
  trainingList: string[]
}

export interface PastRaceDto extends DisplayNameWithIdDto {
  kickOffDate: string
}