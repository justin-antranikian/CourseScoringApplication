import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { keys } from 'lodash'
import { SearchEventsRequestDto } from '../_orchestration/searchEvents/searchEventsRequestDto';
import { EventSearchResultDto } from '../_orchestration/searchEvents/eventSearchResultDto';
import { removeUndefinedKeyValues } from '../_common/jsonHelpers';
import { RaceTypeFilterModel } from '../_common/raceTypeFilterModel';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { getHttpParams } from '../_common/httpParamsHelpers';
import { mapRaceSeriesTypeToImageUrl } from '../_common/IRaceSeriesType';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.css']
})
export class AdvancedSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  public getRaceSeriesSearchResults(searchEventsRequest: SearchEventsRequestDto): Observable<EventSearchResultDto[]> {
    const httpParams = getHttpParams(searchEventsRequest.getAsParamsObject())

    const raceSeriesSearch$ = this.http.get<EventSearchResultDto[]>(`https://localhost:44308/raceSeriesSearchApi`, httpParams).pipe(
      map((raceSeriesEntries: EventSearchResultDto[]): EventSearchResultDto[] => raceSeriesEntries.map(mapRaceSeriesTypeToImageUrl)),
    )

    return raceSeriesSearch$
  }

  public raceTypeFilters: any = new RaceTypeFilterModel()

  public stateFilter!: string
  public areaFilter!: string
  public cityFilter!: string
  public eventFilter!: string

  public searchResults: EventSearchResultDto[] = []

  constructor(
    private readonly route: ActivatedRoute,
    private readonly http: HttpClient,
    private readonly router: Router,
  ) { super() }

  ngOnInit() {
    this.route.queryParams.subscribe((data: any) => {
      this.raceTypeFilters.running = data.running ? data.running : false
      this.raceTypeFilters.triathalon = data.triathalon ? data.triathalon : false
      this.raceTypeFilters.roadBiking = data.roadBiking ? data.roadBiking : false
      this.raceTypeFilters.mountainBiking = data.mountainBiking ? data.mountainBiking : false
      this.raceTypeFilters.crossCountrySkiing = data.crossCountrySkiing ? data.crossCountrySkiing : false
      this.raceTypeFilters.swim = data.swim ? data.swim : false

      this.eventFilter = data.eventFilter
      this.stateFilter = data.stateFilter
      this.areaFilter = data.areaFilter
      this.cityFilter = data.cityFilter

      if (keys(data).length > 0) {
        const raceSeriesTypes = this.raceTypeFilters.getSelectedTypes()
        const searchFilterDto = new SearchEventsRequestDto(this.eventFilter, this.stateFilter, this.areaFilter, this.cityFilter, raceSeriesTypes)
        this.getRaceSeriesSearchResults(searchFilterDto).subscribe((results: any) => {
          this.searchResults = results
        })
      }
    })
  }

  public setFilters = (allSelected: boolean) => {
    this.raceTypeFilters.setFilterValue(allSelected)
  }

  public onSearchClicked = () => {

    const params = removeUndefinedKeyValues({
      ...this.raceTypeFilters.getSelectedTypesValues(),
      eventFilter: this.eventFilter,
      stateFilter: this.stateFilter,
      areaFilter: this.areaFilter,
      cityFilter: this.cityFilter,
    })

    this.router.navigate([],
    {
      relativeTo: this.route,
      queryParams: params
    });
  }
}
