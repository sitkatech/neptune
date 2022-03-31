import { TestBed } from '@angular/core/testing';

import { UnsavedChangesGuardGuard } from './unsaved-changes-guard.guard';

describe('UnsavedChangesGuardGuard', () => {
  let guard: UnsavedChangesGuardGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UnsavedChangesGuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
