import { Component } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import {
    OnlandVisualTrashAssessmentAreaDetailDto,
    OnlandVisualTrashAssessmentAreaDetailDtoForm,
    OnlandVisualTrashAssessmentAreaDetailDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FormFieldComponent, FormFieldType } from "../../../../shared/components/form-field/form-field.component";
import { OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions } from "src/app/shared/generated/enum/onland-visual-trash-assessment-score-enum";
import { ActivatedRoute } from "@angular/router";
import { Observable, switchMap, tap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { AsyncPipe, NgIf } from "@angular/common";

@Component({
    selector: "trash-ovta-review-and-finalize",
    standalone: true,
    imports: [PageHeaderComponent, FormFieldComponent, ReactiveFormsModule, NgIf, AsyncPipe],
    templateUrl: "./trash-ovta-review-and-finalize.component.html",
    styleUrl: "./trash-ovta-review-and-finalize.component.scss",
})
export class TrashOvtaReviewAndFinalizeComponent {
    public isLoadingSubmit = false;
    public FormFieldType = FormFieldType;

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentAreaDetailDto>;

    public onlandVisualTrashAssessmentScoreDropdown = OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions;

    public formGroup: FormGroup<OnlandVisualTrashAssessmentAreaDetailDtoForm> = new FormGroup<any>({
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.AssessmentAreaDescription(),
        LastAssessmentDate: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.LastAssessmentDate(),
        OnlandVisualTrashAssessmentBaselineScoreName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentBaselineScoreName(),
        OnlandVisualTrashAssessmentProgressScoreName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentProgressScoreName(),
    });

    constructor(private route: ActivatedRoute, private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDOnlandVisualTrashAssessmentAreaGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            }),
            tap((ovta) => {
                console.log(ovta);
                this.formGroup.controls.OnlandVisualTrashAssessmentAreaName.setValue(ovta.OnlandVisualTrashAssessmentAreaName);
                this.formGroup.controls.AssessmentAreaDescription.setValue(ovta.AssessmentAreaDescription);
                this.formGroup.controls.LastAssessmentDate.setValue(ovta.LastAssessmentDate);
                this.formGroup.controls.OnlandVisualTrashAssessmentBaselineScoreName.setValue(ovta.OnlandVisualTrashAssessmentBaselineScoreName);
                this.formGroup.controls.OnlandVisualTrashAssessmentProgressScoreName.setValue(ovta.OnlandVisualTrashAssessmentProgressScoreName);
            })
        );
    }

    save(andContinue: boolean = false) {}
}
