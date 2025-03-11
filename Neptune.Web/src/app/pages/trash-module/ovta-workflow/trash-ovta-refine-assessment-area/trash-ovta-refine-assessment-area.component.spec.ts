import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrashOvtaRefineAssessmentAreaComponent } from './trash-ovta-refine-assessment-area.component';

describe('TrashOvtaRefineAssessmentAreaComponent', () => {
  let component: TrashOvtaRefineAssessmentAreaComponent;
  let fixture: ComponentFixture<TrashOvtaRefineAssessmentAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrashOvtaRefineAssessmentAreaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrashOvtaRefineAssessmentAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
