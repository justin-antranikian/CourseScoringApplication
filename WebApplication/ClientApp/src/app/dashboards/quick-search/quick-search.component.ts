import { Component, Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { AthleteSearchResultDto } from 'src/app/_orchestration/searchAthletes/athleteSearchResultDto';
import { EventSearchResultDto } from 'src/app/_orchestration/searchEvents/eventSearchResultDto';
import { SearchAthletesRequestDto } from 'src/app/_orchestration/searchAthletes/searchAthletesRequestDto';
import { SearchEventsRequestDto } from 'src/app/_orchestration/searchEvents/searchEventsRequestDto';
import { ApiRequestService } from 'src/app/_services/api-request.service';

/**
 * Events or Athletes are the possible navigation types.
 */
export enum QuickSearchType {
  Events,
  Athletes
}

@Component({
  selector: 'app-quick-search',
  templateUrl: './quick-search.component.html',
  styleUrls: ['./quick-search.component.css']
})
export class QuickSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('quickSearchType')
  public quickSearchType: QuickSearchType

  @Input('breadcrumbLocation')
  public breadcrumbLocation: BreadcrumbLocation;

  @Input('title')
  public title: string | undefined;

  private searchTerms = new Subject<string>();
  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public searchRequested: boolean = false

  public searchResults: EventSearchResultDto[] | AthleteSearchResultDto[]

  constructor(
     private readonly apiService: ApiRequestService
  ) { super() }

  ngOnInit() {

    const searchTerms$ = this.searchTerms.pipe(
      debounceTime(600),
      distinctUntilChanged(),
      tap((searchTerm: string) => {
        if (searchTerm === '') {
          this.searchResults = []
          this.searchRequested = false
        }
      }),
      filter((searchTerm: string) => searchTerm !== ''),
      switchMap((searchTerm: string) => {

        if (this.quickSearchType === QuickSearchType.Events) {
          const searchFilter = this.getRaceSeriesFilterDto(searchTerm)
          return this.apiService.getRaceSeriesSearchResults(searchFilter)
        }

        const searchFilter = this.getSearchAthletesFilterDto(searchTerm)
        return this.apiService.getAthletes(searchFilter)
      })
    )

    searchTerms$.subscribe((results) => {
      this.searchResults = results
      this.searchRequested = true
    })
  }

  public getRaceSeriesFilterDto = (searchTerm: string): SearchEventsRequestDto => {
    switch (this.breadcrumbLocation) {
      case BreadcrumbLocation.All: {
        return new SearchEventsRequestDto(searchTerm)
      }
      case BreadcrumbLocation.State: {
        return new SearchEventsRequestDto(searchTerm, this.title)
      }
      case BreadcrumbLocation.Area: {
        return new SearchEventsRequestDto(searchTerm, null, this.title)
      }
      case BreadcrumbLocation.City: {
        return new SearchEventsRequestDto(searchTerm, null, null, this.title)
      }
    }
  }

  public getSearchAthletesFilterDto = (searchTerm: string): SearchAthletesRequestDto => {
    switch (this.breadcrumbLocation) {
      case BreadcrumbLocation.All: {
        return new SearchAthletesRequestDto(null, null, null, searchTerm)
      }
      case BreadcrumbLocation.State: {
        return new SearchAthletesRequestDto(this.title, null, null, searchTerm)
      }
      case BreadcrumbLocation.Area: {
        return new SearchAthletesRequestDto(null, this.title, null, searchTerm)
      }
      case BreadcrumbLocation.City: {
        return new SearchAthletesRequestDto(null, null, this.title, searchTerm)
      }
    }
  }

  public search(term: string): void {
    this.searchTerms.next(term);
  }
}
