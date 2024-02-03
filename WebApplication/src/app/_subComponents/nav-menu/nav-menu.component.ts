import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient } from '@angular/common/http';
import { getHttpParams } from '../../_common/httpParamsHelpers';
import { config } from '../../config';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  templateUrl: './nav-menu.component.html',
  imports: [CommonModule, RouterOutlet, RouterModule, NgbModule],
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent extends ComponentBaseWithRoutes implements OnInit {

  constructor(
    private readonly http: HttpClient
  ) { super() }

  private searchTerms = new Subject<string>();

  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public searchRequested: boolean = false

  public searchResults: any

  ngOnInit() {
    const searchTerms$ = this.searchTerms.pipe(
      debounceTime(600),
      distinctUntilChanged(),
      tap(this.handleEmptySearchTerm),
      filter((searchTerm: string) => searchTerm !== ''),
      switchMap((searchTerm: string) => {
        const url = `${config.apiUrl}/SearchAllEntitiesSearchApi?searchTerm=${searchTerm}`
        const httpParams = getHttpParams(searchTerm)
        return this.http.get<any>(url, httpParams)
      })
    )

    searchTerms$.subscribe((searchResult: any) => {
      this.searchResults = searchResult
      this.searchRequested = true
    })
  }

  private handleEmptySearchTerm(searchTerm: string) {
    if (searchTerm !== '') {
      return
    }

    this.searchResults = null
    this.searchRequested = false
  }

  public search(term: string): void {
    this.searchTerms.next(term);
  }
}