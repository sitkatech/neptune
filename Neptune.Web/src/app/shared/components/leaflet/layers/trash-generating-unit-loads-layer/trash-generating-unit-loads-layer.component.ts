
import { Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
@Component({
    selector: "trash-generating-unit-loads-layer",
    imports: [],
    templateUrl: "./trash-generating-unit-loads-layer.component.html",
    styleUrls: ["./trash-generating-unit-loads-layer.component.scss"]
})
export class TrashGeneratingUnitLoadsLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    @Input() style: string;
    @Input() selectedJurisdictionID: number;
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:TrashGeneratingUnits",
            transparent: true,
            format: "image/png",
            tiled: true,
            styles: this.style,
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
