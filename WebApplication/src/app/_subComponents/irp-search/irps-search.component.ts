import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { IrpSearchResultDto } from './IrpSearchResultDto';
import { SearchIrpsRequestDto, SearchOnField } from './SearchIrpsRequestDto';
import { HttpClient } from '@angular/common/http';
import { getHttpParams } from '../../_common/httpParamsHelpers';
import { config } from '../../config';
import { CommonModule } from '@angular/common';
import { IrpsSearchResultComponent } from './irps-search-result.component';
import { FormControl, FormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-irps-search',
  templateUrl: './irps-search.component.html',
  imports: [CommonModule, IrpsSearchResultComponent, FormsModule, NgbDropdownModule, ReactiveFormsModule],
  styleUrls: ['./irps-search.component.css']
})
export class IrpsSearchComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('courseId')
  public courseId!: number | null

  @Input('raceId')
  public raceId!: number | null

  public inputControl = new FormControl();

  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public searchOn: string = 'bib'
  public searchTerm: string = ''
  public $searchResults!: Observable<any>

  constructor(private readonly http: HttpClient) {
    super()
    this.mouseIsOver = false
  }

  ngOnInit() {
    this.$searchResults = this.inputControl.valueChanges.pipe(
      debounceTime(600),
      distinctUntilChanged(),
      filter((searchTerm: string) => searchTerm !== ''),
      switchMap((searchTerm: string) => this.$searchIrps(searchTerm)),
    )
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

  public onSearchOnClicked = (searchOn: string) => {
    this.searchOn = searchOn
    this.searchTerm = ''
  }
}