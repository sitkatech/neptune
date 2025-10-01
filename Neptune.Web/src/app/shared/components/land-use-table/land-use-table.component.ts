import { Component, Input } from "@angular/core";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { SumPipe } from "src/app/shared/pipes/sum.pipe";
import { TreatmentBMPHRUCharacteristicsSummarySimpleDto } from "src/app/shared/generated/model/treatment-bmphru-characteristics-summary-simple-dto";
import { DecimalPipe } from "@angular/common";

@Component({
    selector: "land-use-table",
    templateUrl: "./land-use-table.component.html",
    styleUrls: ["./land-use-table.component.scss"],
    standalone: true,
    imports: [FieldDefinitionComponent, SumPipe, DecimalPipe],
})
export class LandUseTableComponent {
    @Input() hruCharacteristics: TreatmentBMPHRUCharacteristicsSummarySimpleDto[] = [];
}
