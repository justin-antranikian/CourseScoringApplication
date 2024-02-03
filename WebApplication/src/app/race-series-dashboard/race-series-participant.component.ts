import { Component, Input, OnInit } from '@angular/core';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  selector: '[app-race-series-participant]',
  imports: [CommonModule, RouterModule, NgbModule],
  templateUrl: './race-series-participant.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesParticipantComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('participant')
  public participant: any

  // extract as props to render in template.
  public athleteId!: number
  public athleteCourseId!: number
  public firstName!: string
  public fullName!: string
  public bib!: string
  public state!: string
  public city!: string
  public raceAge!: number
  public genderAbbreviated!: string
  public courseGoalDescription!: string
  public personalGoalDescription!: string
  public trainingList!: string[]

  ngOnInit() {
    const {
      athleteId,
      athleteCourseId,
      firstName,
      fullName,
      bib,
      state,
      city,
      raceAge,
      genderAbbreviated,
      courseGoalDescription,
      personalGoalDescription,
      trainingList,
    } = this.participant

    this.athleteId = athleteId
    this.athleteCourseId = athleteCourseId
    this.firstName = firstName
    this.fullName = fullName
    this.bib = bib
    this.state = state
    this.city = city
    this.raceAge = raceAge
    this.genderAbbreviated = genderAbbreviated
    this.courseGoalDescription = courseGoalDescription
    this.personalGoalDescription = personalGoalDescription
    this.trainingList = trainingList
  }
}
