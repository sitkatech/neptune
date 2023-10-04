import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CustomDropdownFilterComponent } from './custom-dropdown-filter.component';

describe('CustomDropdownFilterComponent', () => {
  let component: CustomDropdownFilterComponent;
  let fixture: ComponentFixture<CustomDropdownFilterComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomDropdownFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomDropdownFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
