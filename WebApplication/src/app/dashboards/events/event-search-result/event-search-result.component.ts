import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';
import { IRaceSeriesType } from '../../../_common/IRaceSeriesType';

export interface EventSearchResultDto extends IRaceSeriesType {
  id: number
  name: string
  raceSeriesTypeName: string
  upcomingRaceId: number
  kickOffDate: string
  kickOffTime: string
  description: string
  courses: any[]
  locationInfoWithRank: any
  rating: number
}

@Component({
  standalone: true,
  selector: 'app-event-search-result',
  templateUrl: './event-search-result.component.html',
  styleUrls: ['./event-search-result.component.css'],
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent, NgbModule]
})
export class EventSearchResultComponent extends ComponentBaseWithRoutes {

  @Input('eventSearchResult')
  public eventSearchResult!: EventSearchResultDto

  @Output()
  public dataEvent: EventEmitter<number> = new EventEmitter<number>();

  public onQuickViewClicked = () => {
    this.dataEvent.emit(this.eventSearchResult.upcomingRaceId);
  }
}
