import { ActivatedRoute } from "@angular/router";
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy, OnInit, inject } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ScoringApiService } from "../../services/scoring-api.service";
import { Subject, Subscription, switchMap } from "rxjs";
import { LeaderboardQuickViewModalContent } from "./leaderboardQuickViewModal/leaderboard-quick-view.component";

@Injectable()
export abstract class EventsComponentBase extends BreadcrumbComponent implements OnInit, OnDestroy {

  public eventSearchResultsChunked!: any[][]
  public dashboardInfoResponseDto: any
  public title: any
  public isLanding = false

  private modalService = inject(NgbModal)

  private viewLeaderboardSubject = new Subject<string>();
  private viewLeaderboardSubscription: Subscription | null = null

  protected constructor(route: ActivatedRoute, httpClient: HttpClient, protected scoringApiService: ScoringApiService) {
    super(route, httpClient)
  }

  ngOnInit(): void {
    const viewLeaderboardPiped$ = this.viewLeaderboardSubject.pipe(
      switchMap(data => {
        return this.scoringApiService.getRaceLeaderboard(data as any)
      })
    )

    this.viewLeaderboardSubscription = viewLeaderboardPiped$.subscribe(data => {
      const modalRef = this.modalService.open(LeaderboardQuickViewModalContent, { size: 'xl' });
      modalRef.componentInstance.raceLeaderboard = data
    });
  }

  ngOnDestroy() {
    this.viewLeaderboardSubject?.unsubscribe();
    this.viewLeaderboardSubscription?.unsubscribe();
  }

  receiveData(data: string) {
    this.viewLeaderboardSubject.next(data)
  }
}