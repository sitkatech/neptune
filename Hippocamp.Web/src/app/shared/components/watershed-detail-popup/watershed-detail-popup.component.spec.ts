import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WatershedDetailPopupComponent } from './watershed-detail-popup.component';

describe('WatershedDetailPopupComponent', () => {
  let component: WatershedDetailPopupComponent;
  let fixture: ComponentFixture<WatershedDetailPopupComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ WatershedDetailPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WatershedDetailPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
