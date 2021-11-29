import { PaceWithTime } from "../_core/enums/paceWithTime";

export interface AthleteResultBase {
  athleteId: number
  fullName: string
  raceAge: number
  genderAbbreviated: string
  bib: string
  paceWithTimeCumulative: PaceWithTime
}
