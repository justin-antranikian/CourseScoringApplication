import { chunk } from 'lodash'
import { Component, OnInit } from '@angular/core';
import { ApiRequestService } from '../_services/api-request.service';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';

@Component({
  selector: 'races-by-month-report',
  templateUrl: './races-by-month-report.component.html',
  styleUrls: ['./races-by-month-report.component.css']
})
export class RacesByMonthReportComponent extends ComponentBaseWithRoutes implements OnInit {

  public byMonthReports: any[] = []

  constructor(
    private readonly apiService: ApiRequestService
 ) { super() }

  ngOnInit() {
    this.apiService.getRacesByMonthReport().subscribe((reportData: any[]) => {
      this.byMonthReports = reportData.map(byMonthGrouping => {
        return {
          monthName: byMonthGrouping.monthName,
          racesChunked: chunk(byMonthGrouping.races, 4)
        }
      })
    })
  }
}
