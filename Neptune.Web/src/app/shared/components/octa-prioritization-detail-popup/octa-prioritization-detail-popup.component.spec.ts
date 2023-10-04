import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OctaPrioritizationDetailPopupComponent } from './octa-prioritization-detail-popup.component';

describe('OctaPrioritizationDetailPopupComponent', () => {
  let component: OctaPrioritizationDetailPopupComponent;
  let fixture: ComponentFixture<OctaPrioritizationDetailPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OctaPrioritizationDetailPopupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OctaPrioritizationDetailPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
