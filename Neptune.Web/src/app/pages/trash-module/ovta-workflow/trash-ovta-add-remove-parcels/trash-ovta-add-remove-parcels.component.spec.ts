import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrashOvtaAddRemoveParcelsComponent } from './trash-ovta-add-remove-parcels.component';

describe('TrashOvtaAddRemoveParcelsComponent', () => {
  let component: TrashOvtaAddRemoveParcelsComponent;
  let fixture: ComponentFixture<TrashOvtaAddRemoveParcelsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrashOvtaAddRemoveParcelsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrashOvtaAddRemoveParcelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
