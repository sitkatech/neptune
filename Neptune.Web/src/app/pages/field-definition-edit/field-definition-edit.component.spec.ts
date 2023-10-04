import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { FieldDefinitionEditComponent } from './field-definition-edit.component';

describe('FieldDefinitionEditComponent', () => {
  let component: FieldDefinitionEditComponent;
  let fixture: ComponentFixture<FieldDefinitionEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldDefinitionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldDefinitionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
