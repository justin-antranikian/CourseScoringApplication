import { AthleteResultBase } from "../athleteResultBase";

export interface LeaderboardResultDto extends AthleteResultBase {
  athleteCourseId: number
  overallRank: number
  genderRank: number
  divisionRank: number
}
