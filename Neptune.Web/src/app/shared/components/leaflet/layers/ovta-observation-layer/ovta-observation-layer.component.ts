import { Component, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";

@Component({
    selector: "ovta-observation-layer",
    standalone: true,
    imports: [],
    templateUrl: "./ovta-observation-layer.component.html",
    styleUrl: "./ovta-observation-layer.component.scss",
})
export class OvtaObservationLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "	OCStormwater:ObservationPointExport",
            transparent: true,
            format: "image/png",
            tiled: true,
        };

        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
