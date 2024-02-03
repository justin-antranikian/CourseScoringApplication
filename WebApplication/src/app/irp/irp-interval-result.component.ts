import { Component, Input, OnInit } from "@angular/core";
import { PaceWithTime } from "../_core/enums/paceWithTime";
import { BracketRankComponent } from "../_subComponents/bracket-rank/bracket-rank.component";
import { IntervalTimeComponent } from "../_subComponents/interval-time/interval-time.component";

@Component({
  standalone: true,
  selector: '[app-irp-interval-result]',
  imports: [BracketRankComponent, IntervalTimeComponent],
  templateUrl: './irp-interval-result.component.html',
  styleUrls: ['./irp.component.css']
})
export class IrpIntervalResultComponent implements OnInit {
  
  @Input('irpResultByInterval')
  public irpResultByInterval: any

  // extract as props to render in template.
  public intervalName!: string;
  public paceWithTimeCumulative!: PaceWithTime;
  public paceWithTimeIntervalOnly!: PaceWithTime;
  public overallRank!: number | null;
  public genderRank!: number | null;
  public primaryDivisionRank!: number | null;
  public overallCount!: number;
  public genderCount!: number;
  public primaryDivisionCount!: number;
  public overallIndicator: any
  public genderIndicator: any
  public primaryDivisionIndicator: any
  public crossingTime!: string;

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