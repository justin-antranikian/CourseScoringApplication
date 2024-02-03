import { Component, Input } from '@angular/core';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocationInfoRankingsComponent } from '../location-info-rankings/location-info-rankings.component';
import { IrpIntervalResultComponent } from '../../irp/irp-interval-result.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  selector: '[app-irp-quick-view]',
  templateUrl: './irp-quick-view.component.html',
  styleUrls: [],
  imports: [RouterModule, CommonModule, NgbModule, LocationInfoRankingsComponent, IrpIntervalResultComponent]
})
export class IrpQuickViewComponent extends ComponentBaseWithRoutes {
  @Input('irp')
  public irp: any
}
