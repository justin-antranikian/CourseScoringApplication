import { PaceWithTime } from "@/app/_components/IntervalTime"

export interface Irp {
  athleteId: number
  bib: string
  bracketResults: any[]
  courseGoalDescription: string
  finishTime: string | null
  firstName: string
  fullName: string
  genderAbbreviated: string
  intervalResults: IrpResultByIntervalDto[]
  locationInfoWithRank: any
  paceWithTimeCumulative: PaceWithTime
  personalGoalDescription: string
  raceAge: number
  tags: string[]
  timeZoneAbbreviated: string
  trainingList: string[]
}

export interface IrpResultByIntervalDto {
  intervalName: string;
  intervalType: any;
  intervalFinished: boolean;
  paceWithTimeCumulative: PaceWithTime;
  paceWithTimeIntervalOnly: PaceWithTime;
  overallRank: number | null;
  genderRank: number | null;
  primaryDivisionRank: number | null;
  overallCount: number;
  genderCount: number;
  primaryDivisionCount: number;
  overallIndicator: any;
  genderIndicator: any;
  primaryDivisionIndicator: any;
  crossingTime: string | null;
  isFullCourse: boolean;
  intervalDescription: string;
  percentile: string | null;
  intervalDistance: number;
  cumulativeDistance: number;
}