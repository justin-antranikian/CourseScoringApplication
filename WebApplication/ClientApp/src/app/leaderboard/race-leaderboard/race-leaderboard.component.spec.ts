import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaceLeaderboardComponent } from './race-leaderboard.component';

describe('RaceLeaderboardComponent', () => {
  let component: RaceLeaderboardComponent;
  let fixture: ComponentFixture<RaceLeaderboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaceLeaderboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaceLeaderboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
