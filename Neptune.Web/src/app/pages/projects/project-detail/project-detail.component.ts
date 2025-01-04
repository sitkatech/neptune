import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { AuthenticationService } from "src/app/services/authentication.service";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { PersonDto, ProjectDto, ProjectDocumentDto } from "src/app/shared/generated/model/models";
import { ProjectNetworkSolveHistorySimpleDto } from "src/app/shared/generated/model/project-network-solve-history-simple-dto";
import { TreatmentBMPUpsertDto } from "src/app/shared/generated/model/treatment-bmp-upsert-dto";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { GrantScoresComponent } from "../../../shared/components/projects/grant-scores/grant-scores.component";
import { AttachmentsDisplayComponent } from "../../../shared/components/projects/attachments-display/attachments-display.component";
import { ModelResultsComponent } from "../../../shared/components/projects/model-results/model-results.component";
import { TreatmentBmpMapEditorAndModelingAttributesComponent } from "../../../shared/components/projects/project-map/project-map.component";
import { FieldDefinitionComponent } from "../../../shared/components/field-definition/field-definition.component";
import { NgIf, DatePipe, AsyncPipe } from "@angular/common";
import { routeParams } from "src/app/app.routes";
import { Observable, combineLatest, map, of, forkJoin, switchMap } from "rxjs";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";

@Component({
    selector: "project-detail",
    templateUrl: "./project-detail.component.html",
    styleUrls: ["./project-detail.component.scss"],
    standalone: true,
    imports: [
        NgIf,
        RouterLink,
        FieldDefinitionComponent,
        TreatmentBmpMapEditorAndModelingAttributesComponent,
        ModelResultsComponent,
        AttachmentsDisplayComponent,
        GrantScoresComponent,
        DatePipe,
        AsyncPipe,
        PageHeaderComponent,
        AlertDisplayComponent,
    ],
})
export class ProjectDetailComponent implements OnInit {
    private currentUser: PersonDto;

    public currentProject$: Observable<ProjectDto>;
    public treatmentBMPs$: Observable<Array<TreatmentBMPUpsertDto>>;
    public delineations$: Observable<Array<DelineationUpsertDto>>;
    public projectNetworkSolveHistories$: Observable<Array<ProjectNetworkSolveHistorySimpleDto>>;
    public attachments$: Observable<Array<ProjectDocumentDto>>;
    public isReadOnly: boolean;
    public isCopyingProject = false;
    public modeledResultsData$: Observable<ModeledResultsDto>;

    constructor(
        private authenticationService: AuthenticationService,
        private projectService: ProjectService,
        private treatmentBMPService: TreatmentBMPService,
        private route: ActivatedRoute,
        private router: Router,
        private confirmService: ConfirmService,
        private alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.currentProject$ = this.route.params.pipe(
            switchMap((params) => {
                return this.projectService.projectsProjectIDGet(params[routeParams.projectID]);
            })
        );

        this.modeledResultsData$ = this.currentProject$.pipe(
            switchMap((project) => {
                return combineLatest({
                    treatmentBMPs$: this.treatmentBMPService.treatmentBMPsProjectIDGetByProjectIDGet(project.ProjectID),
                    delineations$: this.projectService.projectsProjectIDDelineationsGet(project.ProjectID),
                    projectNetworkSolveHistories$: this.projectService.projectsProjectIDProjectNetworkSolveHistoriesGet(project.ProjectID),
                });
            }),
            map((value) => {
                return {
                    treatmentBMPs: value.treatmentBMPs$,
                    delineations: value.delineations$,
                    projectNetworkSolveHistories: value.projectNetworkSolveHistories$,
                };
            })
        );

        this.attachments$ = this.currentProject$.pipe(
            switchMap((project) => {
                return this.projectService.projectsProjectIDAttachmentsGet(project.ProjectID);
            })
        );

        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;
            this.isReadOnly = this.authenticationService.isUserUnassigned(currentUser);
        });
    }

    getWorkflowLink(project: ProjectDto) {
        return `/projects/edit/${project.ProjectID}` + (project.ShareOCTAM2Tier2Scores ? "/review-and-share" : "");
    }

    makeProjectCopy(project: ProjectDto) {
        const modalContents = `<p>Are you sure you want to copy project <b>${project.ProjectName}</b>?
      The new copy will be assigned the same name with the addition of <em>- Copy</em> and the current time and date. 
      You can change the name in the project editing workflow afterwards.</p>
      <p>Note: Model results and attachments will not be copied.</p>`;
        this.confirmService
            .confirm({ buttonClassYes: "btn-hippocamp", buttonTextYes: "Copy", buttonTextNo: "Cancel", title: "Copy Project", message: modalContents })
            .then((confirmed) => {
                if (confirmed) {
                    this.isCopyingProject = true;
                    this.projectService.projectsProjectIDCopyPost(project.ProjectID).subscribe(
                        (newProjectID) => {
                            this.router.navigateByUrl(`/projects/${newProjectID}`).then(() => {
                                this.alertService.pushAlert(new Alert("Project successfully copied.", AlertContext.Success));
                            });
                        },
                        (error) => {
                            this.isCopyingProject = false;
                        }
                    );
                }
            });
    }
}

export interface ModeledResultsDto {
    treatmentBMPs: Array<TreatmentBMPUpsertDto>;
    delineations: Array<DelineationUpsertDto>;
    projectNetworkSolveHistories: Array<ProjectNetworkSolveHistorySimpleDto>;
}
