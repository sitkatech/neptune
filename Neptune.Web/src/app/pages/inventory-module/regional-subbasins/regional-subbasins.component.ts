import { Component } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { HybridMapGridComponent } from "src/app/shared/components/hybrid-map-grid/hybrid-map-grid.component";
import { AsyncPipe } from "@angular/common";
import { ColDef } from "ag-grid-community";
import { Observable, tap } from "rxjs";
import { RegionalSubbasinService } from "src/app/shared/generated/api/regional-subbasin.service";
import { RegionalSubbasinDto } from "src/app/shared/generated/model/regional-subbasin-dto";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { Map } from "leaflet";
import { NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";

@Component({
    selector: "regional-subbasins",
    imports: [PageHeaderComponent, HybridMapGridComponent, AsyncPipe, RegionalSubbasinsLayerComponent, AlertDisplayComponent],
    templateUrl: "./regional-subbasins.component.html",
    styleUrl: "./regional-subbasins.component.scss",
})
export class RegionalSubbasinsComponent {
    public regionalSubbasins$: Observable<RegionalSubbasinDto[]>;
    public columnDefs: ColDef[];
    public map: Map;
    public layerControl: L.Control.Layers;
    public mapIsReady: boolean = false;
    public boundingBox$: Observable<BoundingBoxDto>;
    public selectedRegionalSubbasinID: number;
    public isLoading: boolean = true;

    constructor(
        private regionalSubbasinService: RegionalSubbasinService,
        private jurisdictionService: StormwaterJurisdictionService,
        private utilityFunctionsService: UtilityFunctionsService
    ) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Name", "DisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Watershed", "Watershed"),
            this.utilityFunctionsService.createBasicColumnDef("Area (sq mi)", "AreaSqMi"),
            this.utilityFunctionsService.createBasicColumnDef("# of Jurisdictions", "NumberOfJurisdictions"),
        ];
        this.regionalSubbasins$ = this.regionalSubbasinService.listRegionalSubbasin().pipe(tap(() => (this.isLoading = false)));
        this.boundingBox$ = this.jurisdictionService.getBoundingBoxStormwaterJurisdiction();
    }

    public handleMapReady(event: NeptuneMapInitEvent, boundingBox?: BoundingBoxDto) {
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

    public onSelectedRegionalSubbasinIDChanged(selectedRegionalSubbasinID: number, fromMap: boolean = false) {
        if (this.selectedRegionalSubbasinID == selectedRegionalSubbasinID) {
            return;
        }
        this.selectedRegionalSubbasinID = selectedRegionalSubbasinID;
        return this.selectedRegionalSubbasinID;
    }
}
