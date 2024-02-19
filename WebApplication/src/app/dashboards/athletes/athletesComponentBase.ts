import { Injectable, OnDestroy, OnInit, inject } from '@angular/core';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { AthleteSearchResultDto } from '../../_core/athleteSearchResultDto';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject, Subscription, switchMap } from 'rxjs';
import { ScoringApiService } from '../../services/scoring-api.service';
import { AthleteQuickViewComponent } from './athlete-quick-view/athlete-quick-view.component';

@Injectable()
export abstract class AthletesComponentBase extends BreadcrumbComponent implements OnInit, OnDestroy {

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

  ngOnInit() {
    const viewLeaderboard$ = this.quickViewSubject.pipe(
      switchMap(athleteId => {
        return this.scoringApiService.getArpDto(athleteId)
      })
    )

    this.quickViewSubscription = viewLeaderboard$.subscribe(data => {
      const modalRef = this.modalService.open(AthleteQuickViewComponent, { size: 'xl' });
      modalRef.componentInstance.arp = data
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