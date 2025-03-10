import { Component, Input, OnChanges } from "@angular/core";
import * as L from "leaflet";
import { environment } from "src/environments/environment";
import { MapLayerBase } from "../map-layer-base.component";

@Component({
    selector: "transect-line-layer",
    standalone: true,
    imports: [],
    templateUrl: "./transect-line-layer.component.html",
    styleUrl: "./transect-line-layer.component.scss",
})
export class TransectLineLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    @Input() ovtaAreaName: string;
    @Input() jurisdictionID: number;
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:TransectLineExport",
            transparent: true,
            format: "image/png",
            tiled: true,
        };

        if (this.ovtaAreaName && this.jurisdictionID) {
            this.wmsOptions.cql_filter = `OVTAAreaName = '${this.ovtaAreaName}' and JurisID = ${this.jurisdictionID}`;
        }

        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
