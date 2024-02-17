import { Component, Input } from '@angular/core';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

interface Participant {
  athleteCourseId: number
  firstName: string
  courseGoalDescription: string
  personalGoalDescription: string
  trainingList: any[]
  bib: string
  fullName: string
  athleteId: number
  genderAbbreviated: string
  raceAge: number
  state: string
  city: string
}

@Component({
  standalone: true,
  selector: '[app-race-series-participant]',
  imports: [CommonModule, RouterModule, NgbModule],
  templateUrl: './race-series-participant.component.html',
  styleUrls: ['./race-series-dashboard.component.css']
})
export class RaceSeriesParticipantComponent extends ComponentBaseWithRoutes {

  @Input('participant')
  public participant!: Participant
}
