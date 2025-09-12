import { Component, OnInit, inject } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/forms/form-field/form-field.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { LandUseBlockUpsertDtoForm, LandUseBlockUpsertDtoFormControls } from "src/app/shared/generated/model/land-use-block-upsert-dto";
import { DialogRef } from "@ngneat/dialog";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { PriorityLandUseTypesAsSelectDropdownOptions } from "src/app/shared/generated/enum/priority-land-use-type-enum";
import { PermitTypesAsSelectDropdownOptions } from "src/app/shared/generated/enum/permit-type-enum";
import { LandUseBlockService } from "src/app/shared/generated/api/land-use-block.service";

@Component({
    selector: "trash-land-use-block-attributes-edit-modal",
    imports: [ReactiveFormsModule, FormFieldComponent, AlertDisplayComponent],
    templateUrl: "./trash-land-use-block-attributes-edit-modal.component.html",
    styleUrl: "./trash-land-use-block-attributes-edit-modal.component.scss",
})
export class TrashLandUseBlockAttributesEditModalComponent implements OnInit {
    public ref: DialogRef<TrashLandUseBlockAttributesEditModalContext> = inject(DialogRef);

    public FormFieldType = FormFieldType;
    public formGroup = new FormGroup<LandUseBlockUpsertDtoForm>({
        PriorityLandUseTypeID: LandUseBlockUpsertDtoFormControls.PriorityLandUseTypeID(),
        TrashGenerationRate: LandUseBlockUpsertDtoFormControls.TrashGenerationRate(),
        LandUseDescription: LandUseBlockUpsertDtoFormControls.LandUseDescription(),
        MedianHouseholdIncomeResidential: LandUseBlockUpsertDtoFormControls.MedianHouseholdIncomeResidential(),
        MedianHouseholdIncomeRetail: LandUseBlockUpsertDtoFormControls.MedianHouseholdIncomeRetail(),
        PermitTypeID: LandUseBlockUpsertDtoFormControls.PermitTypeID(),
    });

    public landUseTypes = PriorityLandUseTypesAsSelectDropdownOptions;
    public permitTypes = PermitTypesAsSelectDropdownOptions;

    constructor(private alertService: AlertService, private landUseBlockService: LandUseBlockService) {}

    ngOnInit(): void {
        this.alertService.clearAlerts();
        this.formGroup.controls.PriorityLandUseTypeID.setValue(this.ref.data.PriorityLandUseTypeID);
        this.formGroup.controls.TrashGenerationRate.setValue(this.ref.data.TrashGenerationRate);
        this.formGroup.controls.LandUseDescription?.setValue(this.ref.data.LandUseDescription ?? "");
        this.formGroup.controls.MedianHouseholdIncomeResidential?.setValue(this.ref.data.MedianHouseholdIncomeResidential ?? null);
        this.formGroup.controls.MedianHouseholdIncomeRetail?.setValue(this.ref.data.MedianHouseholdIncomeRetail ?? null);
        this.formGroup.controls.PermitTypeID.setValue(this.ref.data.PermitTypeID);
    }

    save(): void {
        this.landUseBlockService.landUseBlocksPut(this.ref.data.LandUseBlockID, this.formGroup.getRawValue()).subscribe(() => {
            this.alertService.clearAlerts();
            this.alertService.pushAlert(
                new Alert(
                    "The Land Use Block was successfully updated. Changes will be reflected in the trash results once the scheduled job to recalculate the underlying trash generation units runs overnight.",
                    AlertContext.Success
                )
            );
            this.ref.close(this.formGroup.value);
        });
    }

    cancel(): void {
        this.ref.close(null);
    }
}

export class TrashLandUseBlockAttributesEditModalContext {
    LandUseBlockID: number;
    PriorityLandUseTypeID: number;
    TrashGenerationRate: number;
    LandUseDescription?: string | null;
    MedianHouseholdIncomeResidential?: number | null;
    MedianHouseholdIncomeRetail?: number | null;
    PermitTypeID: number;
}
