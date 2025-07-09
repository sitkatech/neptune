import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { ProjectWorkflowProgressDto } from "src/app/shared/generated/model/models";
import { NgIf, AsyncPipe } from "@angular/common";
import { WorkflowNavComponent } from "src/app/shared/components/workflow-nav/workflow-nav.component";
import { WorkflowNavItemComponent } from "src/app/shared/components/workflow-nav/workflow-nav-item/workflow-nav-item.component";
import { ProjectWorkflowProgressService } from "src/app/shared/services/project-workflow-progress.service";
import { routeParams } from "src/app/app.routes";

@Component({
    selector: "project-wizard-sidebar",
    templateUrl: "./project-wizard-sidebar.component.html",
    styleUrls: ["./project-wizard-sidebar.component.scss"],
    imports: [NgIf, WorkflowNavComponent, WorkflowNavItemComponent, AsyncPipe]
})
export class ProjectWizardSidebarComponent implements OnInit {
    public submitted: boolean = false;
    public progress$: Observable<ProjectWorkflowProgressDto>;
    public projectID: number;

    constructor(private route: ActivatedRoute, private projectProgressService: ProjectWorkflowProgressService) {}

    ngOnInit() {
        this.projectID = this.route.snapshot.paramMap.get(routeParams.projectID) ? parseInt(this.route.snapshot.paramMap.get(routeParams.projectID)) : null;
        this.progress$ = this.projectProgressService.progressObservable$.pipe(
            tap((x) => {
                this.submitted = false;
            })
        );
        this.projectProgressService.getProgress(this.projectID);
    }
}
