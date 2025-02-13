import { Component, ComponentRef, OnDestroy, OnInit } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { Subscription } from "rxjs";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/form-field/form-field.component";
import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import {
    OnlandVisualTrashAssessmentAreaDetailDto,
    OnlandVisualTrashAssessmentAreaDetailDtoForm,
    OnlandVisualTrashAssessmentAreaDetailDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { AlertService } from "src/app/shared/services/alert.service";
import { ModalService } from "src/app/shared/services/modal/modal.service";
import { NgIf } from "@angular/common";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { OnlandVisualTrashAssessmentDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-detail-dto";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";

@Component({
    selector: "update-ovta-area-details-modal",
    standalone: true,
    imports: [ReactiveFormsModule, FormFieldComponent, NgIf, AlertDisplayComponent],
    templateUrl: "./update-ovta-area-details-modal.component.html",
    styleUrl: "./update-ovta-area-details-modal.component.scss",
})
export class UpdateOvtaAreaDetailsModalComponent implements OnInit {
    public FormFieldType = FormFieldType;
    public modalComponentRef: ComponentRef<ModalComponent>;
    public modalContext: UpdateOvtaAreaModalContext;

    public formGroup = new FormGroup<OnlandVisualTrashAssessmentAreaDetailDtoForm>({
        OnlandVisualTrashAssessmentAreaID: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentAreaID(),
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.AssessmentAreaDescription(),
        StormwaterJurisdictionName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.StormwaterJurisdictionName(),
        LastAssessmentDate: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.LastAssessmentDate(),
        OnlandVisualTrashAssessmentBaselineScoreName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentBaselineScoreName(),
        OnlandVisualTrashAssessmentProgressScoreName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentProgressScoreName(),
    });

    constructor(private modalService: ModalService, private alertService: AlertService, private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService) {}

    ngOnInit(): void {
        if (this.modalContext.OvtaAreaDto) {
            this.formGroup.patchValue(this.modalContext.OvtaAreaDto);
        }
    }

    save(): void {
        let upsertDto = this.formGroup.value as OnlandVisualTrashAssessmentAreaDetailDto;
        this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasPost(upsertDto).subscribe(() => this.modalService.close(this.modalComponentRef, upsertDto));
    }

    cancel(): void {
        this.modalService.close(this.modalComponentRef, null);
    }
}

export class UpdateOvtaAreaModalContext {
    OvtaAreaDto: OnlandVisualTrashAssessmentDetailDto;
}
