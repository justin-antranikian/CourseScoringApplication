import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { EventsLocationBasedComponentBase } from './athletesLocationBasedComponentBase';

@Component({
  standalone: true,
  imports: [CommonModule, HttpClientModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, RouterModule, AthleteBreadcrumbComponent, NgbToastModule, LocationInfoRankingsComponent],
  selector: 'app-athletes-state',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesStateComponent extends EventsLocationBasedComponentBase {

  constructor() {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.State
  }

  override getParamKey(): string {
    return 'state'
  }

  override getDashboardInfoRequestDto(location: string): DashboardInfoRequestDto {
    return new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.State, location)
  }

  override getSearchAthletesRequestDto(location: string): SearchAthletesRequestDto {
    return new SearchAthletesRequestDto(location, null, null, null)
  }

  override getBreadcrumbRequestDto(location: string): BreadcrumbRequestDto {
    return new BreadcrumbRequestDto(BreadcrumbNavigationLevel.State, location)
  }

  // override ngOnInit() {
  //   super.ngOnInit()

  //   this.subscription = this.route.paramMap.pipe(
  //     switchMap((paramMap: ParamMap) => {
  //       const state = paramMap.get('state') as string
  //       const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.State, state)
  //       const searchAthletesRequest = new SearchAthletesRequestDto(this.title, null, null, null)
  //       const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.State, state)
  //       const observables$ = [
  //         this.scoringApiService.getDashboardInfo(dashboardRequest),
  //         this.scoringApiService.getAthletesChunked(searchAthletesRequest),
  //         this.scoringApiService.getAthletesBreadCrumbsResult(breadcrumbRequest),
  //         of(state)
  //       ]
  //       return forkJoin(observables$)
  //     })
  //   ).subscribe(data => {
  //     this.dashboardInfoResponseDto = data[0]
  //     this.athleteSearchResultsChunked = data[1]
  //     this.athletesBreadcrumbResult = data[2]
  //     this.title = data[3]
  //   })
  // }

  // ngOnDestroy() {
  //   this.subscription?.unsubscribe();
  // }
}
