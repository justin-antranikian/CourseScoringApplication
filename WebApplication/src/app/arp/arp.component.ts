import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AthleteBreadcrumbComponent } from '../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { ArpResultComponent } from './arp-result.component';

@Component({
  standalone: true,
  selector: 'app-arp',
  templateUrl: './arp.component.html',
  imports: [CommonModule, RouterModule, HttpClientModule, ArpResultComponent, NgbModule, AthleteBreadcrumbComponent, LocationInfoRankingsComponent],
  styleUrls: []
})
export class ArpComponent extends BreadcrumbComponent implements OnInit {

  public selectedGoal: any
  public arpLoaded: boolean = false

  // extract as props to render in template.
  public fullName!: string
  public firstName!: string
  public age!: number
  public genderAbbreviated!: string
  public locationInfoWithRank: any
  public results!: any[]
  public tags!: string[]
  public rivals!: any[]
  public followings!: any[]
  public goals!: any[]
  public wellnessTrainingAndDiet!: any[]
  public wellnessGoals!: any[]
  public wellnessGearList!: any[]
  public wellnessMotivationalList!: any[]

  constructor(route: ActivatedRoute, http: HttpClient, private modalService: NgbModal) {
    super(route, http)
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
    this.getArpDto(athleteId).subscribe(this.handleGetArpResponse);
  }

  private handleGetArpResponse = (arpDto: any) => {
    const { locationInfoWithRank } = arpDto
    this.athletesBreadcrumbResult = { locationInfoWithUrl: locationInfoWithRank }

    this.setPropsToRender(arpDto)
    this.arpLoaded = true
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (arpDto: any) => {
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

  public onViewGoalsClicked = (content: any, selectedGoal: any) => {
    this.selectedGoal = selectedGoal
    this.modalService.open(content, {size: 'lg'});
  }
}
