import { Component, Input, Output, EventEmitter } from "@angular/core";
import { GenericWmsWfsLayerComponent } from "../generic-wms-wfs-layer/generic-wms-wfs-layer.component";

@Component({
    selector: "regional-subbasins-layer",
    templateUrl: "./regional-subbasins-layer.component.html",
    styleUrls: ["./regional-subbasins-layer.component.scss"],
    imports: [GenericWmsWfsLayerComponent],
})
export class RegionalSubbasinsLayerComponent {
    @Input() map: L.Map;
    @Input() layerControl: any;
    @Input() interactive: boolean = false;
    @Input() displayOnLoad: boolean = true;
    @Input() sortOrder: number = 1;
    @Input() selectedID: number;
    @Input() wmsStyle: string = "regional_subbasin";
    wfsFeatureType: string = "OCStormwater:RegionalSubbasins";
    identifierProperty: string = "RegionalSubbasinID";
    @Input() overlayLabel: string = "Regional Subbasins";
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
