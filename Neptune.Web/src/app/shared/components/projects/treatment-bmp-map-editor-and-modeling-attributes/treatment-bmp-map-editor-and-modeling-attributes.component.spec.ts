import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TreatmentBmpMapEditorAndModelingAttributesComponent } from './treatment-bmp-map-editor-and-modeling-attributes.component';

describe('TreatmentBmpMapEditorAndModelingAttributesComponent', () => {
  let component: TreatmentBmpMapEditorAndModelingAttributesComponent;
  let fixture: ComponentFixture<TreatmentBmpMapEditorAndModelingAttributesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TreatmentBmpMapEditorAndModelingAttributesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TreatmentBmpMapEditorAndModelingAttributesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
