import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from "@angular/core";
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router";
import { Observable, Subscription } from "rxjs";
import { filter, tap } from "rxjs/operators";
import { ProjectWorkflowService } from "src/app/services/project-workflow.service";
import { DelineationUpsertDto } from "../../../generated/model/delineation-upsert-dto";
import { ProjectLoadReducingResultDto, ProjectWorkflowProgressDto } from "../../../generated/model/models";
import { ProjectUpsertDto } from "../../../generated/model/project-upsert-dto";
import { TreatmentBMPUpsertDto } from "../../../generated/model/treatment-bmp-upsert-dto";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { NgIf, AsyncPipe } from "@angular/common";
import { WorkflowNavComponent } from "../../workflow-nav/workflow-nav.component";
import { WorkflowNavItemComponent } from "../../workflow-nav/workflow-nav-item/workflow-nav-item.component";
import { ProjectWorkflowProgressService } from "src/app/shared/services/project-workflow-progress.service";
import { routeParams } from "src/app/app.routes";

@Component({
    selector: "project-wizard-sidebar",
    templateUrl: "./project-wizard-sidebar.component.html",
    styleUrls: ["./project-wizard-sidebar.component.scss"],
    standalone: true,
    imports: [NgIf, WorkflowNavComponent, WorkflowNavItemComponent, AsyncPipe],
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
