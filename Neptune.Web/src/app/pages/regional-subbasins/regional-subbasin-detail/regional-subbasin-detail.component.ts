import { Component, Input, OnInit } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { AsyncPipe, DecimalPipe } from "@angular/common";
import { RegionalSubbasinService } from "src/app/shared/generated/api/regional-subbasin.service";
import { RegionalSubbasinDto } from "src/app/shared/generated/model/regional-subbasin-dto";
import { ColDef } from "ag-grid-community";
import { Observable, tap, map } from "rxjs";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { OverlayMode } from "src/app/shared/components/leaflet/layers/generic-wms-wfs-layer/overlay-mode.enum";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { HRUCharacteristicDto } from "src/app/shared/generated/model/hru-characteristic-dto";
import { LandUseTableComponent } from "src/app/shared/components/land-use-table/land-use-table.component";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { TreatmentBMPHRUCharacteristicsSummarySimpleDto } from "src/app/shared/generated/model/treatment-bmphru-characteristics-summary-simple-dto";
import { SumPipe } from "src/app/shared/pipes/sum.pipe";
import { LoadGeneratingUnitsLayerComponent } from "src/app/shared/components/leaflet/layers/load-generating-units-layer/load-generating-units-layer.component";
import { RouterLink } from "@angular/router";
import { LoadGeneratingUnitGridDto } from "src/app/shared/generated/model/models";
import { HruCharacteristicsGridComponent } from "src/app/shared/components/hru-characteristics-grid/hru-characteristics-grid.component";

@Component({
    selector: "regional-subbasin-detail",
    templateUrl: "./regional-subbasin-detail.component.html",
    styleUrls: ["./regional-subbasin-detail.component.scss"],
    standalone: true,
    imports: [
        PageHeaderComponent,
        AlertDisplayComponent,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        NeptuneGridComponent,
        AsyncPipe,
        DecimalPipe,
        FieldDefinitionComponent,
        LandUseTableComponent,
        LoadGeneratingUnitsLayerComponent,
        RouterLink,
        HruCharacteristicsGridComponent,
    ],
})
export class RegionalSubbasinDetailComponent implements OnInit {
    public OverlayMode = OverlayMode;
    @Input() regionalSubbasinID!: number;
    public regionalSubbasin$: Observable<RegionalSubbasinDto>;
    public map: any;
    public layerControl: any;
    public mapIsReady = false;
    public isLoading = true;
    public landUseStats$: Observable<any[]>;
    public loadGeneratingUnits$!: Observable<LoadGeneratingUnitGridDto[]>;
    public loadGeneratingUnitIDs$: Observable<number[]>;
    public hruCharacteristics$!: Observable<HRUCharacteristicDto[]>;
    public landUseStatsColumnDefs: ColDef[];
    public loadGeneratingUnitsColumnDefs: ColDef[];
    public hruCharacteristicsSummaries: TreatmentBMPHRUCharacteristicsSummarySimpleDto[] = [];

    constructor(
        private regionalSubbasinService: RegionalSubbasinService,
        private utilityFunctionsService: UtilityFunctionsService,
        private groupByPipe: GroupByPipe,
        private sumPipe: SumPipe
    ) {}

    ngOnInit(): void {
        this.landUseStatsColumnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Land Use", "LandUse"),
            this.utilityFunctionsService.createDecimalColumnDef("Area (ac)", "Area"),
            this.utilityFunctionsService.createDecimalColumnDef("Impervious Cover (ac)", "ImperviousCover"),
        ];
        this.loadGeneratingUnitsColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("LGU ID", "LoadGeneratingUnitID", "LoadGeneratingUnitID", {
                InRouterLink: "/load-generating-units/",
            }),
            this.utilityFunctionsService.createLinkColumnDef("TreatmentBMP", "TreatmentBMPName", "TreatmentBMPID", {
                InRouterLink: "/treatment-bmps/",
                FieldDefinitionType: "TreatmentBMP",
            }),
            this.utilityFunctionsService.createLinkColumnDef("WaterQualityManagementPlan", "WaterQualityManagementPlanName", "WaterQualityManagementPlanID", {
                InRouterLink: "/water-quality-management-plans/",
                FieldDefinitionType: "WaterQualityManagementPlan",
            }),
            this.utilityFunctionsService.createLinkColumnDef("RegionalSubbasin", "RegionalSubbasinName", "RegionalSubbasinID", {
                InRouterLink: "/regional-subbasins/",
                FieldDefinitionType: "RegionalSubbasin",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Model Basin", "ModelBasinKey"),
            this.utilityFunctionsService.createDateColumnDef("Date HRU Requested", "DateHRURequested", "short"),
            this.utilityFunctionsService.createBooleanColumnDef("Is Empty", "IsEmptyResponseFromHRUService"),
        ];
        this.loadData();
    }

    ngOnChanges(changes: any): void {
        if (changes["regionalSubbasinID"] && !changes["regionalSubbasinID"].firstChange) {
            this.loadData();
        }
    }

    private loadData(): void {
        this.regionalSubbasin$ = this.regionalSubbasinService.getRegionalSubbasin(this.regionalSubbasinID);
        this.loadGeneratingUnits$ = this.regionalSubbasinService.listLoadGeneratingUnitsRegionalSubbasin(this.regionalSubbasinID);
        this.loadGeneratingUnitIDs$ = this.loadGeneratingUnits$.pipe(map((units) => units.map((u) => u.LoadGeneratingUnitID)));
        this.hruCharacteristics$ = this.regionalSubbasinService.listHRUCharacteristicsRegionalSubbasin(this.regionalSubbasinID).pipe(
            tap((hruCharacteristics) => {
                const grouped = this.groupByPipe.transform(hruCharacteristics, "HRUCharacteristicLandUseCodeDisplayName");
                this.hruCharacteristicsSummaries = Object.entries(grouped).map(
                    ([key, value]) =>
                        new TreatmentBMPHRUCharacteristicsSummarySimpleDto({
                            Area: this.sumPipe.transform(value, "Area"),
                            ImperviousCover: this.sumPipe.transform(value, "ImperviousAcres"),
                            LandUse: key,
                        })
                );
            })
        );
    }

    public handleMapReady(event: any, boundingBox?: any) {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        if (boundingBox && this.map) {
            this.map.fitBounds([
                [boundingBox.Bottom, boundingBox.Left],
                [boundingBox.Top, boundingBox.Right],
            ]);
        }
    }
}
