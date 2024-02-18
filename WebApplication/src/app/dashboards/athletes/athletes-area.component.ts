import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';
import { AthletesComponentBase } from './athletesComponentBase';
import { HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { AthleteSearchResultComponent } from './athlete-search-result/athlete-search-result.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { switchMap, of, forkJoin, Subscription } from 'rxjs';
import { ScoringApiService } from '../../services/scoring-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, HttpClientModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, AthleteSearchResultComponent, RouterModule, AthleteBreadcrumbComponent, NgbToastModule],
  selector: 'app-athletes-area',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesAreaComponent extends AthletesComponentBase implements OnInit, OnDestroy {

  private subscription: Subscription | null = null

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.Area
  }

  ngOnInit() {
    this.subscription = this.route.paramMap.pipe(
      switchMap((paramMap: ParamMap) => {
        const area = paramMap.get('area') as string
        const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.Area, area)
        const searchAthletesRequest = new SearchAthletesRequestDto(null, area, null, null)
        const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Area, area)
        const observables$ = [
          this.scoringApiService.getDashboardInfo(dashboardRequest),
          this.scoringApiService.getAthletesChunked(searchAthletesRequest),
          this.scoringApiService.getAthletesBreadCrumbsResult(breadcrumbRequest),
          of(area)
        ]
        return forkJoin(observables$)
      })
    ).subscribe(data => {
      this.dashboardInfoResponseDto = data[0]
      this.athleteSearchResultsChunked = data[1]
      this.athletesBreadcrumbResult = data[2]
      this.title = data[3]
    })
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
