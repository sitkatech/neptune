import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WatershedDetailComponent } from './watershed-detail.component';

describe('WatershedDetailComponent', () => {
  let component: WatershedDetailComponent;
  let fixture: ComponentFixture<WatershedDetailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ WatershedDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WatershedDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
