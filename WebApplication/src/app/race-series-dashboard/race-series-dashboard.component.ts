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

@Component({
  standalone: true,
  imports: [HttpClientModule, CommonModule, RouterModule, NgbModule, RaceSeriesCourseTabComponent, EventsBreadcrumbComponent, LocationInfoRankingsComponent],
  selector: 'app-race-series-dashboard',
  templateUrl: './race-series-dashboard.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesDashboardComponent extends BreadcrumbComponent implements OnInit {

  // extract as props to render in template.
  public raceSeriesImageUrl!: string
  public name!: string
  public description!: string
  public kickOffDate!: string
  public locationInfoWithRank: any
  public races!: any[]
  public upcomingRaceId!: number
  public firstCourseId!: number
  public courses!: any[]

  public dataLoaded = false

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    const raceSeriesId = this.getId();
    this.getRaceSeriesDashboardDto(raceSeriesId).subscribe(this.setPropsToRender)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.ArpOrRaceSeriesDashboard, raceSeriesId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (raceSeriesDashboardDto: any) => {
    const {
      raceSeriesImageUrl,
      name,
      description,
      kickOffDate,
      locationInfoWithRank,
      races,
      upcomingRaceId,
      firstCourseId,
      courses,
    } = raceSeriesDashboardDto

    this.raceSeriesImageUrl = raceSeriesImageUrl
    this.name = name
    this.description = description
    this.kickOffDate = kickOffDate
    this.locationInfoWithRank = locationInfoWithRank
    this.races = races
    this.upcomingRaceId = upcomingRaceId
    this.firstCourseId = firstCourseId
    this.courses = courses

    this.dataLoaded = true
  }
}
