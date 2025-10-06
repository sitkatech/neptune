import { Component } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { AsyncPipe } from "@angular/common";
import { HRUCharacteristicService } from "src/app/shared/generated/api/hru-characteristic.service";
import { HRUCharacteristicDto } from "src/app/shared/generated/model/hru-characteristic-dto";
import { ColDef } from "ag-grid-community";
import { Observable } from "rxjs";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";

@Component({
    selector: "hru-characteristics",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NeptuneGridComponent, AsyncPipe],
    templateUrl: "./hru-characteristics.component.html",
})
export class HRUCharacteristicsComponent {
    public hruCharacteristics$: Observable<HRUCharacteristicDto[]>;
    public columnDefs: ColDef[];
    public customRichTextTypeID = NeptunePageTypeEnum.HRUCharacteristics;

    constructor(private hruCharacteristicService: HRUCharacteristicService, private utilityFunctions: UtilityFunctionsService) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctions.createBasicColumnDef("Type of HRU Entity", "HRUEntity"),
            this.utilityFunctions.createLinkColumnDef("LGU ID", "LoadGeneratingUnitID", "LoadGeneratingUnitID", {
                InRouterLink: "/inventory/load-generating-units/",
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
                InRouterLink: "/inventory/treatment-bmps/",
                FieldDefinitionLabelOverride: "Treatment BMP",
                FieldDefinitionType: "TreatmentBMP",
            }),
            this.utilityFunctions.createLinkColumnDef("Water Quality Management Plan", "WaterQualityManagementPlanName", "WaterQualityManagementPlanID", {
                InRouterLink: "/inventory/water-quality-management-plans/",
                FieldDefinitionLabelOverride: "Water Quality Management Plan",
                FieldDefinitionType: "WaterQualityManagementPlan",
            }),
            this.utilityFunctions.createLinkColumnDef("Regional Subbasin", "RegionalSubbasinID", "RegionalSubbasinID", {
                InRouterLink: "/inventory/regional-subbasins/",
                FieldDefinitionLabelOverride: "Regional Subbasin",
                FieldDefinitionType: "RegionalSubbasin",
            }),
            this.utilityFunctions.createDateColumnDef("Last Updated", "LastUpdated", "MM/dd/yyyy"),
        ];
        this.hruCharacteristics$ = this.hruCharacteristicService.listHRUCharacteristic();
    }
}
