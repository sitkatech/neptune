import { Component, inject, OnInit } from "@angular/core";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType, SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component";
import { AsyncPipe, CommonModule } from "@angular/common";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPTypeService } from "src/app/shared/generated/api/treatment-bmp-type.service";
import { map, Observable } from "rxjs";
import { TreatmentBMPTypeUpdate, TreatmentBMPTypeUpdateForm, TreatmentBMPTypeUpdateFormControls } from "src/app/shared/generated/model/treatment-bmp-type-update";
import { DialogRef } from "@ngneat/dialog";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";

@Component({
    selector: "treatment-bmp-update-type-modal",
    imports: [AlertDisplayComponent, ReactiveFormsModule, FormFieldComponent, AsyncPipe],
    templateUrl: "./treatment-bmp-update-type-modal.component.html",
    styleUrl: "./treatment-bmp-update-type-modal.component.scss",
})
export class TreatmentBmpUpdateTypeModalComponent implements OnInit {
    public ref: DialogRef<TreatmentBmpUpdateTypeModalContext, boolean> = inject(DialogRef);
    private treatmentBMPService = inject(TreatmentBMPService);
    private treatmentBMPTypeService = inject(TreatmentBMPTypeService);
    private alertService = inject(AlertService);

    public FormFieldType = FormFieldType;

    public formGroup: FormGroup<TreatmentBMPTypeUpdateForm> = new FormGroup<TreatmentBMPTypeUpdateForm>({
        TreatmentBMPTypeID: TreatmentBMPTypeUpdateFormControls.TreatmentBMPTypeID(undefined),
    });

    // populate select options observable
    public treatmentBMPTypeOptions$: Observable<SelectDropdownOption[]>;

    ngOnInit(): void {
        // clear any existing alerts
        this.alertService.clearAlerts();

        // load treatment bmp types as an observable for the template async pipe
        this.treatmentBMPTypeOptions$ = this.treatmentBMPTypeService
            .listTreatmentBMPType()
            .pipe(map((types) => types.map((type) => ({ Label: type.TreatmentBMPTypeName, Value: type.TreatmentBMPTypeID, disabled: false }) as SelectDropdownOption)));

        // if editing, set current value
        if (this.ref.data?.currentTreatmentBMPTypeID != null) {
            this.formGroup.controls["TreatmentBMPTypeID"].setValue(this.ref.data.currentTreatmentBMPTypeID);
        }
    }

    public save(): void {
        const treatmentBMPID = this.ref.data?.treatmentBMPID;
        const updateTypeDto = this.formGroup.value as TreatmentBMPTypeUpdate;
        this.treatmentBMPService.updateTypeTreatmentBMP(treatmentBMPID, updateTypeDto).subscribe(() => {
            this.ref.close(true);
        });
    }

    public cancel(): void {
        this.ref.close(null);
    }
}

export class TreatmentBmpUpdateTypeModalContext {
    treatmentBMPID: number;
    currentTreatmentBMPTypeID?: number;
}
