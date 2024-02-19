import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EventSearchResultDto } from '../../../_core/eventSearchResultDto';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';

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
