import { Component, Input } from '@angular/core';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { IrpDto } from 'src/app/_orchestration/getIrp/irpDto';

@Component({
  selector: '[app-irp-quick-view]',
  templateUrl: './irp-quick-view.component.html',
  styleUrls: []
})
export class IrpQuickViewComponent extends ComponentBaseWithRoutes {
  @Input('irp')
  public irp: IrpDto
}
