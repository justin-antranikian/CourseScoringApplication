import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchEventsRequestDto } from '../../_core/searchEventsRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { EventSearchResultComponent } from './event-search-result/event-search-result.component';
import { EventsByLocationComponentBase } from './eventsByLocationComponentBase';

@Component({
  standalone: true,
  imports: [CommonModule, EventsBreadcrumbComponent, RouterModule, SmartNavigationComponent, SmartNavigationStatesComponent, QuickSearchComponent, EventSearchResultComponent],
  selector: 'app-events-area',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsAreaComponent extends EventsByLocationComponentBase {

  constructor() {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.Area
  }

  protected override getParamKey = () => 'area'

  protected override getSearchEventsRequestDto = (location: string): SearchEventsRequestDto => {
    return new SearchEventsRequestDto(null, null, location)
  }

  protected override getDashboardInfoLocationType = (): DashboardInfoLocationType => DashboardInfoLocationType.Area

  protected override getBreadcrumbNavigationLevel = (): BreadcrumbNavigationLevel => BreadcrumbNavigationLevel.Area
}
