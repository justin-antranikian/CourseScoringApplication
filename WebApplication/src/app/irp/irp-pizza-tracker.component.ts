import { Component, Input, OnInit } from "@angular/core";
import { ComponentBaseWithRoutes } from "../_common/componentBaseWithRoutes";
import { CommonModule } from "@angular/common";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  selector: 'app-irp-pizza-tracker',
  templateUrl: './irp-pizza-tracker.component.html',
  imports: [CommonModule, NgbModule],
  styleUrls: ['./irp.component.css']
})
export class IrpPizzaTrackerComponent extends ComponentBaseWithRoutes implements OnInit {
  
  @Input('interval')
  public interval: any

  // extract as props to render in template.
  public intervalTypeImageUrl!: string;
  public intervalName!: string;
  public intervalFinished!: boolean;
  public overallRank!: number | null;
  public genderRank!: number | null;
  public primaryDivisionRank!: number | null;
  public overallCount!: number;
  public genderCount!: number;
  public primaryDivisionCount!: number;
  public overallIndicator: any
  public genderIndicator: any
  public primaryDivisionIndicator: any
  public isFullCourse!: boolean;
  public intervalDescription!: string;
  public percentile!: string | null;
  public intervalDistance!: number;
  public cumulativeDistance!: number;

  ngOnInit() {
    const {
      intervalTypeImageUrl,
      intervalName,
      intervalFinished,
      overallRank,
      genderRank,
      primaryDivisionRank,
      overallCount,
      genderCount,
      primaryDivisionCount,
      overallIndicator,
      genderIndicator,
      primaryDivisionIndicator,
      isFullCourse,
      intervalDescription,
      percentile,
      intervalDistance,
      cumulativeDistance,
    } = this.interval

    this.intervalTypeImageUrl = intervalTypeImageUrl
    this.intervalName = intervalName
    this.intervalFinished = intervalFinished
    this.overallRank = overallRank
    this.genderRank = genderRank
    this.primaryDivisionCount = primaryDivisionCount
    this.overallCount = overallCount
    this.genderCount = genderCount
    this.primaryDivisionRank = primaryDivisionRank
    this.overallIndicator = overallIndicator
    this.genderIndicator = genderIndicator
    this.primaryDivisionIndicator = primaryDivisionIndicator
    this.isFullCourse = isFullCourse
    this.intervalDescription = intervalDescription
    this.percentile = percentile
    this.intervalDistance = intervalDistance
    this.cumulativeDistance = cumulativeDistance
  }
}