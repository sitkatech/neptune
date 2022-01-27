import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectBasicsComponent } from './project-basics.component';

describe('ProjectBasicsComponent', () => {
  let component: ProjectBasicsComponent;
  let fixture: ComponentFixture<ProjectBasicsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectBasicsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectBasicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
