import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { FieldDefinitionListComponent } from './field-definition-list.component';

describe('FieldDefinitionListComponent', () => {
  let component: FieldDefinitionListComponent;
  let fixture: ComponentFixture<FieldDefinitionListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldDefinitionListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldDefinitionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
