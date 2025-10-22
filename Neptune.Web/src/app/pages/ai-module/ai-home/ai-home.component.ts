import { Component, OnInit } from "@angular/core";
import { map, Observable } from "rxjs";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule, FormControl } from "@angular/forms";
import { WaterQualityManagementPlanService } from "src/app/shared/generated/api/water-quality-management-plan.service";
import { WaterQualityManagementPlanKeyDocumentsComponent } from "src/app/shared/components/water-quality-management-plan-documents/water-quality-management-plan-documents.component";
import { FormFieldComponent, FormFieldType, SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component";

@Component({
    selector: "ai-home",
    templateUrl: "./ai-home.component.html",
    styleUrls: ["./ai-home.component.scss"],
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, WaterQualityManagementPlanKeyDocumentsComponent, FormFieldComponent],
})
export class AiHomeComponent implements OnInit {
    public isExtracting: boolean = false;

    onExtractingChange(isExtracting: boolean) {
        this.isExtracting = isExtracting;
        if (isExtracting) {
            this.planControl.disable();
        } else {
            this.planControl.enable();
        }
    }
    public waterQualityManagementPlans$: Observable<SelectDropdownOption[]>;
    public planControl = new FormControl<number | null>(null);
    public FormFieldType = FormFieldType;

    constructor(private waterQualityManagementPlanService: WaterQualityManagementPlanService) {}

    ngOnInit(): void {
        this.waterQualityManagementPlans$ = this.waterQualityManagementPlanService.listWithFinalWQMPDocumentWaterQualityManagementPlan().pipe(
            map((list) => {
                let options = list.map((x) => ({ Value: x.WaterQualityManagementPlanID, Label: x.WaterQualityManagementPlanName } as SelectDropdownOption));
                return options;
            })
        );
    }
}
