import { Component, Input, OnInit } from '@angular/core';
import { BreadcrumbComponentBase } from '../breadcrumbComponentBase';
import { BreadcrumbLocation } from '../../../_common/breadcrumbLocation';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-events-bread-crumb',
  templateUrl: './events-bread-crumb.component.html',
  imports: [RouterModule, CommonModule],
  styleUrls: []
})
export class EventsBreadcrumbComponent extends BreadcrumbComponentBase implements OnInit {

  @Input('breadcrumbResult')
  public breadcrumbResult: any

  @Input('breadcrumbLocation')
  public breadcrumbLocation: any

  ngOnInit() {
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
        return this.breadcrumbResult.raceSeriesDisplayWithId.displayName
      }
      case BreadcrumbLocation.RaceLeaderboard: {
        return this.breadcrumbResult.raceDisplayWithId.displayName
      }
      case BreadcrumbLocation.CourseLeaderboard: {
        return this.breadcrumbResult.courseDisplayWithId.displayName
      }
      case BreadcrumbLocation.Irp: {
        return this.breadcrumbResult.irpDisplayWithId.displayName
      }
      default: {
        return null
      }
    }
  }
}
