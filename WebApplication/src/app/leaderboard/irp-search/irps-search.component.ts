import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { IrpSearchResultDto } from './IrpSearchResultDto';
import { SearchIrpsRequestDto, SearchOnField } from './SearchIrpsRequestDto';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { ScoringApiService } from '../../services/scoring-api.service';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-irps-search',
  templateUrl: './irps-search.component.html',
  imports: [CommonModule, RouterModule, FormsModule, NgbDropdownModule, ReactiveFormsModule],
  styleUrls: ['./irps-search.component.css']
})
export class IrpsSearchComponent extends ComponentBaseWithRoutes implements OnInit, OnDestroy {

  @Input('courseId')
  public courseId!: number | null

  @Input('raceId')
  public raceId!: number | null

  public inputControl = new FormControl();
  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public searchOn: string = 'bib'
  public searchTerm: string | null = ''
  public searchResults: IrpSearchResultDto[] = []

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
        const requestDto = this.getIrpSearchRequestDto(searchTerm)
        return this.scoringApiService.getIrpsFromSearch(requestDto)
      })
    ).subscribe(results => {
      this.searchResults = results
    })
  }

  ngOnDestroy() {
    this.subscription!.unsubscribe();
  }

  private updateSearchTerm = (searchOn: string) => {
    if (searchOn !== '') {
      this.searchTerm = searchOn
      return
    }

    this.searchTerm = ''
    this.searchResults = []
  }

  public onSearchOnClicked = (searchOn: string) => {
    this.searchOn = searchOn
    this.inputControl.setValue('')
    this.searchTerm = ''
    this.searchResults = []
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
}