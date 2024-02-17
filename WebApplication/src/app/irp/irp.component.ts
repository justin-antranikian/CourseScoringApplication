import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
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
  providers: [HttpClient],
  styleUrls: ['./irp.component.css']
})
export class IrpComponent extends BreadcrumbComponent implements OnInit {

  public $irp!: Observable<any>

  constructor(route: ActivatedRoute, http: HttpClient, private scoringApiService: ScoringApiService) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.Irp
  }

  ngOnInit() {
    const athleteCourseId = this.getId()
    this.$irp = this.scoringApiService.getIrpDto(athleteCourseId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, athleteCourseId.toString())

    this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest).subscribe(result => {
      this.eventsBreadcrumbResult = result
    })
  }
}
