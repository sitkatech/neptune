import { CommonModule } from "@angular/common";
import { Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
@Component({
    selector: "trash-generating-unit-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./trash-generating-unit-layer.component.html",
    styleUrls: ["./trash-generating-unit-layer.component.scss"],
})
export class TrashGeneratingUnitLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:TrashGeneratingUnits",
            transparent: true,
            format: "image/png",
            tiled: true,
            styles: "tgu_style",
        };
        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
