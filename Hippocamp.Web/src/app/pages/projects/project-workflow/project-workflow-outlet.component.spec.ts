import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectWorkflowOutletComponent } from './project-workflow-outlet.component';

describe('ProjectWorkflowOutletComponent', () => {
  let component: ProjectWorkflowOutletComponent;
  let fixture: ComponentFixture<ProjectWorkflowOutletComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectWorkflowOutletComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectWorkflowOutletComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
