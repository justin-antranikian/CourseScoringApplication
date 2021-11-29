import { Component, Input, OnInit } from '@angular/core';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { CourseInformationEntry } from '../_orchestration/getRaceSeriesDashboard/courseInformationEntry';
import { RaceSeriesDashboardCourseDto, RaceSeriesDashboardParticipantDto } from '../_orchestration/getRaceSeriesDashboard/raceSeriesDashboardDto';

@Component({
  selector: 'race-series-course-tab',
  templateUrl: './race-series-course-tab.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesCourseTabComponent extends ComponentBaseWithRoutes implements OnInit {
  
  @Input('course')
  public course: RaceSeriesDashboardCourseDto

  // extract as props to render in template.
  public id: number
  public descriptionEntries: CourseInformationEntry[]
  public promotionalEntries: CourseInformationEntry[]
  public howToPrepareEntries: CourseInformationEntry[]
  public participants: RaceSeriesDashboardParticipantDto[]

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
