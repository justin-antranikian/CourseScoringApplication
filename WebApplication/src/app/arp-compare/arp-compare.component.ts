import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { ScoringApiService } from '../services/scoring-api.service';
import { BreadcrumbComponentBase } from '../_subComponents/breadcrumbs/breadcrumbComponentBase';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule, LocationInfoRankingsComponent], 
  selector: 'app-arp-compare',
  templateUrl: './arp-compare.component.html',
  styleUrls: []
})
export class ArpCompareComponent extends BreadcrumbComponentBase implements OnInit {

  public athletes$!: Observable<any[]>

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService) {
    super()
  }

  ngOnInit() {
    const athleteIdsAsString = (this.route.snapshot.queryParamMap as any).params["athleteIds"] as string
    const athleteIds = JSON.parse(athleteIdsAsString).map((oo: string) => parseInt(oo))
    this.athletes$ = this.scoringApiService.getAthletesToCompare(athleteIds)
  }
}
