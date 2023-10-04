import { TestBed } from '@angular/core/testing';

import { JurisdictionManagerOrEditorOnlyGuard } from './jurisdiction-manager-or-editor-only-guard.guard';

describe('JurisdictionManagerOrEditorOnlyGuardGuard', () => {
  let guard: JurisdictionManagerOrEditorOnlyGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(JurisdictionManagerOrEditorOnlyGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
