import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DelineationsComponent } from './delineations.component';

describe('DelineationsComponent', () => {
  let component: DelineationsComponent;
  let fixture: ComponentFixture<DelineationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DelineationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DelineationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
