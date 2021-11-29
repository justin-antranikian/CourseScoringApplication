import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiRequestService } from '../_services/api-request.service';
import { PastRaceDto, RaceSeriesDashboardCourseDto, RaceSeriesDashboardDto } from '../_orchestration/getRaceSeriesDashboard/raceSeriesDashboardDto';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { LocationInfoWithRank } from '../_orchestration/locationInfoWithRank';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-race-series-dashboard',
  templateUrl: './race-series-dashboard.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesDashboardComponent extends BreadcrumbComponent implements OnInit {

  // extract as props to render in template.
  public raceSeriesImageUrl: string
  public name: string
  public description: string
  public kickOffDate: string
  public locationInfoWithRank: LocationInfoWithRank
  public races: PastRaceDto[]
  public upcomingRaceId: number
  public firstCourseId: number
  public courses: RaceSeriesDashboardCourseDto[]

  public dataLoaded = false

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    const raceSeriesId = this.getId();
    this.apiService.getRaceSeriesDashboardDto(raceSeriesId).subscribe(this.setPropsToRender)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.ArpOrRaceSeriesDashboard, raceSeriesId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (raceSeriesDashboardDto: RaceSeriesDashboardDto) => {
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
