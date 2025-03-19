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

    @Input() ovtaAreaID: number;
    public layer;
    private wmsOptions: L.WMSOptions;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:TransectLine",
            transparent: true,
            format: "image/png",
            tiled: true,
        };

        if (this.ovtaAreaID) {
            this.wmsOptions.cql_filter = `OnlandVisualTrashAssessmentAreaID = '${this.ovtaAreaID}'`;
        }
        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
