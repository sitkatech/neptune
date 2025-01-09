import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from "@angular/core";
import { ActivatedRoute, NavigationEnd, Router, RouterLinkActive, RouterLink } from "@angular/router";
import { forkJoin, Observable, Subscription } from "rxjs";
import { filter, tap } from "rxjs/operators";
import { AuthenticationService } from "src/app/services/authentication.service";
import { ProjectWorkflowService } from "src/app/services/project-workflow.service";
import { DelineationUpsertDto } from "../../../generated/model/delineation-upsert-dto";
import { ProjectLoadReducingResultDto, ProjectWorkflowProgressDto } from "../../../generated/model/models";
import { PersonDto } from "../../../generated/model/person-dto";
import { ProjectUpsertDto } from "../../../generated/model/project-upsert-dto";
import { TreatmentBMPUpsertDto } from "../../../generated/model/treatment-bmp-upsert-dto";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { ProgressIconComponent } from "../../progress-icon/progress-icon.component";
import { NgIf, NgClass, AsyncPipe } from "@angular/common";
import { WorkflowNavComponent } from "../../workflow-nav/workflow-nav.component";
import { WorkflowNavItemComponent } from "../../workflow-nav/workflow-nav-item/workflow-nav-item.component";
import { ProjectWorkflowProgressService } from "src/app/shared/services/project-workflow-progress.service";
import { routeParams } from "src/app/app.routes";

@Component({
    selector: "project-wizard-sidebar",
    templateUrl: "./project-wizard-sidebar.component.html",
    styleUrls: ["./project-wizard-sidebar.component.scss"],
    standalone: true,
    imports: [NgIf, RouterLinkActive, RouterLink, ProgressIconComponent, NgClass, WorkflowNavComponent, WorkflowNavItemComponent, AsyncPipe],
})
export class ProjectWizardSidebarComponent implements OnInit, OnChanges, OnDestroy {
    @Input() projectModel: ProjectUpsertDto;

    public isCreating: boolean;
    public submitted: boolean = false;

    private _routeListener: Subscription;
    private workflowUpdateSubscription: Subscription;
    public progress$: Observable<ProjectWorkflowProgressDto>;

    public projectID: number;
    public treatmentBMPs: TreatmentBMPUpsertDto[];
    public delineations: DelineationUpsertDto[];
    public projectLoadReducingResults: ProjectLoadReducingResultDto[];

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private cdr: ChangeDetectorRef,
        private treatmentBMPService: TreatmentBMPService,
        private projectService: ProjectService,
        private projectWorkflowService: ProjectWorkflowService,
        private projectProgressService: ProjectWorkflowProgressService
    ) {}

    ngOnChanges(changes: SimpleChanges): void {
        this.cdr.detectChanges();
    }

    ngOnInit() {
        this.checkForProjectIDInRouteAndGetEntitiesIfPresent();
        this._routeListener = this.router.events.pipe(filter((e) => e instanceof NavigationEnd)).subscribe(() => {
            this.checkForProjectIDInRouteAndGetEntitiesIfPresent();
        });
        this.workflowUpdateSubscription = this.projectWorkflowService.workflowUpdate.subscribe(() => {
            this.checkForProjectIDInRouteAndGetEntitiesIfPresent();
        });
        this.progress$ = this.projectProgressService.progressObservable$.pipe(
            tap((x) => {
                this.submitted = false;
            })
        );
        this.projectProgressService.getProgress(this.projectID);
    }

    ngOnDestroy() {
        this.workflowUpdateSubscription?.unsubscribe();
    }

    checkForProjectIDInRouteAndGetEntitiesIfPresent() {
        const projectID = this.route.snapshot.paramMap.get("projectID");
        if (projectID) {
            this.projectID = parseInt(projectID);
            this.getProjectRelatedEntities();
        }
    }

    doesProjectHaveTreatmentBMPs(): boolean {
        return this.treatmentBMPs?.length > 0;
    }

    getProjectRelatedEntities() {
        forkJoin({
            treatmentBMPs: this.treatmentBMPService.treatmentBMPsProjectIDGetByProjectIDGet(this.projectID),
            delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
            modeledResults: this.projectService.projectsProjectIDLoadReducingResultsGet(this.projectID),
        }).subscribe(({ treatmentBMPs, delineations, modeledResults }) => {
            this.treatmentBMPs = treatmentBMPs;
            this.delineations = delineations;
            this.projectLoadReducingResults = modeledResults;
            this.cdr.detectChanges();
        });
    }

    isProjectBasicsComplete(): boolean {
        let model = this.projectModel;
        return (
            model &&
            model.ProjectName != null &&
            model.ProjectName != undefined &&
            model.ProjectName != "" &&
            model.OrganizationID != null &&
            model.OrganizationID != undefined &&
            model.StormwaterJurisdictionID != null &&
            model.StormwaterJurisdictionID != undefined &&
            model.PrimaryContactPersonID != null &&
            model.PrimaryContactPersonID != undefined
        );
    }

    isStormwaterTreatmentsComplete(): boolean {
        if (this.treatmentBMPs == null || this.treatmentBMPs == undefined || this.treatmentBMPs.length == 0) {
            return this.projectModel.DoesNotIncludeTreatmentBMPs;
        }

        return this.doAllTreatmentBMPsHaveModelingParameters() && this.doAllTreatmentBMPsHaveDelineations() && this.doAllTreatmentBMPsHaveCalculatedModelResults();
    }

    doAllTreatmentBMPsHaveModelingParameters(): boolean {
        if (this.treatmentBMPs == null || this.treatmentBMPs == undefined || this.treatmentBMPs.length == 0) {
            return this.projectModel.DoesNotIncludeTreatmentBMPs;
        }

        return this.treatmentBMPs.every((x) => x.AreAllModelingAttributesComplete);
    }

    doAllTreatmentBMPsHaveDelineations(): boolean {
        if (this.treatmentBMPs == null || this.treatmentBMPs == undefined || this.treatmentBMPs.length == 0) {
            return this.projectModel.DoesNotIncludeTreatmentBMPs;
        }

        if (this.delineations == null || this.delineations == undefined || this.delineations.length == 0 || this.treatmentBMPs.length != this.delineations.length) {
            return false;
        }

        return this.treatmentBMPs.every((x) => this.delineations.some((y) => y.TreatmentBMPID == x.TreatmentBMPID));
    }

    doAllTreatmentBMPsHaveCalculatedModelResults(): boolean {
        if (this.treatmentBMPs == null || this.treatmentBMPs == undefined || this.treatmentBMPs.length == 0) {
            return this.projectModel.DoesNotIncludeTreatmentBMPs;
        }

        if (
            this.projectLoadReducingResults == null ||
            this.projectLoadReducingResults == undefined ||
            this.projectLoadReducingResults.length == 0 ||
            this.treatmentBMPs.length != this.projectLoadReducingResults.length
        ) {
            return false;
        }

        return this.treatmentBMPs.every((x) => this.projectLoadReducingResults.some((y) => y.TreatmentBMPID == x.TreatmentBMPID));
    }
}
