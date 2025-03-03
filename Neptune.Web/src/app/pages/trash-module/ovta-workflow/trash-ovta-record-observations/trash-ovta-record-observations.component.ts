import { Component } from "@angular/core";
import * as L from "leaflet";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import { JurisdictionsLayerComponent } from "../../../../shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";

@Component({
    selector: "trash-ovta-record-observations",
    standalone: true,
    imports: [PageHeaderComponent, NeptuneMapComponent, JurisdictionsLayerComponent],
    templateUrl: "./trash-ovta-record-observations.component.html",
    styleUrl: "./trash-ovta-record-observations.component.scss",
})
export class TrashOvtaRecordObservationsComponent {
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public isLoadingSubmit = false;

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }
}
