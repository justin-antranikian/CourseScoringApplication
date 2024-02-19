import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AthleteBreadcrumbComponent } from '../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { Observable, Subject, Subscription } from 'rxjs';
import { ScoringApiService } from '../services/scoring-api.service';
import { ArpGoalsModalComponent } from './arp-goals-modal.component';
import { BracketRankComponent } from '../_subComponents/bracket-rank/bracket-rank.component';
import { IntervalTimeComponent } from '../_subComponents/interval-time/interval-time.component';

@Component({
  standalone: true,
  selector: 'app-arp',
  templateUrl: './arp.component.html',
  imports: [CommonModule, RouterModule, HttpClientModule, NgbModule, AthleteBreadcrumbComponent, LocationInfoRankingsComponent, BracketRankComponent, IntervalTimeComponent],
  styleUrls: []
})
export class ArpComponent extends BreadcrumbComponent implements OnInit {

  public selectedGoal: any
  public $arp!: Observable<any>

  private quickViewSubject = new Subject<number>();
  private quickViewSubscription: Subscription | null = null

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService, private modalService: NgbModal) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.RaceSeriesOrArp
  }

  ngOnInit() {
    const athleteId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.$arp = this.scoringApiService.getArpDto(athleteId)

    this.quickViewSubscription = this.quickViewSubject.subscribe(data => {
      const modalRef = this.modalService.open(ArpGoalsModalComponent, { size: 'lg' });
      modalRef.componentInstance.selectedGoal = data
    });
  }

  ngOnDestroy() {
    this.quickViewSubject?.unsubscribe();
    this.quickViewSubscription?.unsubscribe();
    this.modalService.dismissAll()
  }

  public onViewGoalsClicked = (goal: any) => {
    this.quickViewSubject.next(goal)
  }
}
