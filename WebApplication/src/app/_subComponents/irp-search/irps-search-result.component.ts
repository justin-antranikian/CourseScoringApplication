import { Component, Input } from '@angular/core';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { IrpSearchResultDto } from './IrpSearchResultDto';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-irps-search-result',
  templateUrl: './irps-search-result.component.html',
  imports: [CommonModule, RouterModule],
  styleUrls: []
})
export class IrpsSearchResultComponent extends ComponentBaseWithRoutes {

  @Input('irpSearchResultDto')
  public irpSearchResultDto!: IrpSearchResultDto
}