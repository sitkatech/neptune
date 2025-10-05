import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { Component } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { HybridMapGridComponent } from "src/app/shared/components/hybrid-map-grid/hybrid-map-grid.component";
import { AsyncPipe } from "@angular/common";
import { ColDef } from "ag-grid-community";
import { Observable, tap } from "rxjs";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { StormwaterJurisdictionGridDto } from "src/app/shared/generated/model/stormwater-jurisdiction-grid-dto";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { AlertService } from "src/app/shared/services/alert.service";
import { Map } from "leaflet";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";

@Component({
    selector: "jurisdictions",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, HybridMapGridComponent, AsyncPipe, JurisdictionsLayerComponent],
    templateUrl: "./jurisdictions.component.html",
    styleUrl: "./jurisdictions.component.scss",
})
export class JurisdictionsComponent {
    public jurisdictions$: Observable<StormwaterJurisdictionGridDto[]>;
    public columnDefs: ColDef[];
    public customRichTextTypeID = NeptunePageTypeEnum.Jurisdiction;
    public map: Map;
    public layerControl: L.Control.Layers;
    public mapIsReady: boolean = false;
    public boundingBox$: Observable<BoundingBoxDto>;
    public selectedJurisdictionID: number;
    public isLoading: boolean = true;

    constructor(private jurisdictionService: StormwaterJurisdictionService, private utilityFunctionsService: UtilityFunctionsService, private alertService: AlertService) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Name", "StormwaterJurisdictionName", "StormwaterJurisdictionID", {
                InRouterLink: "/inventory/jurisdiction-detail/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("# of Users", "NumberOfUsers"),
            this.utilityFunctionsService.createBasicColumnDef("# of BMPs", "NumberOfBMPs"),
            this.utilityFunctionsService.createBasicColumnDef("Public BMP Visibility", "StormwaterJurisdictionPublicBMPVisibilityTypeName"),
            this.utilityFunctionsService.createBasicColumnDef("Public WQMP Visibility", "StormwaterJurisdictionPublicWQMPVisibilityTypeName"),
        ];
        this.jurisdictions$ = this.jurisdictionService.listStormwaterJurisdiction().pipe(tap(() => (this.isLoading = false)));
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

    public onSelectedJurisdictionIDChanged(selectedJurisdictionID: number, fromMap: boolean = false) {
        if (this.selectedJurisdictionID == selectedJurisdictionID) {
            return;
        }
        this.selectedJurisdictionID = selectedJurisdictionID;
        return this.selectedJurisdictionID;
    }
}
