import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { DisplayNameWithIdDto } from 'src/app/_orchestration/displayNameWithIdDto';
import { EventsBreadcrumbResultDto } from 'src/app/_orchestration/getBreadcrumb/eventsBreadcrumbResultDto';
import { BreadcrumbComponentBase } from '../breadcrumbComponentBase';

@Component({
  selector: 'app-events-bread-crumb',
  templateUrl: './events-bread-crumb.component.html',
  styleUrls: []
})
export class EventsBreadcrumbComponent extends BreadcrumbComponentBase implements OnChanges {

  @Input('breadcrumbResult')
  public breadcrumbResult: EventsBreadcrumbResultDto

  @Input('breadcrumbLocation')
  public breadcrumbLocation: BreadcrumbLocation

  // extract as props to render in template.
  public raceSeriesDisplayWithId: DisplayNameWithIdDto
  public raceDisplayWithId: DisplayNameWithIdDto
  public courseDisplayWithId: DisplayNameWithIdDto
  public irpDisplayWithId: DisplayNameWithIdDto

  ngOnChanges(_changes: SimpleChanges) {
    this.title = this.getTitle()
    this.setLocationInfoWithUrl(this.breadcrumbResult)

    const {
      raceSeriesDisplayWithId,
      raceDisplayWithId,
      courseDisplayWithId,
      irpDisplayWithId,
    } = this.breadcrumbResult

    this.raceSeriesDisplayWithId = raceSeriesDisplayWithId
    this.raceDisplayWithId = raceDisplayWithId
    this.courseDisplayWithId = courseDisplayWithId
    this.irpDisplayWithId = irpDisplayWithId
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
