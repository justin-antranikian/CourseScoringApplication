import { Component, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { SearchEventsRequestDto } from '../../_core/searchEventsRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ScoringApiService } from '../../services/scoring-api.service';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

export enum QuickSearchType {
  Events,
  Athletes
}

@Component({
  standalone: true,
  selector: 'app-quick-search',
  templateUrl: './quick-search.component.html',
  imports: [HttpClientModule, CommonModule, RouterModule, ReactiveFormsModule],
  styleUrls: ['./quick-search.component.css']
})
export class QuickSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('quickSearchType')
  public quickSearchType!: QuickSearchType

  @Input('breadcrumbLocation')
  public breadcrumbLocation!: BreadcrumbLocation;

  @Input('title')
  public title: string | undefined;

  public inputControl = new FormControl();
  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public searchTerm: string | null = ''

  public searchResults: any[] = []
  private subscription: Subscription | null = null

  constructor(private scoringApiService: ScoringApiService) {
     super()
  }

  ngOnInit() {
    this.subscription = this.inputControl.valueChanges.pipe(
      debounceTime(600),
      distinctUntilChanged(),
      tap(this.updateSearchTerm),
      filter((searchTerm: string) => searchTerm !== ''),
      switchMap((searchTerm: string) => {
        if (this.quickSearchType === QuickSearchType.Events) {
          const searchFilter = this.getRaceSeriesFilterDto(searchTerm)
          return this.scoringApiService.getRaceSeriesResults(searchFilter)
        }

        const searchFilter = this.getSearchAthletesFilterDto(searchTerm)
        return this.scoringApiService.getAthletes(searchFilter)
      })
    ).subscribe(results => {
      this.searchResults = results
    })
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

  private updateSearchTerm = (searchOn: string) => {
    if (searchOn !== '') {
      this.searchTerm = searchOn
      return
    }

    this.searchTerm = ''
    this.searchResults = []
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

  // public search(term: string): void {
  //   this.searchTerms.next(term);
  // }
}
