import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TreatmentBmpsComponent } from './treatment-bmps.component';

describe('TreatmentBmpsComponent', () => {
  let component: TreatmentBmpsComponent;
  let fixture: ComponentFixture<TreatmentBmpsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TreatmentBmpsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TreatmentBmpsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
