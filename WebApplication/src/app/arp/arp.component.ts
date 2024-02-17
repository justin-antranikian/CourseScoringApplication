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
import { Observable, tap } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-arp',
  templateUrl: './arp.component.html',
  imports: [CommonModule, RouterModule, HttpClientModule, ArpResultComponent, NgbModule, AthleteBreadcrumbComponent, LocationInfoRankingsComponent],
  styleUrls: []
})
export class ArpComponent extends BreadcrumbComponent implements OnInit {

  public selectedGoal: any
  public $arp!: Observable<any>

  constructor(route: ActivatedRoute, http: HttpClient, private modalService: NgbModal) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    const athleteId = this.getId()
    this.$arp = this.getArpDto(athleteId).pipe(
      tap(arp => this.athletesBreadcrumbResult = { locationInfoWithUrl: arp.locationInfoWithRank })
    )
  }

  public onViewGoalsClicked = (content: any, selectedGoal: any) => {
    this.selectedGoal = selectedGoal
    this.modalService.open(content, {size: 'lg'});
  }
}
