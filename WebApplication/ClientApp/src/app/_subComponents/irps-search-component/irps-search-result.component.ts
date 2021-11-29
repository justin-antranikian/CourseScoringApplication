import { Component, Input, OnInit } from '@angular/core';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { IrpSearchResultDto } from 'src/app/_orchestration/searchIrps/irpSearchResultDto';

@Component({
  selector: 'app-irps-search-result',
  templateUrl: './irps-search-result.component.html',
  styleUrls: []
})
export class IrpsSearchResultComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('irpSearchResultDto')
  private irpSearchResultDto: IrpSearchResultDto

  // extract as props to render in template.
  public athleteCourseId: number
  public fullName: string
  public raceAge: number
  public genderAbbreviated: string
  public bib: string
  public city: string
  public state: string
  public courseName: string

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
