import { TestBed } from '@angular/core/testing';

import { ScoringApiService } from './scoring-api.service';

describe('ScoringApiService', () => {
  let service: ScoringApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScoringApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
