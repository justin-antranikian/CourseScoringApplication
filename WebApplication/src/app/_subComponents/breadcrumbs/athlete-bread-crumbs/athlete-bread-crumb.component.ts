import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BreadcrumbComponentBase } from '../breadcrumbComponentBase';
import { BreadcrumbLocation } from '../../../_common/breadcrumbLocation';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../location-info-rankings/location-info-rankings.component';

@Component({
  standalone: true,
  selector: 'app-athlete-bread-crumb',
  templateUrl: './athlete-bread-crumb.component.html',
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent],
  styleUrls: []
})
export class AthleteBreadcrumbComponent extends BreadcrumbComponentBase implements OnChanges {

  @Input('breadcrumbResult')
  public breadcrumbResult: any

  @Input('breadcrumbLocation')
  public breadcrumbLocation: any

  /** used on the Arp page since it already has the full name and location info. */
  @Input('titleOverride')
  public titleOverride: string | undefined

  ngOnChanges(_changes: SimpleChanges): void {
    this.title = this.getTitle() as any
    this.setLocationInfoWithUrl(this.breadcrumbResult)
  }

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
      case BreadcrumbLocation.RaceSeriesOrArp: {
        return this.titleOverride as any // Arp passes the title in directly.
      }
      default: {
        return null
      }
    }
  }
}
