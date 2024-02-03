
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { config } from '../config';

@Component({
  standalone: true, 
  imports: [CommonModule, RouterModule, HttpClientModule, LocationInfoRankingsComponent], 
  selector: 'app-arp-compare',
  templateUrl: './arp-compare.component.html',
  styleUrls: []
})
export class ArpCompareComponent extends BreadcrumbComponent implements OnInit {

  public getAthletesToCompare(athleteIds: number[]): Observable<any> {
    const body: any = {
      AthleteIds: athleteIds
    };

    const url = `${config.apiUrl}/compareAthletesApi`
    return this.http.post<any>(url, body)
  }

  public athleteInfoDtos!: any[]
  public dataLoaded = false
  public athleteIds: string[]

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)

    const athleteIdsAsString = (this.route.snapshot.queryParamMap as any).params["athleteIds"] as string
    this.athleteIds = JSON.parse(athleteIdsAsString)
  }

  ngOnInit() {

    const athleteIds = this.athleteIds.map(oo => parseInt(oo))

    this.getAthletesToCompare(athleteIds).subscribe((result: any) => {
      this.athleteInfoDtos = result
      this.dataLoaded = true
    })
  }
}
