import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { combineLatest, map, Observable, of, pipe, switchMap, tap } from "rxjs";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OrganizationSimpleDto, PersonDto, ProjectUpsertDtoForm, ProjectUpsertDtoFormControls } from "src/app/shared/generated/model/models";
import { AlertService } from "src/app/shared/services/alert.service";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { OrganizationService } from "src/app/shared/generated/api/organization.service";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { UserService } from "src/app/shared/generated/api/user.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { ProjectDto } from "src/app/shared/generated/model/models";
import { NgSelectModule } from "@ng-select/ng-select";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgIf, AsyncPipe } from "@angular/common";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { WorkflowBodyComponent } from "src/app/shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { routeParams } from "src/app/app.routes";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/form-field/form-field.component";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component";
import { ProjectWorkflowProgressService } from "src/app/shared/services/project-workflow-progress.service";
import { AuthenticationService } from "src/app/services/authentication.service";
import { SelectDropDownModule } from "ngx-select-dropdown";

@Component({
    selector: "project-basics",
    templateUrl: "./project-basics.component.html",
    styleUrls: ["./project-basics.component.scss"],
    standalone: true,
    imports: [
        NgIf,
        AsyncPipe,
        ReactiveFormsModule,
        FormsModule,
        FormFieldComponent,
        NgSelectModule,
        PageHeaderComponent,
        WorkflowBodyComponent,
        AlertDisplayComponent,
        SelectDropDownModule,
    ],
})
export class ProjectBasicsComponent implements OnInit {
    public FormFieldType = FormFieldType;

    public projectID: number;
    public organizationOptions$: Observable<SelectDropdownOption[]>;
    public userOptions$: Observable<SelectDropdownOption[]>;
    public stormwaterJurisdictionOptions$: Observable<SelectDropdownOption[]>;

    public invalidFields: Array<string> = [];
    public isLoadingSubmit = false;
    public customRichTextTypeID: number = NeptunePageTypeEnum.HippocampProjectBasics;
    public originalProjectModel: string;
    public projectBasicInfo$: Observable<ProjectDto>;

    public orgDropdownConfig = {
        search: true,
        height: "320px",
        placeholder: "Select an organization",
        searchFn: (option: SelectDropdownOption) => option.Label,
        displayFn: (option: SelectDropdownOption) => option.Label,
    };

    public contactDropdownConfig = {
        search: true,
        height: "320px",
        placeholder: "Select an contact",
        searchFn: (option: SelectDropdownOption) => option.Label,
        displayFn: (option: SelectDropdownOption) => option.Label,
    };

    public jurisdictionDropdownConfig = {
        search: true,
        height: "320px",
        placeholder: "Select an jurisdiction",
        searchFn: (option: SelectDropdownOption) => option.Label,
        displayFn: (option: SelectDropdownOption) => option.Label,
    };

    public selectedOrg: FormControl = new FormControl();
    public selectedContact: FormControl = new FormControl();
    public selectedJurisdiction: FormControl = new FormControl();

    public formGroup: FormGroup<ProjectUpsertDtoForm> = new FormGroup<any>({
        ProjectName: ProjectUpsertDtoFormControls.ProjectName(),
        ProjectDescription: ProjectUpsertDtoFormControls.ProjectDescription(),
        OrganizationID: ProjectUpsertDtoFormControls.OrganizationID(),
        StormwaterJurisdictionID: ProjectUpsertDtoFormControls.StormwaterJurisdictionID(),
        PrimaryContactPersonID: ProjectUpsertDtoFormControls.PrimaryContactPersonID(),
        CalculateOCTAM2Tier2Scores: ProjectUpsertDtoFormControls.CalculateOCTAM2Tier2Scores(),
        AdditionalContactInformation: ProjectUpsertDtoFormControls.AdditionalContactInformation(),
    });

    constructor(
        private authenticationService: AuthenticationService,
        private route: ActivatedRoute,
        private router: Router,
        private alertService: AlertService,
        private organizationService: OrganizationService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private projectService: ProjectService,
        private userService: UserService,
        private projectWorkflowProgressService: ProjectWorkflowProgressService
    ) {}

    canExit() {
        return !this.formGroup.dirty;
    }

