import { Component, ComponentRef, inject, OnInit } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/forms/form-field/form-field.component";
import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { ModalService } from "src/app/shared/services/modal/modal.service";

import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OnlandVisualTrashAssessmentAreaSimpleDtoForm, OnlandVisualTrashAssessmentAreaSimpleDtoFormControls } from "src/app/shared/generated/model/models";
import { DialogRef } from "@ngneat/dialog";

@Component({
    selector: "update-ovta-area-details-modal",
    imports: [ReactiveFormsModule, FormFieldComponent, AlertDisplayComponent],
    templateUrl: "./update-ovta-area-details-modal.component.html",
    styleUrl: "./update-ovta-area-details-modal.component.scss",
})
export class UpdateOvtaAreaDetailsModalComponent implements OnInit {
    public ref: DialogRef<UpdateOvtaAreaModalContext, boolean> = inject(DialogRef);
    public FormFieldType = FormFieldType;

    public formGroup = new FormGroup<OnlandVisualTrashAssessmentAreaSimpleDtoForm>({
        OnlandVisualTrashAssessmentAreaID: OnlandVisualTrashAssessmentAreaSimpleDtoFormControls.OnlandVisualTrashAssessmentAreaID(),
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentAreaSimpleDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentAreaSimpleDtoFormControls.AssessmentAreaDescription(),
    });

    constructor(private modalService: ModalService, private alertService: AlertService, private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService) {}

    ngOnInit(): void {
        this.alertService.clearAlerts();
        this.formGroup.controls.OnlandVisualTrashAssessmentAreaID.setValue(this.ref.data.OnlandVisualTrashAssessmentAreaID);
        this.formGroup.controls.OnlandVisualTrashAssessmentAreaName.setValue(this.ref.data.OnlandVisualTrashAssessmentAreaName);
        this.formGroup.controls.AssessmentAreaDescription.setValue(this.ref.data.AssessmentAreaDescription);
    }

    save(): void {
        this.onlandVisualTrashAssessmentAreaService
            .updateOnlandVisualTrashAssessmentArea(this.formGroup.controls.OnlandVisualTrashAssessmentAreaID.value, this.formGroup.value)
            .subscribe(() => {
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Successfully updated OVTA area.", AlertContext.Success));
                this.ref.close(true);
            });
    }

    cancel(): void {
        this.ref.close(null);
    }
}

export class UpdateOvtaAreaModalContext {
    OnlandVisualTrashAssessmentAreaID: number;
    OnlandVisualTrashAssessmentAreaName: string;
    AssessmentAreaDescription: string;
}
