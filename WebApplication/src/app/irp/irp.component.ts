import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IrpIntervalResultComponent } from './irp-interval-result.component';
import { BracketRankComponent } from '../_subComponents/bracket-rank/bracket-rank.component';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../_core/breadcrumbRequestDto';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { EventsBreadcrumbComponent } from '../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { IrpPizzaTrackerComponent } from './irp-pizza-tracker.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ScoringApiService } from '../services/scoring-api.service';

@Component({
  standalone: true,
  templateUrl: './irp.component.html',
  imports: [IrpIntervalResultComponent, BracketRankComponent, EventsBreadcrumbComponent, LocationInfoRankingsComponent, IrpPizzaTrackerComponent, NgbModule, CommonModule, RouterModule],
  styleUrls: ['./irp.component.css']
})
export class IrpComponent extends BreadcrumbComponent implements OnInit {

  public $irp!: Observable<any>
  public eventsBreadcrumbResult$!: Observable<any>
  
  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.Irp
  }

  ngOnInit() {
    const athleteCourseId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.$irp = this.scoringApiService.getIrpDto(athleteCourseId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, athleteCourseId.toString())
    this.eventsBreadcrumbResult$ = this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest)
  }
}
