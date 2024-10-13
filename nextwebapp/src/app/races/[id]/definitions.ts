import { LeaderboardResultDto } from "@/app/courses/[id]/definitions"

export interface RaceLeaderboardDto {
  leaderboards: RaceLeaderboardByCourseDto[]
  locationInfoWithRank: any
  raceKickOffDate: string
  raceName: string
  raceSeriesDescription: string
}

export interface RaceLeaderboardByCourseDto {
  courseId: number
  courseName: string
  sortOrder: number
  highestIntervalName: string
  intervalType: any
  results: LeaderboardResultDto[]
}
