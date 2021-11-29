import { PaceWithTime } from "../../_core/enums/paceWithTime";
import { AthleteResultBase } from "../athleteResultBase";
import { BetweenIntervalTimeIndicator } from "../../_core/enums/betweenIntervalTimeIndicator";
import { IRaceSeriesType } from "../../_common/IRaceSeriesType";
import { IIntervalType } from "../../_common/IIntervalName";
import { LocationInfoWithRank } from "../locationInfoWithRank";

export interface IrpDto extends AthleteResultBase, IRaceSeriesType {
  firstName: string
  raceName: string
  courseId: number
  courseName: string
  courseDistance: number
  timeZoneAbbreviated: string
  finishTime: string | null
  courseDate: string
  courseTime: string
  tags: string[]
  raceSeriesCity: string
  raceSeriesState: string
  raceSeriesDescription: string
  trainingList: string[]
  courseGoalDescription: string
  personalGoalDescription: string
  locationInfoWithRank: LocationInfoWithRank
  bracketResults: IrpResultByBracketDto[]
  intervalResults: IrpResultByIntervalDto[]
}

export interface IrpResultByBracketDto {
  id: number
  name: string
  rank: number
  totalRacers: number
  percentile: string
  didBetterThenPercent: number
  didWorseThenPercent: number
  averagePaceWithTime: PaceWithTime
  fastestPaceWithTime: PaceWithTime
  slowestPaceWithTime: PaceWithTime
}

export interface IrpResultByIntervalDto extends IIntervalType {
  intervalName: string
  intervalFinished: boolean
  paceWithTimeCumulative: PaceWithTime
  paceWithTimeIntervalOnly: PaceWithTime
  overallRank: number | null
  genderRank: number | null
  primaryDivisionRank: number | null
  overallCount: number
  genderCount: number
  primaryDivisionCount: number
  overallIndicator: BetweenIntervalTimeIndicator
  genderIndicator: BetweenIntervalTimeIndicator
  primaryDivisionIndicator: BetweenIntervalTimeIndicator
  crossingTime: string
  isFullCourse: boolean
  intervalDescription: string
  percentile: string | null
  intervalDistance: number
  cumulativeDistance: number
}
