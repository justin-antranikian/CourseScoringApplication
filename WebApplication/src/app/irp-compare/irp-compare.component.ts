import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { HttpClient } from '@angular/common/http';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../_core/breadcrumbRequestDto';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ScoringApiService } from '../services/scoring-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule],
  selector: 'app-irp-compare',
  templateUrl: './irp-compare.component.html',
  styleUrls: []
})
export class IrpCompareComponent extends BreadcrumbComponent implements OnInit {

  public irpsToCompare$!: Observable<any[]>
  public dataLoaded = false

  constructor(route: ActivatedRoute, http: HttpClient, private scoringApiService: ScoringApiService) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.Irp
  }

  ngOnInit() {
    const courseId = this.getId()

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, courseId.toString())

    this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest).subscribe(result => {
      this.eventsBreadcrumbResult = result
    })

    const athleteCourseIdsAsString = (this.route.snapshot.queryParamMap as any).params["athleteCourseIds"] as string
    const athleteCourseIds = JSON.parse(athleteCourseIdsAsString).map((oo: string) => parseInt(oo))

    this.irpsToCompare$ = this.scoringApiService.getIrpToCompare(athleteCourseIds)
  }
}
