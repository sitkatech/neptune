import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CustomPageCreateComponent } from './custom-page-create.component';

describe('CustomPageCreateComponent', () => {
  let component: CustomPageCreateComponent;
  let fixture: ComponentFixture<CustomPageCreateComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomPageCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomPageCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
