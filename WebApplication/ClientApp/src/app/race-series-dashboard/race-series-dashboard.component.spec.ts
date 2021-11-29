import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaceSeriesDashboardComponent } from './race-series-dashboard.component';

describe('RaceSeriesComponent', () => {
  let component: RaceSeriesDashboardComponent;
  let fixture: ComponentFixture<RaceSeriesDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaceSeriesDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaceSeriesDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
