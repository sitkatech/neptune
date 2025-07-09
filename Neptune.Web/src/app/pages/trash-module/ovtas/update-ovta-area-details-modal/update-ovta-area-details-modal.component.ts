import { Component, ComponentRef, OnInit } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/forms/form-field/form-field.component";
import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { ModalService } from "src/app/shared/services/modal/modal.service";
import { NgIf } from "@angular/common";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OnlandVisualTrashAssessmentAreaSimpleDtoForm, OnlandVisualTrashAssessmentAreaSimpleDtoFormControls } from "src/app/shared/generated/model/models";

@Component({
    selector: "update-ovta-area-details-modal",
    imports: [ReactiveFormsModule, FormFieldComponent, NgIf, AlertDisplayComponent],
    templateUrl: "./update-ovta-area-details-modal.component.html",
    styleUrl: "./update-ovta-area-details-modal.component.scss"
})
export class UpdateOvtaAreaDetailsModalComponent implements OnInit {
    public FormFieldType = FormFieldType;
    public modalComponentRef: ComponentRef<ModalComponent>;
    public modalContext: UpdateOvtaAreaModalContext;

    public formGroup = new FormGroup<OnlandVisualTrashAssessmentAreaSimpleDtoForm>({
        OnlandVisualTrashAssessmentAreaID: OnlandVisualTrashAssessmentAreaSimpleDtoFormControls.OnlandVisualTrashAssessmentAreaID(),
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentAreaSimpleDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentAreaSimpleDtoFormControls.AssessmentAreaDescription(),
    });

    constructor(private modalService: ModalService, private alertService: AlertService, private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService) {}

    ngOnInit(): void {
        this.formGroup.controls.OnlandVisualTrashAssessmentAreaID.setValue(this.modalContext.OnlandVisualTrashAssessmentAreaID);
        this.formGroup.controls.OnlandVisualTrashAssessmentAreaName.setValue(this.modalContext.OnlandVisualTrashAssessmentAreaName);
        this.formGroup.controls.AssessmentAreaDescription.setValue(this.modalContext.AssessmentAreaDescription);
    }

    save(): void {
        this.onlandVisualTrashAssessmentAreaService
            .onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDPut(this.formGroup.controls.OnlandVisualTrashAssessmentAreaID.value, this.formGroup.value)
            .subscribe(() => {
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Successfully updated OVTA area.", AlertContext.Success));
                this.modalService.close(this.modalComponentRef, true);
            });
    }

    cancel(): void {
        this.modalService.close(this.modalComponentRef, null);
    }
}

export class UpdateOvtaAreaModalContext {
    OnlandVisualTrashAssessmentAreaID: number;
    OnlandVisualTrashAssessmentAreaName: string;
    AssessmentAreaDescription: string;
}
