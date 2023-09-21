import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanningMapComponent } from './planning-map.component';

describe('PlanningMapComponent', () => {
  let component: PlanningMapComponent;
  let fixture: ComponentFixture<PlanningMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanningMapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanningMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
