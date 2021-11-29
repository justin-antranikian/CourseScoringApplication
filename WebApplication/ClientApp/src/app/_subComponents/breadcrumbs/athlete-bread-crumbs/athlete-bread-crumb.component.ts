import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { BreadcrumbResultDto } from 'src/app/_orchestration/getBreadcrumb/breadcrumbResultDto';
import { BreadcrumbComponentBase } from '../breadcrumbComponentBase';

@Component({
  selector: 'app-athlete-bread-crumb',
  templateUrl: './athlete-bread-crumb.component.html',
  styleUrls: []
})
export class AthleteBreadcrumbComponent extends BreadcrumbComponentBase implements OnChanges {

  @Input('breadcrumbResult')
  public breadcrumbResult: BreadcrumbResultDto

  @Input('breadcrumbLocation')
  public breadcrumbLocation: BreadcrumbLocation

  /** used on the Arp page since it already has the full name and location info. */
  @Input('titleOverride')
  public titleOverride: string | undefined

  ngOnChanges(_changes: SimpleChanges): void {
    this.title = this.getTitle()
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
        return this.titleOverride // Arp passes the title in directly.
      }
      default: {
        return null
      }
    }
  }
}
