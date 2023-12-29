import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectInstructionsComponent } from './project-instructions.component';

describe('ProjectInstructionsComponent', () => {
  let component: ProjectInstructionsComponent;
  let fixture: ComponentFixture<ProjectInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectInstructionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
