import { Component, OnInit, Input, SimpleChanges, OnChanges } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { FieldDefinitionTypeEnum } from "src/app/shared/generated/enum/field-definition-type-enum";
import { RouterModule } from "@angular/router";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { LoadGeneratingUnitsLayerComponent } from "src/app/shared/components/leaflet/layers/load-generating-units-layer/load-generating-units-layer.component";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { AsyncPipe, DatePipe, DecimalPipe } from "@angular/common";
import { LoadGeneratingUnitService } from "src/app/shared/generated/api/load-generating-unit.service";
import { LoadGeneratingUnitDto } from "src/app/shared/generated/model/load-generating-unit-dto";
import { HRULogDto } from "src/app/shared/generated/model/hru-log-dto";
import { HRUCharacteristicDto } from "src/app/shared/generated/model/hru-characteristic-dto";
import { ColDef } from "ag-grid-community";
import { Observable } from "rxjs";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { OverlayMode } from "src/app/shared/components/leaflet/layers/generic-wms-wfs-layer/overlay-mode.enum";

@Component({
    selector: "load-generating-unit-detail",
    templateUrl: "./load-generating-unit-detail.component.html",
    styleUrls: ["./load-generating-unit-detail.component.scss"],
    standalone: true,
    imports: [
        PageHeaderComponent,
        AlertDisplayComponent,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        LoadGeneratingUnitsLayerComponent,
        NeptuneGridComponent,
        AsyncPipe,
        FieldDefinitionComponent,
        RouterModule,
        DatePipe,
        DecimalPipe,
    ],
})
export class LoadGeneratingUnitDetailComponent implements OnInit, OnChanges {
    public OverlayMode = OverlayMode;
    FieldDefinitionTypeEnum = FieldDefinitionTypeEnum;
    @Input() loadGeneratingUnitID!: number;
    public loadGeneratingUnit$!: Observable<LoadGeneratingUnitDto>;
    public hruCharacteristics$!: Observable<HRUCharacteristicDto[]>;
    public hruCharacteristicsColumnDefs: ColDef[];
    public map: any;
    public layerControl: any;
    public mapIsReady = false;
    public boundingBox: any;

    constructor(private loadGeneratingUnitService: LoadGeneratingUnitService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit(): void {
        this.loadData();
        this.hruCharacteristicsColumnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Type of HRU Entity", "HRUEntity"),
            this.utilityFunctionsService.createLinkColumnDef("LGU ID", "LoadGeneratingUnitID", "LoadGeneratingUnitID", {
                InRouterLink: "/inventory/load-generating-units/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Model Basin Land Use Description", "HRUCharacteristicLandUseCodeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Baseline Model Basin Land Use Description", "BaselineHRUCharacteristicLandUseCodeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Hydrologic Soil Group", "HydrologicSoilGroup", {
                FieldDefinitionLabelOverride: "Hydrologic Soil Group",
                FieldDefinitionType: "UnderlyingHydrologicSoilGroupID",
            }),
            this.utilityFunctionsService.createDecimalColumnDef("Slope Percentage", "SlopePercentage"),
            this.utilityFunctionsService.createDecimalColumnDef("Impervious Acres", "ImperviousAcres", {
                FieldDefinitionLabelOverride: "Impervious Acres",
                FieldDefinitionType: "ImperviousArea",
            }),
            this.utilityFunctionsService.createDecimalColumnDef("Baseline Impervious Acres", "BaselineImperviousAcres"),
            this.utilityFunctionsService.createDecimalColumnDef("Total Acres", "Area", {
                FieldDefinitionLabelOverride: "Total Acres",
                FieldDefinitionType: "Area",
            }),
            this.utilityFunctionsService.createLinkColumnDef("Treatment BMP", "TreatmentBMPName", "TreatmentBMPID", {
                InRouterLink: "/inventory/treatment-bmps/",
                FieldDefinitionLabelOverride: "Treatment BMP",
                FieldDefinitionType: "TreatmentBMP",
            }),
            this.utilityFunctionsService.createLinkColumnDef("Water Quality Management Plan", "WaterQualityManagementPlanName", "WaterQualityManagementPlanID", {
                InRouterLink: "/inventory/water-quality-management-plans/",
                FieldDefinitionLabelOverride: "Water Quality Management Plan",
                FieldDefinitionType: "WaterQualityManagementPlan",
            }),
            this.utilityFunctionsService.createLinkColumnDef("Regional Subbasin", "RegionalSubbasinID", "RegionalSubbasinID", {
                InRouterLink: "/inventory/regional-subbasins/",
                FieldDefinitionLabelOverride: "Regional Subbasin",
                FieldDefinitionType: "RegionalSubbasin",
            }),
            this.utilityFunctionsService.createDateColumnDef("Last Updated", "LastUpdated", "MM/dd/yyyy"),
        ];
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes["loadGeneratingUnitID"] && !changes["loadGeneratingUnitID"].firstChange) {
            this.loadData();
        }
    }

    private loadData(): void {
        this.loadGeneratingUnit$ = this.loadGeneratingUnitService.getLoadGeneratingUnit(this.loadGeneratingUnitID);
        this.hruCharacteristics$ = this.loadGeneratingUnitService.listHRUCharacteristicsLoadGeneratingUnit(this.loadGeneratingUnitID);
    }

    handleMapReady(event: any): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }

    downloadHRURequest(hruLog: HRULogDto | null | undefined): void {
        if (hruLog?.HRURequest) {
            const json = typeof hruLog.HRURequest === "string" ? hruLog.HRURequest : JSON.stringify(hruLog.HRURequest, null, 2);
            const blob = new Blob([json], { type: "application/json" });
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement("a");
            a.href = url;
            a.download = "HRURequest.json";
            a.click();
            window.URL.revokeObjectURL(url);
        }
    }

    downloadHRUResponse(hruLog: HRULogDto | null | undefined): void {
        if (hruLog?.HRUResponse) {
            const json = typeof hruLog.HRUResponse === "string" ? hruLog.HRUResponse : JSON.stringify(hruLog.HRUResponse, null, 2);
            const blob = new Blob([json], { type: "application/json" });
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement("a");
            a.href = url;
            a.download = "HRUResponse.json";
            a.click();
            window.URL.revokeObjectURL(url);
        }
    }
}
