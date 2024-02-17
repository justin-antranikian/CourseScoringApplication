import { Component, Input } from '@angular/core';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RaceSeriesParticipantComponent } from './race-series-participant.component';
import { RouterModule } from '@angular/router';

interface Course {
  id: number
  descriptionEntries: any[]
  promotionalEntries: any[]
  howToPrepareEntries: any[]
  participants: any[]
}

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, RaceSeriesParticipantComponent],
  selector: 'race-series-course-tab',
  templateUrl: './race-series-course-tab.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesCourseTabComponent extends ComponentBaseWithRoutes {

  @Input('course')
  public course!: Course
}
