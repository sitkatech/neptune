import { TestBed } from '@angular/core/testing';

import { StormwaterJurisdictionService } from './stormwater-jurisdiction.service';

describe('StormwaterJurisdictionService', () => {
  let service: StormwaterJurisdictionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StormwaterJurisdictionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
