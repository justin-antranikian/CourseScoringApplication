import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseLeaderboardComponent } from './course-leaderboard.component';

describe('CourseLeaderboardComponent', () => {
  let component: CourseLeaderboardComponent;
  let fixture: ComponentFixture<CourseLeaderboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CourseLeaderboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseLeaderboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
