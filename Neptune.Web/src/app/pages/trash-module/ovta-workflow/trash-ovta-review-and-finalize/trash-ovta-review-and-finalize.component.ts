import { Component } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import {
    OnlandVisualTrashAssessmentAreaDetailDtoForm,
    OnlandVisualTrashAssessmentAreaDetailDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FormFieldComponent, FormFieldType } from "../../../../shared/components/form-field/form-field.component";
import { OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions } from "src/app/shared/generated/enum/onland-visual-trash-assessment-score-enum";

@Component({
    selector: "trash-ovta-review-and-finalize",
    standalone: true,
    imports: [PageHeaderComponent, FormFieldComponent, ReactiveFormsModule],
    templateUrl: "./trash-ovta-review-and-finalize.component.html",
    styleUrl: "./trash-ovta-review-and-finalize.component.scss",
})
export class TrashOvtaReviewAndFinalizeComponent {
    public isLoadingSubmit = false;
    public FormFieldType = FormFieldType;

    public onlandVisualTrashAssessmentScoreDropdown = OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions;

    public formGroup: FormGroup<OnlandVisualTrashAssessmentAreaDetailDtoForm> = new FormGroup<any>({
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.AssessmentAreaDescription(),
        LastAssessmentDate: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.LastAssessmentDate(),
        OnlandVisualTrashAssessmentBaselineScoreName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentBaselineScoreName(),
        OnlandVisualTrashAssessmentProgressScoreName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentProgressScoreName(),
    });

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService) {}

    ngOnInit(): void {}

    save(andContinue: boolean = false) {}
}
