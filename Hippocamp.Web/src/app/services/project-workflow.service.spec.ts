import { TestBed } from '@angular/core/testing';

import { ProjectWorkflowService } from './project-workflow.service';

describe('ProjectWorkflowService', () => {
  let service: ProjectWorkflowService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectWorkflowService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
