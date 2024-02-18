import { ActivatedRoute } from "@angular/router";
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClient } from '@angular/common/http';

export abstract class EventsComponentBase extends BreadcrumbComponent {

  public eventSearchResultsChunked!: any[][];
  public dashboardInfoResponseDto: any
  public title: any
  public isLanding = false

  constructor(route: ActivatedRoute, httpClient: HttpClient) {
    super(route, httpClient)
  }
}