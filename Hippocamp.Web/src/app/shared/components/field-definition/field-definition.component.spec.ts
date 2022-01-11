import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { FieldDefinitionComponent } from './field-definition.component';

describe('FieldDefinitionComponent', () => {
  let component: FieldDefinitionComponent;
  let fixture: ComponentFixture<FieldDefinitionComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldDefinitionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldDefinitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
