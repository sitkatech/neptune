import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrashOvtaIndexComponent } from './trash-ovta-index.component';

describe('TrashOvtaIndexComponent', () => {
  let component: TrashOvtaIndexComponent;
  let fixture: ComponentFixture<TrashOvtaIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrashOvtaIndexComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrashOvtaIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
