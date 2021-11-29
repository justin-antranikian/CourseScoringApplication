import { Component, Input, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { SearchOnField, SearchIrpsRequestDto } from 'src/app/_orchestration/searchIrps/searchIrpsRequestDto';
import { IrpSearchResultDto } from 'src/app/_orchestration/searchIrps/irpSearchResultDto';
import { ApiRequestService } from 'src/app/_services/api-request.service';

@Component({
  selector: 'app-irps-search',
  templateUrl: './irps-search.component.html',
  styleUrls: ['./irps-search.component.css']
})
export class IrpsSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('courseId')
  public courseId: number | undefined

  @Input('raceId')
  public raceId: number | undefined

  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public hasResults: boolean = false
  public searchOn: string = 'bib'
  public searchTerm: string = ''
  private searchTerms = new Subject<string>();

  public searchResults: IrpSearchResultDto[] = [];

  constructor(
    private apiService: ApiRequestService
  ) { super() }

  ngOnInit() {
    const searchTerms$ = this.searchTerms.pipe(
      debounceTime(600),
      distinctUntilChanged(),
      tap(this.handleEmptySearchTerm),
      filter((searchTerm: string) => searchTerm !== ''),
      switchMap((searchTerm: string) => this.$searchIrps(searchTerm))
    )

    searchTerms$.subscribe((searchResults: IrpSearchResultDto[]) => {
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
    return this.apiService.searchIrps(requestDto)
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
