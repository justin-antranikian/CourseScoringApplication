import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { CommonModule } from '@angular/common';
import { ScoringApiService } from '../../services/scoring-api.service';
import { Observable } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule],
  selector: 'app-awards-podium',
  templateUrl: './awards-podium.component.html',
  styleUrls: []
})
export class AwardsPodiumComponent extends BreadcrumbComponent implements OnInit {

  public awards$!: Observable<any[]>
  public eventsBreadcrumbResult$!: Observable<any>

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.CourseLeaderboard
  }

  ngOnInit() {
    const courseId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.awards$ = this.scoringApiService.getAwardsPodium(courseId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.CourseLeaderboard, courseId.toString())
    this.eventsBreadcrumbResult$ = this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest)
  }
}
