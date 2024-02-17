import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../_core/breadcrumbRequestDto';
import { EventsBreadcrumbComponent } from '../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RaceSeriesCourseTabComponent } from './race-series-course-tab.component';
import { Observable } from 'rxjs';

interface RaceSeries {
  name: string
  raceSeriesImageUrl: string
  city: string
  locationInfoWithRank: any
  kickOffDate: string
  description: string
  races: any
  firstCourseId: number
  courses: any
}

@Component({
  standalone: true,
  imports: [HttpClientModule, CommonModule, RouterModule, NgbModule, RaceSeriesCourseTabComponent, EventsBreadcrumbComponent, LocationInfoRankingsComponent],
  selector: 'app-race-series-dashboard',
  templateUrl: './race-series-dashboard.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesDashboardComponent extends BreadcrumbComponent implements OnInit {

  public $raceSeries!: Observable<RaceSeries>

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    const raceSeriesId = this.getId();
    this.$raceSeries = this.getRaceSeriesDashboardDto(raceSeriesId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.ArpOrRaceSeriesDashboard, raceSeriesId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }
}
