import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../../_core/breadcrumbRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { LeaderboardResultComponent } from '../../_subComponents/leaderboard-results-grid/leaderboard-result.component';
import { IrpsSearchComponent } from '../../_subComponents/irp-search/irps-search.component';
import { ScoringApiService } from '../../services/scoring-api.service';
import { Observable, Subject, Subscription, switchMap } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IrpQuickViewComponent } from '../irp-quick-view/irp-quick-view.component';

@Component({
  standalone: true,
  selector: 'app-race-leaderboard',
  templateUrl: './race-leaderboard.component.html',
  imports: [HttpClientModule, CommonModule, RouterModule, EventsBreadcrumbComponent, LocationInfoRankingsComponent, LeaderboardResultComponent, IrpsSearchComponent],
  styleUrls: ['./race-leaderboard.component.css']
})
export class RaceLeaderboardComponent extends BreadcrumbComponent implements OnInit, OnDestroy {

  public raceId!: number
  public race$!: Observable<any>

  private quickViewSubject = new Subject<number>();
  private quickViewSubscription: Subscription | null = null

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService, private modalService: NgbModal) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.RaceLeaderboard
  }

  ngOnInit() {
    this.raceId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.race$ = this.scoringApiService.getRaceLeaderboard(this.raceId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.RaceLeaderboard, this.raceId.toString())
    this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest).subscribe(result => {
      this.eventsBreadcrumbResult = result
    })

    const quickView$ = this.quickViewSubject.pipe(
      switchMap(athleteCourseId => {
        return this.scoringApiService.getIrpDto(athleteCourseId)
      })
    )

    this.quickViewSubscription = quickView$.subscribe(data => {
      const modalRef = this.modalService.open(IrpQuickViewComponent, { size: 'xl' });
      modalRef.componentInstance.irp = data
    });
  }

  ngOnDestroy() {
    this.quickViewSubject?.unsubscribe();
    this.quickViewSubscription?.unsubscribe();
    this.modalService.dismissAll()
  }

  public onViewIrpClicked = ({ athleteCourseId }: any) => {
    this.quickViewSubject.next(athleteCourseId as number)
  }
}
