import { AfterViewInit, Component, OnChanges } from "@angular/core";
import * as L from "leaflet";
import { environment } from "src/environments/environment";
import { MapLayerBase } from "../map-layer-base.component";

@Component({
    selector: "parcel-layer",
    standalone: true,
    imports: [],
    templateUrl: "./parcel-layer.component.html",
    styleUrl: "./parcel-layer.component.scss",
})
export class ParcelLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    constructor() {
        super();
    }
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:Parcels",
            transparent: true,
            format: "image/png",
            tiled: true,
        };

        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
