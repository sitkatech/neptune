import { CommonModule } from "@angular/common";
import { Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
@Component({
    selector: "jurisdictions-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./jurisdictions-layer.component.html",
    styleUrls: ["./jurisdictions-layer.component.scss"],
})
export class JurisdictionsLayerComponent extends MapLayerBase implements OnChanges {
    constructor() {
        super();
    }
    @Input() jurisdictionID: number;
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:Jurisdictions",
            transparent: true,
            format: "image/png",
            tiled: true,
            styles: "jurisdiction_orange",
        };

        if (this.jurisdictionID) {
            this.wmsOptions.cql_filter = `StormwaterJurisdictionID = ${this.jurisdictionID}`;
        }
        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
