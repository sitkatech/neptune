import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrashOvtaDetailComponent } from './trash-ovta-detail.component';

describe('TrashOvtaDetailComponent', () => {
  let component: TrashOvtaDetailComponent;
  let fixture: ComponentFixture<TrashOvtaDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrashOvtaDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrashOvtaDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
