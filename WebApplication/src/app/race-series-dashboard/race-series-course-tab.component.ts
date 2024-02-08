import { Component, Input, OnInit } from '@angular/core';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RaceSeriesParticipantComponent } from './race-series-participant.component';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, RaceSeriesParticipantComponent],
  selector: 'race-series-course-tab',
  templateUrl: './race-series-course-tab.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesCourseTabComponent extends ComponentBaseWithRoutes implements OnInit {
  
  @Input('course')
  public course: any

  // extract as props to render in template.
  public id!: number
  public descriptionEntries!: any[]
  public promotionalEntries!: any[]
  public howToPrepareEntries!: any[]
  public participants!: any[]

  ngOnInit() {
    const {
      id,
      descriptionEntries,
      promotionalEntries,
      howToPrepareEntries,
      participants
    } = this.course

    this.id = id
    this.descriptionEntries = descriptionEntries
    this.promotionalEntries = promotionalEntries
    this.howToPrepareEntries = howToPrepareEntries
    this.participants = participants
  }
}