    ngOnInit(): void {
        this.projectBasicInfo$ = this.route.params.pipe(
            switchMap((params) => {
                this.projectID = params[routeParams.projectID] ? parseInt(params[routeParams.projectID]) : null;
                return combineLatest({
                    CurrentUser: this.authenticationService.getCurrentUser(),
                    Project: this.projectID ? this.projectService.projectsProjectIDGet(params[routeParams.projectID]) : of(new ProjectDto()),
                });
            }),
            pipe(
                tap((value) => {
                    if (value.Project.ProjectID) {
                        // redirect to review step if project is shared with OCTA grant program
                        if (value.Project.ShareOCTAM2Tier2Scores) {
                            this.router.navigateByUrl(`/planning/projects/edit/${value.Project.ProjectID}/review-and-share`);
                        }
                        this.formGroup.controls.ProjectName.setValue(value.Project.ProjectName);
                        this.formGroup.controls.ProjectDescription.setValue(value.Project.ProjectDescription);
                        this.formGroup.controls.OrganizationID.setValue(value.Project.OrganizationID);
                        this.formGroup.controls.StormwaterJurisdictionID.setValue(value.Project.StormwaterJurisdictionID);
                        this.formGroup.controls.PrimaryContactPersonID.setValue(value.Project.PrimaryContactPersonID);
                        this.formGroup.controls.AdditionalContactInformation.setValue(value.Project.AdditionalContactInformation);
                        this.formGroup.controls.CalculateOCTAM2Tier2Scores.setValue(value.Project.CalculateOCTAM2Tier2Scores);
                        this.isLoadingSubmit = false;
                        this.selectedOrg.setValue({ Value: value.Project.OrganizationID, Label: value.Project.Organization.OrganizationName });
                        this.selectedContact.setValue({ Value: value.Project.PrimaryContactPersonID, Label: value.Project.PrimaryContactPerson.FullName });
                        this.selectedJurisdiction.setValue({
                            Value: value.Project.StormwaterJurisdictionID,
                            Label: value.Project.StormwaterJurisdiction.Organization.OrganizationName,
                        });
                    } else {
                        this.formGroup.controls.PrimaryContactPersonID.setValue(value.CurrentUser.PersonID);
                    }
                })
            ),
            map((value) => {
                return value.Project;
            })
        );

        this.organizationOptions$ = this.organizationService.organizationsGet().pipe(
            map((list) => {
                let options = list.map((x) => ({ Value: x.OrganizationID, Label: x.OrganizationName } as SelectDropdownOption));
                options = [{ Value: null, Label: "- Select -", Disabled: true }, ...options]; // insert an empty option at the front
                return options;
            })
        );
        this.userOptions$ = this.userService.usersGet().pipe(
            map((list) => {
                let options = list.map((x) => ({ Value: x.PersonID, Label: x.FullName } as SelectDropdownOption));
                options = [{ Value: null, Label: "- Select -", Disabled: true }, ...options]; // insert an empty option at the front
                return options;
            })
        );
        this.stormwaterJurisdictionOptions$ = this.stormwaterJurisdictionService.jurisdictionsGet().pipe(
            map((list) => {
                if (list.length == 1) {
                    return [{ Value: list[0].StormwaterJurisdictionID, Label: list[0].Organization.OrganizationName, Disabled: false }];
                }

                let options = list.map((x) => ({ Value: x.StormwaterJurisdictionID, Label: x.Organization.OrganizationName } as SelectDropdownOption));
                options = [{ Value: null, Label: "- Select -", Disabled: true }, ...options]; // insert an empty option at the front
                return options;
            })
        );
    }

    public onOrgSelected(event: any) {
        this.formGroup.controls.OrganizationID.patchValue(event.Value);
    }

    public onContactSelected(event: any) {
        this.formGroup.controls.PrimaryContactPersonID.patchValue(event.Value);
    }

    public onJurisdictionSelected(event: any) {
        this.formGroup.controls.StormwaterJurisdictionID.patchValue(event.Value);
    }

    public save(andContinue: boolean = false) {
        this.isLoadingSubmit = true;
        if (this.projectID) {
            this.projectService.projectsProjectIDUpdatePost(this.projectID, this.formGroup.getRawValue()).subscribe((response) => {
                this.isLoadingSubmit = false;
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Your project was successfully updated.", AlertContext.Success));
                this.projectWorkflowProgressService.updateProgress(this.projectID);
                //this.formGroup.patchValue(response);
                this.formGroup.markAsPristine();
                if (andContinue) {
                    this.router.navigate(["../stormwater-treatments/treatment-bmps"], { relativeTo: this.route });
                }
            });
        } else {
            this.projectService.projectsPost(this.formGroup.getRawValue()).subscribe((response) => {
                this.isLoadingSubmit = false;
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Your project was successfully created.", AlertContext.Success));
                this.projectWorkflowProgressService.updateProgress(response.ProjectID);
                this.formGroup.patchValue(response);
                this.formGroup.markAsPristine();
                if (andContinue) {
                    this.router.navigate([`../../edit/${response.ProjectID}/stormwater-treatments/treatment-bmps`], { relativeTo: this.route });
                }
            });
        }
    }
}
