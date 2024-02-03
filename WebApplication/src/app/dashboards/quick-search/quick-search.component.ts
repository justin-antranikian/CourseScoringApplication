import { Component, Input, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { SearchEventsRequestDto } from '../../_orchestration/searchEvents/searchEventsRequestDto';
import { SearchAthletesRequestDto } from '../../_orchestration/searchAthletes/searchAthletesRequestDto';
import { getHttpParams } from '../../_common/httpParamsHelpers';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { mapRaceSeriesTypeToImageUrl } from '../../_common/IRaceSeriesType';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
// import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
// import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
// import { AthleteSearchResultDto } from 'src/app/_orchestration/searchAthletes/athleteSearchResultDto';
// import { EventSearchResultDto } from 'src/app/_orchestration/searchEvents/eventSearchResultDto';
// import { SearchAthletesRequestDto } from 'src/app/_orchestration/searchAthletes/searchAthletesRequestDto';
// import { SearchEventsRequestDto } from 'src/app/_orchestration/searchEvents/searchEventsRequestDto';
// import { ApiRequestService } from 'src/app/_services/api-request.service';

/**
 * Events or Athletes are the possible navigation types.
 */
export enum QuickSearchType {
  Events,
  Athletes
}

@Component({
  standalone: true,
  selector: 'app-quick-search',
  templateUrl: './quick-search.component.html',
  imports: [HttpClientModule, CommonModule, RouterModule],
  styleUrls: ['./quick-search.component.css']
})
export class QuickSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('quickSearchType')
  public quickSearchType!: QuickSearchType

  @Input('breadcrumbLocation')
  public breadcrumbLocation!: BreadcrumbLocation;

  @Input('title')
  public title: string | undefined;

  private searchTerms = new Subject<string>();
  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public searchRequested: boolean = false

  public searchResults!: any[] | any[]

  public getRaceSeriesSearchResults(searchEventsRequest: SearchEventsRequestDto): Observable<any[]> {
    const httpParams = getHttpParams(searchEventsRequest.getAsParamsObject())

    const raceSeriesSearch$ = this.http.get<any[]>(`https://localhost:44308/raceSeriesSearchApi`, httpParams).pipe(
      map((raceSeriesEntries: any[]): any[] => raceSeriesEntries.map(mapRaceSeriesTypeToImageUrl)),
    )

    return raceSeriesSearch$
  }

  public getAthletes(searchAthletesRequest: SearchAthletesRequestDto): Observable<any[]> {
    const httpParams = getHttpParams(searchAthletesRequest.getAsParamsObject())
    return this.http.get<any[]>(`https://localhost:44308/athleteSearchApi`, httpParams)
  }

  constructor(
     private readonly http: HttpClient
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
          return this.getRaceSeriesSearchResults(searchFilter)
        }

        const searchFilter = this.getSearchAthletesFilterDto(searchTerm)
        return this.getAthletes(searchFilter)
      })
    )

    searchTerms$.subscribe((results) => {
      this.searchResults = results
      this.searchRequested = true
    })
  }

  public getRaceSeriesFilterDto = (searchTerm: string): any => {
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

  public getSearchAthletesFilterDto = (searchTerm: string): any => {
    switch (this.breadcrumbLocation) {
      case BreadcrumbLocation.All: {
        return new SearchAthletesRequestDto(null, null, null, searchTerm)
      }
      case BreadcrumbLocation.State: {
        return new SearchAthletesRequestDto(this.title as any, null, null, searchTerm)
      }
      case BreadcrumbLocation.Area: {
        return new SearchAthletesRequestDto(null, this.title as any, null, searchTerm)
      }
      case BreadcrumbLocation.City: {
        return new SearchAthletesRequestDto(null, null, this.title as any, searchTerm)
      }
    }
  }

  public search(term: string): void {
    this.searchTerms.next(term);
  }
}
