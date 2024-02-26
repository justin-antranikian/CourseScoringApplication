import { PaceWithTime } from "../_core/paceWithTime";

export interface LeaderboardResultDto {
  athleteCourseId: number
  athleteId: number
  bib: string
  divisionRank: number
  genderRank: number
  genderAbbreviated: string
  fullName: string
  overallRank: number
  raceAge: number
  paceWithTimeCumulative: PaceWithTime
}
