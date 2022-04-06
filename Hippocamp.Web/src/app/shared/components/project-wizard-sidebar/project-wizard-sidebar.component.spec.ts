import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectWizardSidebarComponent } from './project-wizard-sidebar.component';

describe('ProjectWizardSidebarComponent', () => {
  let component: ProjectWizardSidebarComponent;
  let fixture: ComponentFixture<ProjectWizardSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectWizardSidebarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectWizardSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
