import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CustomPageEditPropertiesComponent } from './custom-page-edit-properties.component';

describe('CustomPageEditPropertiesComponent', () => {
  let component: CustomPageEditPropertiesComponent;
  let fixture: ComponentFixture<CustomPageEditPropertiesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomPageEditPropertiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomPageEditPropertiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
