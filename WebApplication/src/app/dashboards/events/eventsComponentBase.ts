import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { Injectable, OnDestroy, OnInit, inject } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ScoringApiService } from "../../services/scoring-api.service";
import { Subject, Subscription, switchMap } from "rxjs";
import { LeaderboardQuickViewModalContent } from "./leaderboard-quick-view/leaderboard-quick-view.component";

@Injectable()
export abstract class EventsComponentBase extends BreadcrumbComponent implements OnInit, OnDestroy {

  public eventSearchResultsChunked!: any[][]
  public dashboardInfoResponseDto: any
  public title: any
  public isLanding = false

  private modalService = inject(NgbModal)
  protected scoringApiService = inject(ScoringApiService)

  private viewLeaderboardSubject = new Subject<number>();
  private viewLeaderboardSubscription: Subscription | null = null

  ngOnInit(): void {
    const viewLeaderboard$ = this.viewLeaderboardSubject.pipe(
      switchMap(raceId => {
        return this.scoringApiService.getRaceLeaderboard(raceId)
      })
    )

    this.viewLeaderboardSubscription = viewLeaderboard$.subscribe(data => {
      const modalRef = this.modalService.open(LeaderboardQuickViewModalContent, { size: 'xl' });
      modalRef.componentInstance.raceLeaderboard = data
    });
  }

  ngOnDestroy() {
    this.viewLeaderboardSubject?.unsubscribe();
    this.viewLeaderboardSubscription?.unsubscribe();
    this.modalService.dismissAll()
  }

  public receiveData = (data: number) => this.viewLeaderboardSubject.next(data)
}