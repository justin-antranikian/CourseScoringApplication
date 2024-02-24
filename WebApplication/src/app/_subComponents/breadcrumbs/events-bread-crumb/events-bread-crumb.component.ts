import { Component, Input } from '@angular/core';
import { BreadcrumbLocation } from '../../../_common/breadcrumbLocation';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';

@Component({
  standalone: true,
  selector: 'app-events-bread-crumb',
  templateUrl: './events-bread-crumb.component.html',
  imports: [RouterModule, CommonModule],
  styleUrls: []
})
export class EventsBreadcrumbComponent extends ComponentBaseWithRoutes {

  public breadcrumbAll = BreadcrumbLocation.All
  public breadcrumbState = BreadcrumbLocation.State
  public breadcrumbArea = BreadcrumbLocation.Area
  public breadcrumbCity = BreadcrumbLocation.City
  public breadcrumbRaceSeries = BreadcrumbLocation.RaceSeries
  public breadcrumbRaceLeaderboard = BreadcrumbLocation.RaceLeaderboard
  public breadcrumbCourseLeaderboard = BreadcrumbLocation.CourseLeaderboard
  public breadcrumbIrp = BreadcrumbLocation.Irp

  @Input('breadcrumbResult')
  public breadcrumbResult: any

  @Input('breadcrumbLocation')
  public breadcrumbLocation: any
}
