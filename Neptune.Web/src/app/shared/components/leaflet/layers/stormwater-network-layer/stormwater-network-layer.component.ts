import { CommonModule } from "@angular/common";
import { Component, OnChanges } from "@angular/core";
import * as L from "leaflet";
import * as esri from "esri-leaflet";
import { MapLayerBase } from "../map-layer-base.component";
@Component({
    selector: "stormwater-network-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./stormwater-network-layer.component.html",
    styleUrls: ["./stormwater-network-layer.component.scss"],
})
export class StormwaterNetworkLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    public layer;

    ngAfterViewInit(): void {
        this.layer = esri.dynamicMapLayer({
            url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        });
        this.initLayer();
    }
}
