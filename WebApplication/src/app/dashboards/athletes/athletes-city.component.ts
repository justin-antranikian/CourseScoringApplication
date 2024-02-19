import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { EventsLocationBasedComponentBase } from './athletesLocationBasedComponentBase';

@Component({
  standalone: true,
  imports: [CommonModule, HttpClientModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, RouterModule, AthleteBreadcrumbComponent, NgbToastModule, LocationInfoRankingsComponent],
  selector: 'app-athletes-city',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesCityComponent extends EventsLocationBasedComponentBase {

  constructor() {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.City
  }

  override getParamKey(): string {
    return 'city'
  }

  override getDashboardInfoRequestDto(location: string): DashboardInfoRequestDto {
    return new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.City, location)
  }

  override getSearchAthletesRequestDto(location: string): SearchAthletesRequestDto {
    return new SearchAthletesRequestDto(null, null, location, null)
  }

  override getBreadcrumbRequestDto(location: string): BreadcrumbRequestDto {
    return new BreadcrumbRequestDto(BreadcrumbNavigationLevel.City, location)
  }
}
