import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrashOvtaInstructionsComponent } from './trash-ovta-instructions.component';

describe('TrashOvtaInstructionsComponent', () => {
  let component: TrashOvtaInstructionsComponent;
  let fixture: ComponentFixture<TrashOvtaInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrashOvtaInstructionsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrashOvtaInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
