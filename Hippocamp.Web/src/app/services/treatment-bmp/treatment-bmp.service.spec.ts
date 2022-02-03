import { TestBed } from '@angular/core/testing';

import { TreatmentBMPService } from './treatment-bmp.service';

describe('TreatmentBMPService', () => {
  let service: TreatmentBMPService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TreatmentBMPService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
