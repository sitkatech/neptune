import { Component, Input } from "@angular/core";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { BtnGroupRadioInputComponent } from "src/app/shared/components/inputs/btn-group-radio-input/btn-group-radio-input.component";
import { DecimalPipe } from "@angular/common";
import { NeptuneModelingResultSigFigPipe } from "src/app/shared/pipes/neptune-modeling-result-sig-fig.pipe";
import { ModeledPerformanceDisplayTypeEnum } from "src/app/pages/planning-module/projects/project-model-results/project-model-results.component";

@Component({
    selector: "model-results",
    templateUrl: "./model-results.component.html",
    styleUrls: ["./model-results.component.scss"],
    standalone: true,
    imports: [FieldDefinitionComponent, BtnGroupRadioInputComponent, DecimalPipe, NeptuneModelingResultSigFigPipe],
})
export class ModelResultsComponent {
    /**
     * For planning module: pass derived objects (selectedProjectLoadReducingResult, selectedTreatmentBMPHRUCharacteristicSummaries, selectedTreatmentBMPHRUCharacteristicSummaryTotal)
     * For detail page: pass single BMP data directly as selectedProjectLoadReducingResult, and summaries as needed
     */
    @Input() treatmentBMPIDForSelectedProjectLoadReducingResult?: number | null;
    @Input() selectedProjectLoadReducingResult!: any; // required in both cases
    @Input() selectedTreatmentBMPHRUCharacteristicSummaries?: any; // optional for detail page
    @Input() selectedTreatmentBMPHRUCharacteristicSummaryTotal?: any; // optional for detail page
    @Input() getModelResultsLastCalculatedText?: () => string;

    public ModeledPerformanceDisplayTypeEnum = ModeledPerformanceDisplayTypeEnum;
    public activeID: ModeledPerformanceDisplayTypeEnum = ModeledPerformanceDisplayTypeEnum.Total;

    public activeTab: string = "Total";
    public tabs = [
        { label: "Total", value: "Total" },
        { label: "Dry", value: "Dry" },
        { label: "Wet", value: "Wet" },
    ];

    public setActiveTab(event: string) {
        this.activeTab = event;
    }
}
