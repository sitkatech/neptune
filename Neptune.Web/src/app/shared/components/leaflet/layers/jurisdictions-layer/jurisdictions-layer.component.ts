import { Component, Input, Output, EventEmitter } from "@angular/core";
import { GenericWmsWfsLayerComponent } from "../generic-wms-wfs-layer/generic-wms-wfs-layer.component";

@Component({
    selector: "jurisdictions-layer",
    templateUrl: "./jurisdictions-layer.component.html",
    styleUrls: ["./jurisdictions-layer.component.scss"],
    imports: [GenericWmsWfsLayerComponent],
})
export class JurisdictionsLayerComponent {
    @Input() map: any;
    @Input() layerControl: any;
    @Input() interactive: boolean = false;
    @Input() displayOnLoad: boolean = true;
    @Input() sortOrder: number = 1;
    @Input() selectedID: number;
    wmsLayerName: string = "OCStormwater:Jurisdictions";
    @Input() wmsStyle: string = "jurisdiction_orange";
    wfsFeatureType: string = "OCStormwater:Jurisdictions";
    identifierProperty: string = "StormwaterJurisdictionID";
    @Input() overlayLabel: string = "Jurisdictions";
    @Input() styleDictionary: any = {
        Highlight: { color: "#fcfc12", weight: 2, opacity: 0.65, fillOpacity: 0.1 },
    };
    @Output() selected = new EventEmitter<number>();

    onSelected(id: number) {
        this.selected.emit(id);
    }
}
