import { CommonModule } from "@angular/common";
import { Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
@Component({
    selector: "delineations-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./delineations-layer.component.html",
    styleUrls: ["./delineations-layer.component.scss"],
})
export class DelineationsLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:Delineations",
            transparent: true,
            format: "image/png",
            tiled: true,
            cql_filter: "DelineationStatus = 'Verified' AND IsAnalyzedInModelingModule = 1",
        };

        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.layer["layerName"] = "Inventoried BMP Delineations";
        this.layer["legendImageSource"] = "./assets/main/map-legend-images/delineationVerified.png";
        this.initLayer();
    }
}
