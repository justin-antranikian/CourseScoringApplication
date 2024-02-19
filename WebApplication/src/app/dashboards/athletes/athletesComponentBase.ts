import { Injectable, OnDestroy, OnInit, inject } from '@angular/core';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { AthleteSearchResultDto } from '../../_core/athleteSearchResultDto';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject, Subscription, switchMap, tap } from 'rxjs';
import { ScoringApiService } from '../../services/scoring-api.service';
import { AthleteQuickViewModalComponent } from './athlete-quick-view-modal/athlete-quick-view-modal.component';

@Injectable()
export abstract class AthletesComponentBase extends BreadcrumbComponent implements OnInit, OnDestroy {

  public athletesBreadcrumbResult: any
  public athleteSearchResultsChunked!: any[][]
  public dashboardInfoResponseDto!: any

  public title: any
  public isLanding = false

  private modalService = inject(NgbModal)
  protected scoringApiService = inject(ScoringApiService)

  private quickViewSubject = new Subject<number>();
  private quickViewSubscription: Subscription | null = null

  public athleteIdsToCompare: number[] = []
  public athleteIdsToCompareString: any = null
  public showToast = false;
  public showSpinner = true

  ngOnInit() {
    const viewLeaderboard$ = this.quickViewSubject.pipe(
      tap(() => this.showSpinner = true),
      switchMap(athleteId => {
        return this.scoringApiService.getArpDto(athleteId)
      })
    )

    this.quickViewSubscription = viewLeaderboard$.subscribe(data => {
      const modalRef = this.modalService.open(AthleteQuickViewModalComponent, { size: 'xl' });
      modalRef.componentInstance.arp = data
      this.showSpinner = false
    });
  }

  ngOnDestroy() {
    this.quickViewSubject?.unsubscribe();
    this.quickViewSubscription?.unsubscribe();
    this.modalService.dismissAll()
  }

  public onViewArpClicked = ({ id }: any) => {
    this.quickViewSubject.next(id as number)
  }

  public onCompareClicked = ({ id }: AthleteSearchResultDto) => {
    const athleteIds = this.athleteIdsToCompare

    if (!athleteIds.includes(id)) {
      athleteIds.push(id)
    } else {
      const index = athleteIds.indexOf(id);
      athleteIds.splice(index, 1);
    }

    this.athleteIdsToCompareString = JSON.stringify(athleteIds)
    this.showToast = true
  }

  public onCloseCompareClicked = () => {
    this.athleteIdsToCompare = []
    this.showToast = false
  }
}