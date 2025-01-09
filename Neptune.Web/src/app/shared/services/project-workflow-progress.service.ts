import { Injectable } from "@angular/core";
import { Observable, ReplaySubject, Subject, Subscription } from "rxjs";
import { ProjectWorkflowProgressDto, ProjectWorkflowProgressDtoSteps } from "../generated/model/models";
import { ActivatedRoute, Router } from "@angular/router";
import { ProjectService } from "../generated/api/project.service";

@Injectable({
    providedIn: "root",
})
export class ProjectWorkflowProgressService {
    private progressSubject: Subject<ProjectWorkflowProgressDto> = new ReplaySubject();
    public progressObservable$: Observable<ProjectWorkflowProgressDto> = this.progressSubject.asObservable();

    private progressSubscription = Subscription.EMPTY;

    constructor(private projectService: ProjectService, private route: ActivatedRoute, private router: Router) {}

    updateProgress(projectID: number): void {
        this.progressSubscription.unsubscribe();
        this.getProgress(projectID);
    }

    getProgress(projectID: number) {
        if (projectID) {
            this.progressSubscription = this.projectService.projectsProjectIDProgressGet(projectID).subscribe((response) => {
                this.progressSubject.next(response);
            });
        } else {
            this.progressSubject.next(new ProjectWorkflowProgressDto({ Steps: new ProjectWorkflowProgressDtoSteps() }));
        }
    }
}
