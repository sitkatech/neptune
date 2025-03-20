import { AfterViewInit, Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";
import { CommonModule } from "@angular/common";

@Component({
    selector: "ovta-area-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./ovta-area-layer.component.html",
    styleUrl: "./ovta-area-layer.component.scss",
})
export class OvtaAreaLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    constructor() {
        super();
    }
    public wmsOptions: L.WMSOptions;
    public layer;

    ngAfterViewInit(): void {
        this.wmsOptions = {
            layers: "OCStormwater:OnlandVisualTrashAssessmentAreas",
            transparent: true,
            format: "image/png",
            tiled: true,
        };

        this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", this.wmsOptions);
        this.initLayer();
    }
}
