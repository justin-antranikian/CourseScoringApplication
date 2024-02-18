import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { IrpQuickViewComponent } from './irp-quick-view.component';
import { ScoringApiService } from '../../services/scoring-api.service';

@Component({
  standalone: true,
  selector: '[app-leaderboard-result]',
  templateUrl: './leaderboard-result.component.html',
  imports: [CommonModule, RouterModule, IrpQuickViewComponent],
  styleUrls: []
})
export class LeaderboardResultComponent extends ComponentBaseWithRoutes {

  constructor(
    private modalService: NgbModal,
    private scoringApiService: ScoringApiService
  ) { super() }

  @Input('leaderboardResult')
  public leaderboardResult: any

  @Input('hideViewIrpButton')
  public hideViewIrpButton!: boolean

  public selectedIrp: any = null

  public onViewIrpClicked = (modal: any) => {
    const { athleteCourseId } = this.leaderboardResult

    this.scoringApiService.getIrpDto(athleteCourseId).subscribe((irpDto: any) => {
      this.selectedIrp = irpDto
      this.modalService.open(modal, { size: 'xl' });
    })
  }
}
