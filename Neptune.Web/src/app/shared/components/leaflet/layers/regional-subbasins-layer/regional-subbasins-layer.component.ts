import { Component, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
@Component({
    selector: "regional-subbasins-layer",
    imports: [],
    templateUrl: "./regional-subbasins-layer.component.html",
    styleUrls: ["./regional-subbasins-layer.component.scss"],
})
export class RegionalSubbasinsLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:RegionalSubbasins",
            transparent: true,
            format: "image/png",
            tiled: true,
        } as any;

        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
