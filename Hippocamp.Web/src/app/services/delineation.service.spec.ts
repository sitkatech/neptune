import { TestBed } from '@angular/core/testing';

import { DelineationService } from './delineation.service';

describe('DelineationService', () => {
  let service: DelineationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DelineationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
