import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CustomPageDetailComponent } from './custom-page-detail.component';

describe('CustomPageDetailComponent', () => {
  let component: CustomPageDetailComponent;
  let fixture: ComponentFixture<CustomPageDetailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomPageDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomPageDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
