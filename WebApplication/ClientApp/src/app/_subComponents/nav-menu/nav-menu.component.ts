import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { ApiRequestService } from 'src/app/_services/api-request.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent extends ComponentBaseWithRoutes implements OnInit {

  constructor(
    private readonly apiService: ApiRequestService
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
        return this.apiService.searchAllEntities(searchTerm)
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
