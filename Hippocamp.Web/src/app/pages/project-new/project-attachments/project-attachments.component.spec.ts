import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectAttachmentsComponent } from './project-attachments.component';

describe('ProjectAttachmentsComponent', () => {
  let component: ProjectAttachmentsComponent;
  let fixture: ComponentFixture<ProjectAttachmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectAttachmentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectAttachmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
