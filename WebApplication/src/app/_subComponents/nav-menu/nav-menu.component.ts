import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap, tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ScoringApiService } from '../../services/scoring-api.service';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  templateUrl: './nav-menu.component.html',
  imports: [CommonModule, RouterOutlet, RouterModule, NgbModule, ReactiveFormsModule],
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent extends ComponentBaseWithRoutes implements OnInit, OnDestroy {

  constructor(
    private scoringApiService: ScoringApiService
  ) { super() }

  public inputControl = new FormControl();
  public mouseIsOver: boolean = false
  public searchIsFocused: boolean = false
  public searchTerm: string | null = ''

  public searchResults: any
  private subscription: Subscription | null = null

  ngOnInit() {
    this.subscription = this.inputControl.valueChanges.pipe(
      debounceTime(600),
      distinctUntilChanged(),
      tap(this.updateSearchTerm),
      filter((searchTerm: string) => searchTerm !== ''),
      switchMap((searchTerm: string) => {
        return this.scoringApiService.searchAllEntities(searchTerm)
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
}