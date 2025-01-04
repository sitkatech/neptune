import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { forkJoin } from "rxjs";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AuthenticationService } from "src/app/services/authentication.service";
import { OrganizationSimpleDto } from "src/app/shared/generated/model/organization-simple-dto";
import { ProjectUpsertDto } from "src/app/shared/generated/model/project-upsert-dto";
import { AlertService } from "src/app/shared/services/alert.service";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { OrganizationService } from "src/app/shared/generated/api/organization.service";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { UserService } from "src/app/shared/generated/api/user.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PersonDto, ProjectDto, StormwaterJurisdictionDto } from "src/app/shared/generated/model/models";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { FormsModule } from "@angular/forms";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";
import { NgIf, NgClass } from "@angular/common";

@Component({
    selector: "project-basics",
    templateUrl: "./project-basics.component.html",
    styleUrls: ["./project-basics.component.scss"],
    standalone: true,
    imports: [NgIf, CustomRichTextComponent, FormsModule, NgClass, NgSelectModule, FieldDefinitionComponent],
})
export class ProjectBasicsComponent implements OnInit {
    public currentUser: PersonDto;

    public projectID: number;
    public projectModel: ProjectUpsertDto;
    public organizations: Array<OrganizationSimpleDto>;
    public users: Array<PersonDto>;
    public stormwaterJurisdictions: Array<StormwaterJurisdictionDto>;
    public invalidFields: Array<string> = [];
    public isLoadingSubmit = false;
    public customRichTextTypeID: number = NeptunePageTypeEnum.HippocampProjectBasics;
    public originalProjectModel: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private cdr: ChangeDetectorRef,
        private authenticationService: AuthenticationService,
        private alertService: AlertService,
        private organizationService: OrganizationService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private projectService: ProjectService,
        private userService: UserService
    ) {}

    canExit() {
        return this.originalProjectModel == JSON.stringify(this.projectModel);
    }

    ngOnInit(): void {
        const projectID = this.route.snapshot.paramMap.get("projectID");
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;
            this.projectModel = new ProjectUpsertDto();
            if (projectID) {
                this.projectID = parseInt(projectID);
                this.projectService.projectsProjectIDGet(this.projectID).subscribe((project) => {
                    // redirect to review step if project is shared with OCTA grant program
                    if (project.ShareOCTAM2Tier2Scores) {
                        this.router.navigateByUrl(`projects/edit/${projectID}/review-and-share`);
                    }

                    this.mapProjectSimpleDtoToProjectModel(project);
                    this.originalProjectModel = JSON.stringify(this.projectModel);
                });
            } else {
                this.projectModel.PrimaryContactPersonID = this.currentUser.PersonID;
                this.originalProjectModel = "";
            }

            forkJoin({
                organizations: this.organizationService.organizationsGet(),
                stormwaterJurisdictions: this.stormwaterJurisdictionService.jurisdictionsPersonIDGet(this.currentUser.PersonID),
                users: this.userService.usersGet(),
            }).subscribe(({ organizations, stormwaterJurisdictions, users }) => {
                this.organizations = organizations;
                this.stormwaterJurisdictions = stormwaterJurisdictions;
                this.users = users;

                if (stormwaterJurisdictions.length == 1) {
                    this.projectModel.StormwaterJurisdictionID = stormwaterJurisdictions[0].StormwaterJurisdictionID;
                }
            });

            this.cdr.detectChanges();
        });
    }

    ngOnDestroy() {
        this.cdr.detach();
    }

    private mapProjectSimpleDtoToProjectModel(project: ProjectDto) {
        this.projectModel.ProjectName = project.ProjectName;
        this.projectModel.OrganizationID = project.OrganizationID;
        this.projectModel.StormwaterJurisdictionID = project.StormwaterJurisdictionID;
        this.projectModel.PrimaryContactPersonID = project.PrimaryContactPersonID;
        this.projectModel.ProjectDescription = project.ProjectDescription;
        this.projectModel.AdditionalContactInformation = project.AdditionalContactInformation;
        this.projectModel.CalculateOCTAM2Tier2Scores = project.CalculateOCTAM2Tier2Scores;
        this.projectModel.ShareOCTAM2Tier2Scores = project.ShareOCTAM2Tier2Scores;
        this.projectModel.DoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs;
    }

    public isFieldInvalid(fieldName: string) {
        return this.invalidFields.indexOf(fieldName) > -1;
    }

    private onSubmitSuccess(successMessage: string, projectID: number, continueToNextStep?: boolean) {
        this.isLoadingSubmit = false;
        this.originalProjectModel = JSON.stringify(this.projectModel);

        const rerouteURL = continueToNextStep ? `/projects/edit/${projectID}/stormwater-treatments/treatment-bmps` : `/projects/edit/${projectID}/project-basics`;

        this.router.navigateByUrl(rerouteURL).then(() => {
            this.alertService.pushAlert(new Alert(successMessage, AlertContext.Success));
        });
        window.scroll(0, 0);
    }

    private onSubmitFailure(error) {
        if (error.error?.errors) {
            for (let key of Object.keys(error.error.errors)) {
                this.invalidFields.push(key);
            }
        }
        this.isLoadingSubmit = false;
        window.scroll(0, 0);
        this.cdr.detectChanges();
    }

    public onSubmit(continueToNextStep?: boolean): void {
        this.isLoadingSubmit = true;
        this.invalidFields = [];
        this.alertService.clearAlerts();

        if (this.projectID) {
            this.projectService.projectsProjectIDUpdatePost(this.projectID, this.projectModel).subscribe(
                () => {
                    this.onSubmitSuccess("Your project was successfully updated.", this.projectID, continueToNextStep);
                },
                (error) => {
                    this.onSubmitFailure(error);
                }
            );
        } else {
            this.projectService.projectsNewPost(this.projectModel).subscribe(
                (project) => {
                    this.onSubmitSuccess("Your project was successfully created.", project.ProjectID, continueToNextStep);
                },
                (error) => {
                    this.onSubmitFailure(error);
                }
            );
        }
    }
}
