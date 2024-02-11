import { Component, Input, OnInit } from '@angular/core';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { map, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AthleteSearchResultDto } from '../../../_core/athleteSearchResultDto';
import { Observable } from 'rxjs';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';
import { BracketRankComponent } from '../../../_subComponents/bracket-rank/bracket-rank.component';
import { IntervalTimeComponent } from '../../../_subComponents/interval-time/interval-time.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { config } from '../../../config';

@Component({
  standalone: true,
  selector: 'athlete-search-result',
  templateUrl: './athlete-search-result.component.html',
  imports: [HttpClientModule, RouterModule, CommonModule, NgbModule, LocationInfoRankingsComponent, BracketRankComponent, IntervalTimeComponent],
  styleUrls: []
})
export class AthleteSearchResultComponent extends ComponentBaseWithRoutes implements OnInit {

  public getArpDto(athleteId: number): Observable<any> {
    return this.http.get<any>(`${config.apiUrl}/arpApi/${athleteId}`).pipe(
      map((arpDto: any): any => ({
        ...arpDto,
        results: this.mapSeriesTypeImages(arpDto.results)
      }))
    )
  }

  constructor(
    private http: HttpClient,
    private modalService: NgbModal
  ) { super() }

  @Input('athleteSearchResult')
  public athleteSearchResult!: AthleteSearchResultDto;

  // extract as props to render in template.
  public id!: number
  public fullName!: string
  public locationInfoWithRank: any

  // props for when view arp is clicked.
  public age!: number
  public genderAbbreviated!: string
  public results!: any[]
  public tags!: string[]
  public allEventsGoal: any

  ngOnInit() {
    const {
      id,
      fullName,
      locationInfoWithRank,
    } = this.athleteSearchResult

    this.id = id
    this.fullName = fullName
    this.locationInfoWithRank = locationInfoWithRank
  }

  public onViewArpClicked = (athleteInfoModal: any) => {
    const athleteId = this.athleteSearchResult.id

    const getArpDto$ = this.getArpDto(athleteId).pipe(
      tap(this.setPropsToRender)
    )

    getArpDto$.subscribe((_arpDto: any) => {
      this.modalService.open(athleteInfoModal, { size: 'xl' });
    })
  }

  /**
  * this sets the properties of the ArpDto when user clicks to view more.
  */
  private setPropsToRender = (arpDto: any) => {
    const {
      age,
      genderAbbreviated,
      results,
      tags,
      allEventsGoal,
    } = arpDto

    this.age = age
    this.genderAbbreviated = genderAbbreviated
    this.results = results
    this.tags = tags
    this.allEventsGoal = allEventsGoal
  }
}
