import { Component, Input, Output, EventEmitter } from "@angular/core";
import { GenericWmsWfsLayerComponent } from "../generic-wms-wfs-layer/generic-wms-wfs-layer.component";
import * as L from "leaflet";

@Component({
    selector: "load-generating-units-layer",
    templateUrl: "./load-generating-units-layer.component.html",
    styleUrls: ["./load-generating-units-layer.component.scss"],
    imports: [GenericWmsWfsLayerComponent],
})
export class LoadGeneratingUnitsLayerComponent {
    @Input() map: L.Map;
    @Input() layerControl: any;
    @Input() interactive: boolean = false;
    @Input() displayOnLoad: boolean = true;
    @Input() sortOrder: number = 1;
    @Input() selectedID: number;
    @Input() wmsStyle: string = "load_generating_unit";
    wfsFeatureType: string = "OCStormwater:LoadGeneratingUnits";
    identifierProperty: string = "LoadGeneratingUnitID";
    @Input() overlayLabel: string = "Load Generating Units";
    @Input() selectedStyle: L.PathOptions = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };
    @Input() filterToIDs: number[];
    cqlFilter: string;
    @Output() selected = new EventEmitter<number>();

    ngOnChanges(): void {
        if (this.filterToIDs && this.filterToIDs.length > 0) {
            this.cqlFilter = `${this.identifierProperty} in (${this.filterToIDs.join(",")})`;
        } else {
            this.cqlFilter = "1=1";
        }
    }
}
