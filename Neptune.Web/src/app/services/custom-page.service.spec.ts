import { TestBed } from '@angular/core/testing';

import { CustomPageService } from './custom-page.service';

describe('CustomPageService', () => {
  let service: CustomPageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomPageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
