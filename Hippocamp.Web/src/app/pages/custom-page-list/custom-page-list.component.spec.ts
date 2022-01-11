import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CustomPageListComponent } from './custom-page-list.component';

describe('CustomPageListComponent', () => {
  let component: CustomPageListComponent;
  let fixture: ComponentFixture<CustomPageListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomPageListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomPageListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
