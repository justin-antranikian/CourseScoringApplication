import { TestBed } from '@angular/core/testing';

import { ApiRequestService } from './api-request.service';

describe('ApiRequestService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApiRequestService = TestBed.get(ApiRequestService);
    expect(service).toBeTruthy();
  });
});
