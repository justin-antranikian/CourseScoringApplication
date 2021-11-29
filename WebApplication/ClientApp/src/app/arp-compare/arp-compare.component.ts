
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiRequestService } from '../_services/api-request.service';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-arp-compare',
  templateUrl: './arp-compare.component.html',
  styleUrls: []
})
export class ArpCompareComponent extends BreadcrumbComponent implements OnInit {

  public athleteInfoDtos: any[]
  public dataLoaded = false
  public athleteIds: string[]

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)

    const athleteIdsAsString = (this.route.snapshot.queryParamMap as any).params["athleteIds"] as string
    this.athleteIds = JSON.parse(athleteIdsAsString)
  }

  ngOnInit() {

    const athleteIds = this.athleteIds.map(oo => parseInt(oo))

    this.apiService.getAthletesToCompare(athleteIds).subscribe((result: any) => {
      this.athleteInfoDtos = result
      this.dataLoaded = true
    })
  }
}
