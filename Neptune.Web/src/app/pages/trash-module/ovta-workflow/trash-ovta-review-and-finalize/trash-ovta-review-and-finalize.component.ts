import { Component } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FormFieldComponent, FormFieldType, FormInputOption } from "../../../../shared/components/form-field/form-field.component";
import { OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions } from "src/app/shared/generated/enum/onland-visual-trash-assessment-score-enum";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable, switchMap, tap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { AsyncPipe, NgFor, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentObservationWithPhotoDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";
import { PreliminarySourceIdentificationCategories } from "src/app/shared/generated/enum/preliminary-source-identification-category-enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import {
    OnlandVisualTrashAssessmentReviewAndFinalizeDto,
    OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-review-and-finalize-dto";
import { OnlandVisualTrashAssessmentStatusEnum } from "src/app/shared/generated/enum/onland-visual-trash-assessment-status-enum";
import { WorkflowBodyComponent } from "../../../../shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { ObservationsMapComponent } from "../../ovtas/observations-map/observations-map.component";
import { OnlandVisualTrashAssessmentObservationService } from "src/app/shared/generated/api/onland-visual-trash-assessment-observation.service";
import { OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDtoForm } from "src/app/shared/generated/model/onland-visual-trash-assessment-preliminary-source-identification-upsert-dto";

@Component({
    selector: "trash-ovta-review-and-finalize",
    standalone: true,
    imports: [
        PageHeaderComponent,
        FormFieldComponent,
        ReactiveFormsModule,
        NgIf,
        AsyncPipe,
        FormsModule,
        NgFor,
        ObservationsMapComponent,
        NgFor,
        WorkflowBodyComponent,
        AlertDisplayComponent,
    ],
    templateUrl: "./trash-ovta-review-and-finalize.component.html",
    styleUrl: "./trash-ovta-review-and-finalize.component.scss",
})
export class TrashOvtaReviewAndFinalizeComponent {
    public isLoadingSubmit = false;
    public FormFieldType = FormFieldType;
    public PreliminarySourceIdentificationCategories = PreliminarySourceIdentificationCategories;

    public ovtaID: number;

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentReviewAndFinalizeDto>;
    public onlandVisualTrashAssessmentObservations$: Observable<OnlandVisualTrashAssessmentObservationWithPhotoDto[]>;

    public onlandVisualTrashAssessmentScoreDropdown = OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions;

    public ProgressScoreOptions: FormInputOption[] = [
        { Value: false, Label: "Baseline", Disabled: false },
        { Value: true, Label: "Progress", Disabled: false },
    ];

    public formGroup: FormGroup<OnlandVisualTrashAssessmentReviewAndFinalizeDtoCustomForm> = new FormGroup<OnlandVisualTrashAssessmentReviewAndFinalizeDtoCustomForm>({
        OnlandVisualTrashAssessmentID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentID(),
        OnlandVisualTrashAssessmentAreaID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentAreaID(),
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        StormwaterJurisdictionID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.StormwaterJurisdictionID(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.AssessmentAreaDescription(),
        AssessmentDate: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.AssessmentDate(),
        OnlandVisualTrashAssessmentScoreID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentScoreID(),
        IsProgressAssessment: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.IsProgressAssessment(),
        Notes: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.Notes(),
        PreliminarySourceIdentifications: new FormArray<FormGroup<OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDtoForm>>([]),
        OnlandVisualTrashAssessmentStatusID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentStatusID(),
    });

    constructor(
        private route: ActivatedRoute,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentObservationService: OnlandVisualTrashAssessmentObservationService,
        private router: Router,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService,
        private alertService: AlertService,
        private formBuilder: FormBuilder
    ) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDReviewAndFinalizeGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            })
        );

        this.onlandVisualTrashAssessmentObservations$ = this.onlandVisualTrashAssessment$.pipe(
            tap((ovta) => {
                this.ovtaID = ovta.OnlandVisualTrashAssessmentID;
                this.formGroup.controls.OnlandVisualTrashAssessmentID.setValue(ovta.OnlandVisualTrashAssessmentID);
                this.formGroup.controls.OnlandVisualTrashAssessmentAreaID.setValue(ovta.OnlandVisualTrashAssessmentAreaID);
                this.formGroup.controls.OnlandVisualTrashAssessmentAreaName.setValue(ovta.OnlandVisualTrashAssessmentAreaName);
                this.formGroup.controls.AssessmentAreaDescription.setValue(ovta.AssessmentAreaDescription);
                this.formGroup.controls.AssessmentDate.setValue(new Date(ovta.AssessmentDate).toISOString().split("T")[0]);
                this.formGroup.controls.OnlandVisualTrashAssessmentScoreID.setValue(ovta.OnlandVisualTrashAssessmentScoreID);
                this.formGroup.controls.IsProgressAssessment.setValue(ovta.IsProgressAssessment);
                this.formGroup.controls.Notes.setValue(ovta.Notes);
                const formArray = this.formGroup.controls.PreliminarySourceIdentifications as FormArray;
                ovta.PreliminarySourceIdentifications.forEach((x) => {
                    let preliminarySourceIdenitfication = this.formBuilder.group<OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDtoForm>({
                        Selected: new FormControl(x.Selected),
                        PreliminarySourceIdentificationCategoryID: new FormControl(x.PreliminarySourceIdentificationCategoryID),
                        PreliminarySourceIdentificationTypeID: new FormControl(x.PreliminarySourceIdentificationTypeID),
                        PreliminarySourceIdentificationTypeName: new FormControl(x.PreliminarySourceIdentificationTypeName),
                        IsOther: new FormControl(x.IsOther),
                        ExplanationIfTypeIsOther: new FormControl({ value: x.ExplanationIfTypeIsOther, disabled: x.IsOther && !x.Selected }),
                    });

                    formArray.push(preliminarySourceIdenitfication);
                });
            }),
            switchMap((onlandVisualTrashAssessment) => {
                return this.onlandVisualTrashAssessmentObservationService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID
                );
            })
        );
    }

    filterByCategory(
        onlandVisualTrashAssessmentPreliminarySourceIdentifications: FormGroup<OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDtoForm>[],
        preliminarySourceIdentificationCategoryID: number
    ) {
        const preliminarySourceIdentifications = onlandVisualTrashAssessmentPreliminarySourceIdentifications.filter(
            (x) => x.controls.PreliminarySourceIdentificationCategoryID.value === preliminarySourceIdentificationCategoryID
        );
        return preliminarySourceIdentifications;
    }

    onPreliminarySourceIdentificationTypeChange(event: any, otherTextBox: FormControl<string>) {
        if (event.target.value === "IsOther") {
            if (event.target.checked) {
                otherTextBox.enable();
                otherTextBox.setValidators([Validators.required]);
                otherTextBox.updateValueAndValidity();
            } else {
                otherTextBox.disable();
                otherTextBox.setValidators(null);
                otherTextBox.updateValueAndValidity();
            }
        }
    }

    save(andContinue: boolean = false) {
        this.formGroup.controls.OnlandVisualTrashAssessmentStatusID.setValue(
            andContinue ? OnlandVisualTrashAssessmentStatusEnum.Complete : OnlandVisualTrashAssessmentStatusEnum.InProgress
        );
        this.onlandVisualTrashAssessmentService
            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDReviewAndFinalizePost(this.ovtaID, this.formGroup.getRawValue())
            .subscribe(() => {
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Assessment successfully updated.", AlertContext.Success));
                this.ovtaWorkflowProgressService.updateProgress(this.ovtaID);
                if (andContinue) {
                    this.router.navigate([`../../../${this.ovtaID}`], { relativeTo: this.route });
                }
            });
    }
}

export class OnlandVisualTrashAssessmentReviewAndFinalizeDtoCustomForm {
    OnlandVisualTrashAssessmentID: FormControl<number>;
    OnlandVisualTrashAssessmentAreaID: FormControl<number>;
    OnlandVisualTrashAssessmentAreaName: FormControl<string>;
    StormwaterJurisdictionID: FormControl<number>;
    AssessmentAreaDescription: FormControl<string>;
    AssessmentDate: FormControl<string>;
    OnlandVisualTrashAssessmentScoreID: FormControl<number>;
    IsProgressAssessment: FormControl<boolean>;
    Notes: FormControl<string>;
    OnlandVisualTrashAssessmentStatusID: FormControl<number>;
    PreliminarySourceIdentifications: FormArray<FormGroup<OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDtoForm>>;
}
