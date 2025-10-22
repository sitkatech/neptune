import { Component, Input } from "@angular/core";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { HRUCharacteristicDto } from "src/app/shared/generated/model/hru-characteristic-dto";
import { ColDef } from "ag-grid-community";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";

@Component({
    selector: "hru-characteristics-grid",
    standalone: true,
    imports: [NeptuneGridComponent],
    templateUrl: "./hru-characteristics-grid.component.html",
})
export class HruCharacteristicsGridComponent {
    @Input() rowData: HRUCharacteristicDto[] | null = null;
    @Input() height: string = "400px";
    @Input() rowSelection: "single" | "multiple";
    @Input() downloadFileName: string = "HRUCharacteristics";

    public columnDefs: ColDef[];

    constructor(private utilityFunctions: UtilityFunctionsService) {
        this.columnDefs = [
            this.utilityFunctions.createBasicColumnDef("Type of HRU Entity", "HRUEntity"),
            this.utilityFunctions.createLinkColumnDef("LGU ID", "LoadGeneratingUnitID", "LoadGeneratingUnitID", {
                InRouterLink: "/load-generating-units/",
            }),
            this.utilityFunctions.createBasicColumnDef("Model Basin Land Use Description", "HRUCharacteristicLandUseCodeDisplayName"),
            this.utilityFunctions.createBasicColumnDef("Baseline Model Basin Land Use Description", "BaselineHRUCharacteristicLandUseCodeDisplayName"),
            this.utilityFunctions.createBasicColumnDef("Hydrologic Soil Group", "HydrologicSoilGroup", {
                FieldDefinitionLabelOverride: "Hydrologic Soil Group",
                FieldDefinitionType: "UnderlyingHydrologicSoilGroupID",
            }),
            this.utilityFunctions.createDecimalColumnDef("Slope Percentage", "SlopePercentage"),
            this.utilityFunctions.createDecimalColumnDef("Impervious Acres", "ImperviousAcres", {
                FieldDefinitionLabelOverride: "Impervious Acres",
                FieldDefinitionType: "ImperviousArea",
            }),
            this.utilityFunctions.createDecimalColumnDef("Baseline Impervious Acres", "BaselineImperviousAcres"),
            this.utilityFunctions.createDecimalColumnDef("Total Acres", "Area", {
                FieldDefinitionLabelOverride: "Total Acres",
                FieldDefinitionType: "Area",
            }),
            this.utilityFunctions.createLinkColumnDef("Treatment BMP", "TreatmentBMPName", "TreatmentBMPID", {
                InRouterLink: "/treatment-bmps/",
                FieldDefinitionLabelOverride: "Treatment BMP",
                FieldDefinitionType: "TreatmentBMP",
            }),
            this.utilityFunctions.createLinkColumnDef("Water Quality Management Plan", "WaterQualityManagementPlanName", "WaterQualityManagementPlanID", {
                InRouterLink: "/water-quality-management-plans/",
                FieldDefinitionLabelOverride: "Water Quality Management Plan",
                FieldDefinitionType: "WaterQualityManagementPlan",
            }),
            this.utilityFunctions.createLinkColumnDef("Regional Subbasin", "RegionalSubbasinID", "RegionalSubbasinID", {
                InRouterLink: "/regional-subbasins/",
                FieldDefinitionLabelOverride: "Regional Subbasin",
                FieldDefinitionType: "RegionalSubbasin",
            }),
            this.utilityFunctions.createDateColumnDef("Last Updated", "LastUpdated", "MM/dd/yyyy"),
        ];
    }
}
