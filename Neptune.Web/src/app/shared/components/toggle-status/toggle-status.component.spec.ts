import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToggleStatusComponent } from './toggle-status.component';

describe('ToggleStatusComponent', () => {
  let component: ToggleStatusComponent;
  let fixture: ComponentFixture<ToggleStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToggleStatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToggleStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
