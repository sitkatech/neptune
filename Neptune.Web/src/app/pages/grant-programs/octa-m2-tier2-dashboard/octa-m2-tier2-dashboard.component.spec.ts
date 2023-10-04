import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OCTAM2Tier2DashboardComponent } from './octa-m2-tier2-dashboard.component';

describe('OCTAM2Tier2DashboardComponent', () => {
  let component: OCTAM2Tier2DashboardComponent;
  let fixture: ComponentFixture<OCTAM2Tier2DashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OCTAM2Tier2DashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OCTAM2Tier2DashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
