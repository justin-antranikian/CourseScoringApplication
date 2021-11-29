import { AthleteResultBase } from "../athleteResultBase";

export interface GetCompetetorsForIrpDto {
  fasterAthletes: IrpCompetetorDto[]
  slowerAthletes: IrpCompetetorDto[]
}

export interface IrpCompetetorDto extends AthleteResultBase {
  athleteCourseId: number
  firstName: string
}