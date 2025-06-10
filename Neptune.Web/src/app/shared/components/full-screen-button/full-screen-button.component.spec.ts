import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullScreenButtonComponent } from './full-screen-button.component';

describe('FullScreenButtonComponent', () => {
  let component: FullScreenButtonComponent;
  let fixture: ComponentFixture<FullScreenButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FullScreenButtonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FullScreenButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
