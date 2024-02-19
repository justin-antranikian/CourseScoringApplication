import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../_core/breadcrumbRequestDto';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ScoringApiService } from '../services/scoring-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule],
  selector: 'app-irp-compare',
  templateUrl: './irp-compare.component.html'
})
export class IrpCompareComponent extends BreadcrumbComponent implements OnInit {

  public irpsToCompare$!: Observable<any[]>
  public eventsBreadcrumbResult$!: Observable<any>

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.Irp
  }

  ngOnInit() {
    const athleteCourseIdsAsString = (this.route.snapshot.queryParamMap as any).params["athleteCourseIds"] as string
    const athleteCourseIds = JSON.parse(athleteCourseIdsAsString).map((oo: string) => parseInt(oo))
    this.irpsToCompare$ = this.scoringApiService.getIrpToCompare(athleteCourseIds)

    const courseId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, courseId.toString())
    this.eventsBreadcrumbResult$ = this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest)
  }
}
