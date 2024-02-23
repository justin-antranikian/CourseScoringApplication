import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { AthletesByLocationComponentBase } from './athletesByLocationComponentBase';

@Component({
  standalone: true,
  imports: [CommonModule, HttpClientModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, RouterModule, AthleteBreadcrumbComponent, NgbToastModule, LocationInfoRankingsComponent],
  selector: 'app-athletes-city',
  templateUrl: './athletes.component.html',
  styleUrl: './athletes.component.css',
})
export class AthletesCityComponent extends AthletesByLocationComponentBase {

  constructor() {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.City
  }

  protected override getParamKey = () => 'city'

  protected override getSearchAthletesRequestDto = (location: string): SearchAthletesRequestDto => {
    return new SearchAthletesRequestDto(null, null, location, null)
  }

  protected override getDashboardInfoLocationType = (): DashboardInfoLocationType => DashboardInfoLocationType.City

  protected override getBreadcrumbNavigationLevel = (): BreadcrumbNavigationLevel => BreadcrumbNavigationLevel.City
}
