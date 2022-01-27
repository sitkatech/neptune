import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressIconComponent } from './progress-icon.component';

describe('ProgressIconComponent', () => {
  let component: ProgressIconComponent;
  let fixture: ComponentFixture<ProgressIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgressIconComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgressIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
