
import { Component, OnInit } from '@angular/core';
import { ApiRequestService } from '../_services/api-request.service';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';

@Component({
  selector: 'athlete-results-report',
  templateUrl: './athlete-results-report.component.html',
  styleUrls: []
})
export class AthleteResultsReportComponent extends ComponentBaseWithRoutes implements OnInit {
  
  public athleteReportData: any[] = []

  constructor(
    private readonly apiService: ApiRequestService
 ) { super() }

  ngOnInit() {
    this.apiService.getAthleteResultsReport().subscribe((reportData: any[]) => {
      this.athleteReportData = reportData
    })
  }
}
