import { Component, Input, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../location-info-rankings/location-info-rankings.component';
import { BreadcrumbLocation } from '../../../_common/breadcrumbLocation';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';

@Component({
  standalone: true,
  selector: 'app-athlete-bread-crumb',
  templateUrl: './athlete-bread-crumb.component.html',
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent],
  styleUrls: []
})
export class AthleteBreadcrumbComponent extends ComponentBaseWithRoutes {

  public breadcrumbAll = BreadcrumbLocation.All
  public breadcrumbState = BreadcrumbLocation.State
  public breadcrumbArea = BreadcrumbLocation.Area
  public breadcrumbCity = BreadcrumbLocation.City
  public breadcrumbArp = BreadcrumbLocation.Arp

  @Input('breadcrumbResult')
  public breadcrumbResult: any

  @Input('breadcrumbLocation')
  public breadcrumbLocation: any

  /** used on the Arp page since it already has the full name and location info. */
  @Input('titleOverride')
  public titleOverride: string | undefined

  public title = computed(() => this.getTitle())

  private getTitle = (): string | null => {

    switch (this.breadcrumbLocation) {
      case BreadcrumbLocation.State: {
        return this.breadcrumbResult.locationInfoWithUrl.state
      }
      case BreadcrumbLocation.Area: {
        return this.breadcrumbResult.locationInfoWithUrl.area
      }
      case BreadcrumbLocation.City: {
        return this.breadcrumbResult.locationInfoWithUrl.city
      }
      case BreadcrumbLocation.Arp: {
        return this.titleOverride as any // Arp passes the title in directly.
      }
      default: {
        return null
      }
    }
  }
}
