import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { FieldDefinitionGridHeaderComponent } from './field-definition-grid-header.component';

describe('FieldDefinitionGridHeaderComponent', () => {
  let component: FieldDefinitionGridHeaderComponent;
  let fixture: ComponentFixture<FieldDefinitionGridHeaderComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldDefinitionGridHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldDefinitionGridHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
