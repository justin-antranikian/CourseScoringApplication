import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../_core/breadcrumbRequestDto';
import { EventsBreadcrumbComponent } from '../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RaceSeriesCourseTabComponent } from './race-series-course-tab.component';
import { Observable } from 'rxjs';
import { ScoringApiService } from '../services/scoring-api.service';

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

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    const raceSeriesId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.$raceSeries = this.scoringApiService.getRaceSeriesDashboardDto(raceSeriesId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.ArpOrRaceSeriesDashboard, raceSeriesId.toString())
    this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest).subscribe(result => {
      this.eventsBreadcrumbResult = result
    })
  }
}
