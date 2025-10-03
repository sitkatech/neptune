import { Component } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { HybridMapGridComponent } from "src/app/shared/components/hybrid-map-grid/hybrid-map-grid.component";
import { AsyncPipe } from "@angular/common";
import { ColDef } from "ag-grid-community";
import { Observable, tap } from "rxjs";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { StormwaterJurisdictionDto } from "src/app/shared/generated/model/stormwater-jurisdiction-dto";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { AlertService } from "src/app/shared/services/alert.service";
import { Map } from "leaflet";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { SelectedJurisdictionLayerComponent } from "src/app/shared/components/leaflet/layers/selected-jurisdiction-layer/selected-jurisdiction-layer.component";

@Component({
    selector: "jurisdictions",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, HybridMapGridComponent, AsyncPipe, SelectedJurisdictionLayerComponent],
    templateUrl: "./jurisdictions.component.html",
    styleUrl: "./jurisdictions.component.scss",
})
export class JurisdictionsComponent {
    public jurisdictions$: Observable<StormwaterJurisdictionDto[]>;
    public columnDefs: ColDef[];
    public customRichTextTypeID = NeptunePageTypeEnum.Jurisdiction;
    public map: Map;
    public layerControl: L.Control.Layers;
    public mapIsReady: boolean = false;
    public selectedJurisdictionID: number;
    public isLoading: boolean = true;

    constructor(private jurisdictionService: StormwaterJurisdictionService, private utilityFunctionsService: UtilityFunctionsService, private alertService: AlertService) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Name", "Organization.OrganizationName"),
            this.utilityFunctionsService.createBasicColumnDef("# of Users", "NumberOfUsers"),
            this.utilityFunctionsService.createBasicColumnDef("# of BMPs", "NumberOfBMPs"),
            this.utilityFunctionsService.createBasicColumnDef("Public BMP Visibility", "PublicBMPVisibilityType.PublicBMPVisibilityTypeName"),
            this.utilityFunctionsService.createBasicColumnDef("Public WQMP Visibility", "PublicWQMPVisibilityType.PublicWQMPVisibilityTypeName"),
        ];
        this.jurisdictions$ = this.jurisdictionService.listStormwaterJurisdiction().pipe(tap(() => (this.isLoading = false)));
    }

    public handleMapReady(event: NeptuneMapInitEvent) {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }

    public onSelectedJurisdictionIDChanged(selectedJurisdictionID: number) {
        if (this.selectedJurisdictionID == selectedJurisdictionID) {
            return;
        }

        this.selectedJurisdictionID = selectedJurisdictionID;
        return this.selectedJurisdictionID;
    }
}
