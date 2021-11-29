import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { ArpDto, ArpGoalDto, ArpResultDto } from 'src/app/_orchestration/getArp/arpDto';
import { AthleteSearchResultDto } from 'src/app/_orchestration/searchAthletes/athleteSearchResultDto';
import { LocationInfoWithRank } from 'src/app/_orchestration/locationInfoWithRank';
import { ApiRequestService } from 'src/app/_services/api-request.service';

@Component({
  selector: 'athlete-search-result',
  templateUrl: './athlete-search-result.component.html',
  styleUrls: []
})
export class AthleteSearchResultComponent extends ComponentBaseWithRoutes implements OnInit {

  constructor(
    private apiService: ApiRequestService,
    private modalService: NgbModal
  ) { super() }

  @Input('athleteSearchResult')
  public athleteSearchResult: AthleteSearchResultDto;

  // extract as props to render in template.
  public id: number
  public fullName: string
  public locationInfoWithRank: LocationInfoWithRank

  // props for when view arp is clicked.
  public age: number
  public genderAbbreviated: string
  public results: ArpResultDto[]
  public tags: string[]
  public allEventsGoal: ArpGoalDto

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

  public onViewArpClicked = (athleteInfoModal) => {
    const athleteId = this.athleteSearchResult.id

    const getArpDto$ = this.apiService.getArpDto(athleteId).pipe(
      tap(this.setPropsToRender)
    )

    getArpDto$.subscribe((_arpDto: ArpDto) => {
      this.modalService.open(athleteInfoModal, { size: 'xl' });
    })
  }

  /**
  * this sets the properties of the ArpDto when user clicks to view more.
  */
  private setPropsToRender = (arpDto: ArpDto) => {
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
