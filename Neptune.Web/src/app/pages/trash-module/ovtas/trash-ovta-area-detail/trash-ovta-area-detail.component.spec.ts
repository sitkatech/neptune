import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrashOvtaAreaDetailComponent } from './trash-ovta-area-detail.component';

describe('TrashOvtaAreaDetailComponent', () => {
  let component: TrashOvtaAreaDetailComponent;
  let fixture: ComponentFixture<TrashOvtaAreaDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrashOvtaAreaDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrashOvtaAreaDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
