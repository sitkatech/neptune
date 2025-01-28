import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransectLineLayerComponent } from './transect-line-layer.component';

describe('TransectLineLayerComponent', () => {
  let component: TransectLineLayerComponent;
  let fixture: ComponentFixture<TransectLineLayerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransectLineLayerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransectLineLayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
