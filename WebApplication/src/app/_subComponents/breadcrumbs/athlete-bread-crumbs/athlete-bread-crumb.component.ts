import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../location-info-rankings/location-info-rankings.component';
import { BreadcrumbLocation } from '../../../_common/breadcrumbLocation';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';

@Component({
  standalone: true,
  selector: 'app-athlete-bread-crumb',
  templateUrl: './athlete-bread-crumb.component.html',
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent],
  styleUrls: []
})
export class AthleteBreadcrumbComponent extends ComponentBaseWithRoutes {

  public breadcrumbAll = BreadcrumbLocation.All
  public breadcrumbState = BreadcrumbLocation.State
  public breadcrumbArea = BreadcrumbLocation.Area
  public breadcrumbCity = BreadcrumbLocation.City
  public breadcrumbArp = BreadcrumbLocation.Arp

  @Input('breadcrumbResult')
  public breadcrumbResult: any

  @Input('breadcrumbLocation')
  public breadcrumbLocation: any

  @Input('titleOverride')
  public titleOverride: string | undefined
}
