import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectNewSidebarComponent } from './project-new-sidebar.component';

describe('ProjectNewSidebarComponent', () => {
  let component: ProjectNewSidebarComponent;
  let fixture: ComponentFixture<ProjectNewSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectNewSidebarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectNewSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
