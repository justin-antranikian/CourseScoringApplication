import { OnInit, OnDestroy, Injectable, inject } from "@angular/core";
import { BreadcrumbComponent } from "../_common/breadcrumbComponent";
import { Subject, Subscription, switchMap } from "rxjs";
import { ScoringApiService } from "../services/scoring-api.service";
import { IrpQuickViewModalComponent } from "./irp-quick-view-modal/irp-quick-view-modal.component";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Injectable()
export abstract class LeaderboardComponentBase extends BreadcrumbComponent implements OnInit, OnDestroy {

  protected scoringApiService = inject(ScoringApiService)
  private modalService = inject(NgbModal)

  private quickViewSubject = new Subject<number>();
  private quickViewSubscription: Subscription | null = null

  ngOnInit() {
    const quickView$ = this.quickViewSubject.pipe(
      switchMap(athleteCourseId => {
        return this.scoringApiService.getIrpDto(athleteCourseId)
      })
    )

    this.quickViewSubscription = quickView$.subscribe(data => {
      const modalRef = this.modalService.open(IrpQuickViewModalComponent, { size: 'xl' });
      modalRef.componentInstance.irp = data
    });
  }

  ngOnDestroy() {
    this.quickViewSubject?.unsubscribe();
    this.quickViewSubscription?.unsubscribe();
    this.modalService.dismissAll()
  }

  public onViewIrpClicked = (athleteCourseId: number) => {
    this.quickViewSubject.next(athleteCourseId)
  }
}