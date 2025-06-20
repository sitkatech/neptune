import { AfterViewInit, Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";
import { CommonModule } from "@angular/common";

@Component({
    selector: "ovta-areas-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./ovta-areas-layer.component.html",
    styleUrl: "./ovta-areas-layer.component.scss",
})
export class OvtaAreasLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    constructor() {
        super();
    }
    @Input() selectedJurisdictionID: number;
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:OnlandVisualTrashAssessmentAreas",
            transparent: true,
            format: "image/png",
            tiled: true,
        };
        if (this.selectedJurisdictionID) {
            this.wmsOptions.cql_filter = `StormwaterJurisdictionID=${this.selectedJurisdictionID}`;
        }
        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }

    ngOnChanges(changes: any): void {
        if (!this.layer) {
            return;
        }
        this.layer.wmsParams.cql_filter = `StormwaterJurisdictionID=${this.selectedJurisdictionID}`;
        this.layer.redraw();
    }
}
