import { Component, Input, OnInit } from '@angular/core';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { IrpSearchResultDto } from './IrpSearchResultDto';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-irps-search-result',
  templateUrl: './irps-search-result.component.html',
  imports: [CommonModule, RouterModule],
  styleUrls: []
})
export class IrpsSearchResultComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('irpSearchResultDto')
  public irpSearchResultDto!: IrpSearchResultDto

  // extract as props to render in template.
  public athleteCourseId!: number
  public fullName!: string
  public raceAge!: number
  public genderAbbreviated!: string
  public bib!: string
  public city!: string
  public state!: string
  public courseName!: string

  ngOnInit() {
    const {
      athleteCourseId,
      fullName,
      raceAge,
      genderAbbreviated,
      bib,
      city,
      state,
      courseName
    } = this.irpSearchResultDto

    this.athleteCourseId = athleteCourseId
    this.fullName = fullName
    this.raceAge = raceAge
    this.genderAbbreviated = genderAbbreviated
    this.bib = bib
    this.city = city
    this.state = state
    this.courseName = courseName
  }
}