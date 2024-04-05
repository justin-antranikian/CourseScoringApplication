import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { Injectable, OnDestroy, OnInit, inject } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ScoringApiService } from "../../services/scoring-api.service";
import { Subject, Subscription, switchMap } from "rxjs";
import { LeaderboardQuickViewModalComponent } from "./leaderboard-quick-view-modal/leaderboard-quick-view-modal.component";
import { EventSearchResultDto } from './eventSearchResultDto';

@Injectable()
export abstract class EventsComponentBase extends BreadcrumbComponent implements OnInit, OnDestroy {

  public eventsBreadcrumbResult: any
  public eventSearchResults!: EventSearchResultDto[]
  public dashboardInfoResponseDto: any
  public title: any
  public isLanding = false

  private modalService = inject(NgbModal)
  protected scoringApiService = inject(ScoringApiService)

  private quickViewSubject = new Subject<number>();
  private quickViewSubscription: Subscription | null = null

  ngOnInit() {
    const viewLeaderboard$ = this.quickViewSubject.pipe(
      switchMap(raceId => {
        return this.scoringApiService.getRaceLeaderboard(raceId)
      })
    )

    this.quickViewSubscription = viewLeaderboard$.subscribe(data => {
      const modalRef = this.modalService.open(LeaderboardQuickViewModalComponent, { size: 'xl' });
      modalRef.componentInstance.raceLeaderboard = data
    });
  }

  ngOnDestroy() {
    this.quickViewSubject.unsubscribe();
    this.quickViewSubscription!.unsubscribe();
    this.modalService.dismissAll()
  }

  public onQuickViewClicked = (raceId: number) => this.quickViewSubject.next(raceId)
}