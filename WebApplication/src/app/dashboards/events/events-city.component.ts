import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchEventsRequestDto } from '../../_core/searchEventsRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { EventSearchResultComponent } from './event-search-result/event-search-result.component';
import { EventsLocationBasedComponentBase } from './eventsLocationBasedComponentBase';

@Component({
  standalone: true,
  imports: [CommonModule, EventsBreadcrumbComponent, RouterModule, SmartNavigationComponent, SmartNavigationStatesComponent, QuickSearchComponent, EventSearchResultComponent],
  selector: 'app-events-city',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsCityComponent extends EventsLocationBasedComponentBase {

  constructor() {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.City
  }

  override getParamKey(): any {
    return 'city'
  }

  override getDashboardInfoRequestDto(location: string): DashboardInfoRequestDto {
    return new DashboardInfoRequestDto(DashboardInfoType.Events, DashboardInfoLocationType.City, location)
  }

  override getSearchEventsRequestDto(location: string): SearchEventsRequestDto {
    return new SearchEventsRequestDto(null, null, null, location)
  }

  override getBreadcrumbRequestDto(location: string): BreadcrumbRequestDto {
    return new BreadcrumbRequestDto(BreadcrumbNavigationLevel.City, location)
  }
}
