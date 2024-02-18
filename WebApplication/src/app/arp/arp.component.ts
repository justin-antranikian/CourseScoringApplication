import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AthleteBreadcrumbComponent } from '../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { ArpResultComponent } from './arp-result.component';
import { Observable, tap } from 'rxjs';
import { ScoringApiService } from '../services/scoring-api.service';

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

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService, private modalService: NgbModal) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    const athleteId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.$arp = this.scoringApiService.getArpDto(athleteId).pipe(
      tap(arp => this.athletesBreadcrumbResult = { locationInfoWithUrl: arp.locationInfoWithRank })
    )
  }

  public onViewGoalsClicked = (content: any, selectedGoal: any) => {
    this.selectedGoal = selectedGoal
    this.modalService.open(content, { size: 'lg' });
  }
}
