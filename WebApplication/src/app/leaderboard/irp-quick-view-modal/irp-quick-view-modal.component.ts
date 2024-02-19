import { Component, inject } from '@angular/core';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { IrpIntervalResultComponent } from '../../irp/irp-interval-result.component';

@Component({
  standalone: true,
  selector: 'athlete-search-result',
  templateUrl: './irp-quick-view-modal.component.html',
  imports: [CommonModule, NgbModule, RouterModule, LocationInfoRankingsComponent, IrpIntervalResultComponent],
  styleUrls: []
})
export class IrpQuickViewModalComponent extends ComponentBaseWithRoutes {
  public modal = inject(NgbActiveModal)
  public irp: any
}
