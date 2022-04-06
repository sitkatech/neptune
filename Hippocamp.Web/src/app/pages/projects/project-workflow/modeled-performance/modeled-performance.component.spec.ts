import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeledPerformanceComponent } from './modeled-performance.component';

describe('ModeledPerformanceComponent', () => {
  let component: ModeledPerformanceComponent;
  let fixture: ComponentFixture<ModeledPerformanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModeledPerformanceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModeledPerformanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
