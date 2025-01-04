import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { forkJoin } from "rxjs";
import { AuthenticationService } from "src/app/services/authentication.service";
import { ProjectWorkflowService } from "src/app/services/project-workflow.service";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { ProjectDocumentDto, ProjectDto, ProjectNetworkSolveHistorySimpleDto } from "src/app/shared/generated/model/models";
import { ProjectUpsertDto } from "src/app/shared/generated/model/project-upsert-dto";
import { TreatmentBMPUpsertDto } from "src/app/shared/generated/model/treatment-bmp-upsert-dto";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { GrantScoresComponent } from "../../../../shared/components/projects/grant-scores/grant-scores.component";
import { AttachmentsDisplayComponent } from "../../../../shared/components/projects/attachments-display/attachments-display.component";
import { ModelResultsComponent } from "../../../../shared/components/projects/model-results/model-results.component";
import { TreatmentBmpMapEditorAndModelingAttributesComponent } from "../../../../shared/components/projects/project-map/project-map.component";
import { NgIf, NgClass, DatePipe } from "@angular/common";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";

@Component({
    selector: "review",
    templateUrl: "./review.component.html",
    styleUrls: ["./review.component.scss"],
    standalone: true,
    imports: [
        CustomRichTextComponent,
        NgIf,
        NgClass,
        TreatmentBmpMapEditorAndModelingAttributesComponent,
        ModelResultsComponent,
        AttachmentsDisplayComponent,
        GrantScoresComponent,
        DatePipe,
    ],
})
export class ReviewComponent implements OnInit {
    public projectID: number;
    public project: ProjectDto;
    public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
    public delineations: Array<DelineationUpsertDto>;
    public projectNetworkSolveHistories: Array<ProjectNetworkSolveHistorySimpleDto>;
    public attachments: Array<ProjectDocumentDto>;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampReview;

    public isLoadingSubmit = false;

    constructor(
        private authenticationService: AuthenticationService,
        private projectService: ProjectService,
        private treatmentBMPService: TreatmentBMPService,
        private route: ActivatedRoute,
        private alertService: AlertService,
        private confirmService: ConfirmService,
        private projectWorkflowService: ProjectWorkflowService
    ) {}

    ngOnInit(): void {
        this.authenticationService.getCurrentUser().subscribe((result) => {
            const projectID = this.route.snapshot.paramMap.get("projectID");
            if (projectID) {
                this.projectID = parseInt(projectID);
                forkJoin({
                    project: this.projectService.projectsProjectIDGet(this.projectID),
                    treatmentBMPs: this.treatmentBMPService.treatmentBMPsProjectIDGetByProjectIDGet(this.projectID),
                    delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
                    projectNetworkSolveHistories: this.projectService.projectsProjectIDProjectNetworkSolveHistoriesGet(this.projectID),
                    attachments: this.projectService.projectsProjectIDAttachmentsGet(this.projectID),
                }).subscribe(({ project, treatmentBMPs, delineations, projectNetworkSolveHistories, attachments }) => {
                    this.treatmentBMPs = treatmentBMPs;
                    this.delineations = delineations;
                    this.projectNetworkSolveHistories = projectNetworkSolveHistories;
                    this.project = project;
                    this.attachments = attachments;
                });
            }
        });
    }

    showModelResultsPanel(): boolean {
        return !this.project?.DoesNotIncludeTreatmentBMPs && this.project.HasModeledResults;
    }

    private mapProjectToUpsertDto(): ProjectUpsertDto {
        return new ProjectUpsertDto({
            ProjectName: this.project.ProjectName,
            OrganizationID: this.project.OrganizationID,
            StormwaterJurisdictionID: this.project.StormwaterJurisdictionID,
            PrimaryContactPersonID: this.project.PrimaryContactPersonID,
            ProjectDescription: this.project.ProjectDescription,
            AdditionalContactInformation: this.project.AdditionalContactInformation,
            DoesNotIncludeTreatmentBMPs: this.project.DoesNotIncludeTreatmentBMPs,
            CalculateOCTAM2Tier2Scores: this.project.CalculateOCTAM2Tier2Scores,
        });
    }

    shareOrRevokeOCTAScores() {
        var modalContents =
            "<p>You are about to revoke sharing of this project with the OCTA M2 Tier 2 grant program. This will allow you to edit this project." +
            "<p>Are you sure you wish to proceed?</p>";
        var buttonTextYes = "Revoke";
        var canSubmit = true;

        if (!this.project.ShareOCTAM2Tier2Scores) {
            buttonTextYes = "Share";

            canSubmit =
                this.project.CalculateOCTAM2Tier2Scores &&
                (this.treatmentBMPs.length > 0 ? this.projectNetworkSolveHistories.length > 0 : this.project.DoesNotIncludeTreatmentBMPs);

            modalContents = canSubmit
                ? "<p>I certify that I have inventoried all upstream BMPs of my project within the OC Stormwater Tools Inventory Module and made them ready for modeling.</p>"
                : "<p>You are required to check the box to view OCTA M2 Tier 2 Metrics on the Basics step, and to calculate OCTA Metrics for Treatment BMPs before the project can be shared and submitted to the grant agency.</p>";
        }

        this.confirmService
            .confirm({
                buttonClassYes: "btn-hippocamp",
                //buttonDisabledYes: !canSubmit,
                buttonTextYes: buttonTextYes,
                buttonTextNo: "Cancel",
                title: `${buttonTextYes} Project`,
                message: modalContents,
            })
            .then((confirmed) => {
                if (confirmed) {
                    this.isLoadingSubmit = true;

                    var model = this.mapProjectToUpsertDto();
                    model.ShareOCTAM2Tier2Scores = !this.project.ShareOCTAM2Tier2Scores;

                    this.projectService.projectsProjectIDUpdatePost(this.projectID, model).subscribe(
                        () => {
                            this.isLoadingSubmit = false;
                            this.projectWorkflowService.emitWorkflowUpdate();
                            this.project.ShareOCTAM2Tier2Scores = !this.project.ShareOCTAM2Tier2Scores;
                            this.alertService.pushAlert(new Alert(`Your project was successfully ${buttonTextYes == "Share" ? "shared" : "revoked"}.`, AlertContext.Success));
                            window.scroll(0, 0);
                        },
                        (error) => {
                            this.isLoadingSubmit = false;
                            window.scroll(0, 0);
                        }
                    );
                }
            });
    }
}
