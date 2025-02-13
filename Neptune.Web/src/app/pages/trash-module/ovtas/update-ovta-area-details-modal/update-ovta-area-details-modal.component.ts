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
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentAreaDetailDtoFormControls.AssessmentAreaDescription(),
    });

    constructor(private modalService: ModalService, private alertService: AlertService) {}

    ngOnInit(): void {
        if (this.modalContext.OvtaAreaDto) {
            this.formGroup.patchValue(this.modalContext.OvtaAreaDto);
        }
    }

    save(): void {
        let upsertDto = this.formGroup.value as OnlandVisualTrashAssessmentAreaDetailDto;
        // if (this.isNewReportingPeriod) {
        //     this.addOrEditSubscription = this.reportingPeriodService.geographiesGeographyIDReportingPeriodsPost(this.modalContext.GeographyID, upsertDto).subscribe(
        //         (response) => {
        //             this.modalService.close(this.modalComponentRef, response);
        //         }
        //         // (error) => {
        //         //     //MK 2/3/2025: App Alert Display is on the parent page, not sure it makes sense to have it here as well.
        //         //     this.modalService.close(this.modalComponentRef, null);
        //         // }
        //     );
        // } else {
        //     this.addOrEditSubscription = this.reportingPeriodService
        //         .geographiesGeographyIDReportingPeriodsReportingPeriodIDPut(this.modalContext.GeographyID, this.modalContext.ReportingPeriod.ReportingPeriodID, upsertDto)
        //         .subscribe(
        //             (response) => {
        //                 this.modalService.close(this.modalComponentRef, response);
        //             }
        //             // (error) => {
        //             //     //MK 2/3/2025: App Alert Display is on the parent page, not sure it makes sense to have it here as well.
        //             //     this.modalService.close(this.modalComponentRef, null);
        //             // }
        //         );
        // }
    }

    cancel(): void {
        this.modalService.close(this.modalComponentRef, null);
    }
}

export class UpdateOvtaAreaModalContext {
    OvtaAreaDto: OnlandVisualTrashAssessmentDetailDto;
}
