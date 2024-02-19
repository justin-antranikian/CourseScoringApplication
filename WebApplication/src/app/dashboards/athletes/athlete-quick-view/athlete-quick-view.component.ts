import { Component, inject } from '@angular/core';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { BracketRankComponent } from '../../../_subComponents/bracket-rank/bracket-rank.component';
import { IntervalTimeComponent } from '../../../_subComponents/interval-time/interval-time.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'athlete-search-result',
  templateUrl: './athlete-quick-view.component.html',
  imports: [CommonModule, NgbModule, RouterModule, BracketRankComponent, IntervalTimeComponent],
  styleUrls: []
})
export class AthleteQuickViewComponent extends ComponentBaseWithRoutes {
  public modal = inject(NgbActiveModal)
  public arp: any
}
