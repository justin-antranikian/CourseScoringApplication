import { Component, Input, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { IrpSearchResultDto } from './IrpSearchResultDto';
import { SearchIrpsRequestDto, SearchOnField } from './SearchIrpsRequestDto';
import { HttpClient } from '@angular/common/http';
import { getHttpParams } from '../../_common/httpParamsHelpers';
import { config } from '../../config';
import { CommonModule } from '@angular/common';
import { IrpsSearchResultComponent } from './irps-search-result.component';
import { FormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  selector: 'app-irps-search',
  templateUrl: './irps-search.component.html',
  imports: [CommonModule, IrpsSearchResultComponent, FormsModule, NgbDropdownModule],
  styleUrls: ['./irps-search.component.css']
})
export class IrpsSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('courseId')
  public courseId!: number | null

  @Input('raceId')
  public raceId!: number | null

  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public hasResults: boolean = false
  public searchOn: string = 'bib'
  public searchTerm: string = ''
  private searchTerms = new Subject<string>();

  public searchResults: any[] | null = [];

  constructor(private readonly http: HttpClient) {
    super()
    this.mouseIsOver = false
  }

  ngOnInit() {
    const searchTerms$ = this.searchTerms.pipe(
      debounceTime(600),
      distinctUntilChanged(),
      tap(this.handleEmptySearchTerm),
      filter((searchTerm: string) => searchTerm !== ''),
      switchMap((searchTerm: string) => this.$searchIrps(searchTerm))
    )

    searchTerms$.subscribe((searchResults: any[]) => {
      this.searchResults = searchResults
      this.hasResults = true
    })
  }

  private handleEmptySearchTerm(searchTerm: string) {
    if (searchTerm !== '') {
      return
    }

    this.searchResults = null
    this.hasResults = false
  }

  private $searchIrps = (searchTerm: string): Observable<IrpSearchResultDto[]> => {
    const requestDto = this.getIrpSearchRequestDto(searchTerm)
    return this.searchIrps(requestDto)
  }

  public searchIrps(irpSearchRequest: SearchIrpsRequestDto): Observable<IrpSearchResultDto[]> {
    const httpParams = getHttpParams(irpSearchRequest.getAsParamsObject())
    return this.http.get<IrpSearchResultDto[]>(`${config.apiUrl}/searchIrpsApi`, httpParams)
  }

  private getIrpSearchRequestDto = (searchTerm: string): SearchIrpsRequestDto => {
    const fieldName = this.mapSearchTextToInt()
    const courseSearchRequestDto = new SearchIrpsRequestDto(fieldName, searchTerm, null, this.courseId)
    const raceSearchRequestDto = new SearchIrpsRequestDto(fieldName, searchTerm, this.raceId, null)
    return this.courseId ? courseSearchRequestDto : raceSearchRequestDto
  }

  private mapSearchTextToInt = (): SearchOnField => {
    switch(this.searchOn) {
      case 'bib': {
        return SearchOnField.Bib
      }
      case 'first name': {
        return SearchOnField.FirstName
      }
      case 'last name': {
        return SearchOnField.LastName
      }
      case 'full name': {
        return SearchOnField.FullName
      }
    }

    throw new Error('Cannot resolve search on field')
  }

  public search = (term: string) => {
    this.searchTerms.next(term);
  }

  public onSearchOnClicked = (searchOn: string) => {
    this.searchOn = searchOn
    this.searchTerm = ''
    this.hasResults = false
  }
}