import { Component, OnInit } from '@angular/core';
import { ApiRequestService } from '../_services/api-request.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';
import { RaceTypeFilterModel } from '../_common/raceTypeFilterModel';
import { keys } from 'lodash'
import { SearchEventsRequestDto } from '../_orchestration/searchEvents/searchEventsRequestDto';
import { EventSearchResultDto } from '../_orchestration/searchEvents/eventSearchResultDto';
import { removeUndefinedKeyValues } from '../_common/jsonHelpers';

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.css']
})
export class AdvancedSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  public raceTypeFilters: RaceTypeFilterModel = new RaceTypeFilterModel()

  public stateFilter: string
  public areaFilter: string
  public cityFilter: string
  public eventFilter: string

  public searchResults: EventSearchResultDto[] = []

  constructor(
    private readonly route: ActivatedRoute,
    private readonly apiService: ApiRequestService,
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
        this.apiService.getRaceSeriesSearchResults(searchFilterDto).subscribe((results) => {
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
