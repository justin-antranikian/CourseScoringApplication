import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiRequestService } from '../_services/api-request.service';
import { ArpDto, ArpGoalDto, ArpResultDto, AthleteWellnessEntry } from '../_orchestration/getArp/arpDto';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { ChartOptionsForArp } from './chartOptionsForArp';
import { DisplayNameWithIdDto } from '../_orchestration/displayNameWithIdDto';
import { LocationInfoWithRank } from '../_orchestration/locationInfoWithRank';
import { BreadcrumbsApiRequestService } from '../_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-arp',
  templateUrl: './arp.component.html',
  styleUrls: []
})
export class ArpComponent extends BreadcrumbComponent implements OnInit {

  public chartOptions = new ChartOptionsForArp()
  public selectedGoal: ArpGoalDto
  public arpLoaded: boolean = false

  // extract as props to render in template.
  public fullName: string
  public firstName: string
  public age: number
  public genderAbbreviated: string
  public locationInfoWithRank: LocationInfoWithRank
  public results: ArpResultDto[]
  public tags: string[]
  public rivals: DisplayNameWithIdDto[]
  public followings: DisplayNameWithIdDto[]
  public goals: ArpGoalDto[]
  public wellnessTrainingAndDiet: AthleteWellnessEntry[]
  public wellnessGoals: AthleteWellnessEntry[]
  public wellnessGearList: AthleteWellnessEntry[]
  public wellnessMotivationalList: AthleteWellnessEntry[]

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService, private modalService: NgbModal) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.arpLoaded = false
      this.initData()
    });
  }

  private initData = () => {
    const athleteId = this.getId()
    this.apiService.getArpDto(athleteId).subscribe(this.handleGetArpResponse);
  }

  private handleGetArpResponse = (arpDto: ArpDto) => {
    const { goals, locationInfoWithRank } = arpDto
    this.athletesBreadcrumbResult = { locationInfoWithUrl: locationInfoWithRank }

    this.chartOptions.setLabels(goals.map(oo => oo.raceSeriesTypeName))
    this.chartOptions.setData(goals.map(oo => oo.totalDistance))

    this.setPropsToRender(arpDto)
    this.arpLoaded = true
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (arpDto: ArpDto) => {
    const {
      fullName,
      firstName,
      age,
      genderAbbreviated,
      locationInfoWithRank,
      results,
      tags,
      rivals,
      followings,
      goals,
      wellnessTrainingAndDiet,
      wellnessGoals,
      wellnessGearList,
      wellnessMotivationalList,
    } = arpDto

    this.fullName = fullName
    this.firstName = firstName
    this.age = age
    this.genderAbbreviated = genderAbbreviated
    this.locationInfoWithRank = locationInfoWithRank
    this.results = results
    this.tags = tags
    this.rivals = rivals
    this.followings = followings
    this.goals = goals
    this.wellnessTrainingAndDiet = wellnessTrainingAndDiet
    this.wellnessGoals = wellnessGoals
    this.wellnessGearList = wellnessGearList
    this.wellnessMotivationalList = wellnessMotivationalList
  }

  public onViewGoalsClicked = (content, selectedGoal: ArpGoalDto) => {
    this.selectedGoal = selectedGoal
    this.modalService.open(content, {size: 'lg'});
  }
}
