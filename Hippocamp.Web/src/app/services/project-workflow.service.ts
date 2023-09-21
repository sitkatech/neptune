import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectWorkflowService {

  public workflowUpdate = new BehaviorSubject(null);

  constructor() { }

  emitWorkflowUpdate(){
    this.workflowUpdate.next(null);
  }
}
