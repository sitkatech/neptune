import { FormControl } from "@angular/forms";
import { Component, inject, OnInit } from "@angular/core";
import { FormGroup, FormArray, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/forms/form-field/form-field.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { IconComponent } from "src/app/shared/components/icon/icon.component";
import { FundingEventUpsertDtoFormControls } from "src/app/shared/generated/model/funding-event-upsert-dto";
import { FundingEventFundingSourceSimpleDtoFormControls } from "src/app/shared/generated/model/funding-event-funding-source-simple-dto";
import { FundingEventByTreatmentBMPIDService } from "src/app/shared/generated/api/funding-event-by-treatment-bmpid.service";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { DialogRef } from "@ngneat/dialog";

@Component({
    selector: "funding-event-modal",
    imports: [ReactiveFormsModule, FormFieldComponent, AlertDisplayComponent, IconComponent],
    templateUrl: "./funding-event-modal.component.html",
    styleUrl: "./funding-event-modal.component.scss",
})
export class FundingEventModalComponent implements OnInit {
    private nextFundingEventFundingSourceID = 0;

    getFundingSourceNameControl(group: FormGroup): FormControl {
        return group.get("FundingSourceName") as FormControl;
    }

    getFundingSourceAmountControl(group: FormGroup): FormControl {
        return group.get("Amount") as FormControl;
    }
    getFundingSourceGroups(): FormGroup[] {
        return (this.formGroup.controls.FundingEventFundingSources as FormArray).controls as FormGroup[];
    }
    get fundingSources(): FormArray {
        return this.formGroup.controls.FundingEventFundingSources as FormArray;
    }

    addFundingSource(): void {
        // Use negative IDs for new sources to avoid collision with existing positive IDs
        this.fundingSources.push(
            new FormGroup({
                FundingEventFundingSourceID: FundingEventFundingSourceSimpleDtoFormControls.FundingEventFundingSourceID(--this.nextFundingEventFundingSourceID),
                FundingSourceName: FundingEventFundingSourceSimpleDtoFormControls.FundingSourceName(),
                Amount: FundingEventFundingSourceSimpleDtoFormControls.Amount(),
            })
        );
    }

    removeFundingSource(idx: number): void {
        this.fundingSources.removeAt(idx);
    }
    public ref: DialogRef<FundingEventModalContext, boolean> = inject(DialogRef);
    public FormFieldType = FormFieldType;
    public formGroup = new FormGroup({
        FundingEventTypeID: FundingEventUpsertDtoFormControls.FundingEventTypeID(),
        Year: FundingEventUpsertDtoFormControls.Year(),
        Description: FundingEventUpsertDtoFormControls.Description(),
        FundingEventFundingSources: new FormArray([]),
    });

    constructor(private fundingEventService: FundingEventByTreatmentBMPIDService, private alertService: AlertService) {}

    ngOnInit(): void {
        this.alertService.clearAlerts();
        if (this.ref.data?.editData) {
            // Patch simple fields
            this.formGroup.controls.FundingEventTypeID.setValue(this.ref.data.editData.FundingEventTypeID);
            this.formGroup.controls.Year.setValue(this.ref.data.editData.Year);
            this.formGroup.controls.Description.setValue(this.ref.data.editData.Description);

            // Patch funding sources as FormGroups
            const sourcesArray = this.formGroup.controls.FundingEventFundingSources as FormArray;
            sourcesArray.clear();
            (this.ref.data.editData.FundingEventFundingSources || []).forEach((src: any) => {
                sourcesArray.push(
                    new FormGroup({
                        FundingEventFundingSourceID: FundingEventFundingSourceSimpleDtoFormControls.FundingEventFundingSourceID(src.FundingEventFundingSourceID),
                        FundingSourceName: FundingEventFundingSourceSimpleDtoFormControls.FundingSourceName(src.FundingSourceName),
                        Amount: FundingEventFundingSourceSimpleDtoFormControls.Amount(src.Amount),
                    })
                );
            });
        }
    }

    save(): void {
        const treatmentBMPID = this.ref.data.treatmentBMPID;
        const dto = this.formGroup.value;
        if (this.ref.data?.editData) {
            // Edit
            this.fundingEventService.treatmentBmpsTreatmentBMPIDFundingEventsFundingEventIDPut(treatmentBMPID, this.ref.data.editData.FundingEventID, dto).subscribe(() => {
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Successfully updated Funding Event.", AlertContext.Success));
                this.ref.close(true);
            });
        } else {
            // Add
            this.fundingEventService.treatmentBmpsTreatmentBMPIDFundingEventsPost(treatmentBMPID, dto).subscribe(() => {
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Successfully added Funding Event.", AlertContext.Success));
                this.ref.close(true);
            });
        }
    }

    cancel(): void {
        this.ref.close(null);
    }
}

export class FundingEventModalContext {
    treatmentBMPID: number;
    editData?: any; // FundingEventDto for edit
}
