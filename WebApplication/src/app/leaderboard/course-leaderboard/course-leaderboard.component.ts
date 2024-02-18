import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { NgbModal, NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { IrpQuickViewComponent } from '../../_subComponents/leaderboard-results-grid/irp-quick-view.component';
import { CommonModule } from '@angular/common';
import { IrpsSearchComponent } from '../../_subComponents/irp-search/irps-search.component';
import { FormsModule } from '@angular/forms';
import { ScoringApiService } from '../../services/scoring-api.service';
import { Observable } from 'rxjs';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../../_core/breadcrumbRequestDto';

@Component({
  standalone: true,
  selector: 'app-course-leaderboard',
  imports: [EventsBreadcrumbComponent, LocationInfoRankingsComponent, RouterLink, NgbToastModule, IrpQuickViewComponent, HttpClientModule, CommonModule, IrpsSearchComponent, FormsModule],
  templateUrl: './course-leaderboard.component.html',
  styleUrls: ['./course-leaderboard.component.css']
})
export class CourseLeaderboardComponent extends BreadcrumbComponent implements OnInit {

  public courseId!: number
  public course$!: Observable<any>

  public selectedIrp: any
  public showToast = false;

  public athleteCourseIdsToCompare: number[] = []
  public athleteCourseIdsToCompareString: any = null

  constructor(
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private scoringApiService: ScoringApiService,
  ) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.CourseLeaderboard
  }

  ngOnInit() {
    const courseId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.courseId = courseId
    this.course$ = this.scoringApiService.getCourseLeaderboard(courseId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.CourseLeaderboard, courseId.toString())
    this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest).subscribe(result => {
      this.eventsBreadcrumbResult = result
    })
  }

  public onViewIrpClicked = (modal: any, result: any) => {
    this.scoringApiService.getIrpDto(result.athleteCourseId).subscribe((irpDto: any) => {
      this.selectedIrp = irpDto
      this.modalService.open(modal, { size: 'xl' });
    })
  }

  public compareIrpClicked = ({ athleteCourseId }: any) => {
    const athleteCourseIds = this.athleteCourseIdsToCompare

    if (!athleteCourseIds.includes(athleteCourseId)) {
      athleteCourseIds.push(athleteCourseId)
    } else {
      const index = athleteCourseIds.indexOf(athleteCourseId);
      athleteCourseIds.splice(index, 1);
    }

    this.athleteCourseIdsToCompareString = JSON.stringify(athleteCourseIds)
    this.showToast = true
  }

  public onCloseCompareCliecked = () => {
    this.athleteCourseIdsToCompare = []
    this.showToast = false
  }
}
