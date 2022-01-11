import { TestBed } from '@angular/core/testing';

import { WatershedService } from './watershed.service';

describe('TractService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WatershedService = TestBed.get(WatershedService);
    expect(service).toBeTruthy();
  });
});
