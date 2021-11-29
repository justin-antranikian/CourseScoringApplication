import { Component, Input, OnInit } from "@angular/core";
import { ComponentBaseWithRoutes } from "../_common/componentBaseWithRoutes";
import { BetweenIntervalTimeIndicator } from "../_core/enums/betweenIntervalTimeIndicator";
import { PaceWithTime } from "../_core/enums/paceWithTime";
import { IrpResultByIntervalDto } from "../_orchestration/getIrp/irpDto";

@Component({
  selector: '[app-irp-interval-result]',
  templateUrl: './irp-interval-result.component.html',
  styleUrls: ['./irp.component.css']
})
export class IrpIntervalResultComponent extends ComponentBaseWithRoutes implements OnInit {
  
  @Input('irpResultByInterval')
  public irpResultByInterval: IrpResultByIntervalDto

  // extract as props to render in template.
  public intervalName: string
  public paceWithTimeCumulative: PaceWithTime
  public paceWithTimeIntervalOnly: PaceWithTime
  public overallRank: number | null
  public genderRank: number | null
  public primaryDivisionRank: number | null
  public overallCount: number
  public genderCount: number
  public primaryDivisionCount: number
  public overallIndicator: BetweenIntervalTimeIndicator
  public genderIndicator: BetweenIntervalTimeIndicator
  public primaryDivisionIndicator: BetweenIntervalTimeIndicator
  public crossingTime: string

  ngOnInit() {
    const {
      intervalName,
      paceWithTimeCumulative,
      paceWithTimeIntervalOnly,
      overallRank,
      genderRank,
      primaryDivisionRank,
      overallCount,
      genderCount,
      primaryDivisionCount,
      overallIndicator,
      genderIndicator,
      primaryDivisionIndicator,
      crossingTime,
    } = this.irpResultByInterval

    this.intervalName = intervalName
    this.paceWithTimeCumulative = paceWithTimeCumulative
    this.paceWithTimeIntervalOnly = paceWithTimeIntervalOnly
    this.overallRank = overallRank
    this.genderRank = genderRank
    this.primaryDivisionCount = primaryDivisionCount
    this.overallCount = overallCount
    this.genderCount = genderCount
    this.primaryDivisionRank = primaryDivisionRank
    this.overallIndicator = overallIndicator
    this.genderIndicator = genderIndicator
    this.primaryDivisionIndicator = primaryDivisionIndicator
    this.crossingTime = crossingTime
  }
}