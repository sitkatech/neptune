import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import "leaflet-gesture-handling";
import "leaflet.fullscreen";
import "leaflet-loading";
import "leaflet.markercluster";

@Component({
    selector: "trash-ovta-area-edit-location",
    standalone: true,
    imports: [PageHeaderComponent, NeptuneMapComponent],
    templateUrl: "./trash-ovta-area-edit-location.component.html",
    styleUrl: "./trash-ovta-area-edit-location.component.scss",
})
export class TrashOvtaAreaEditLocationComponent {
    public customRichTextTypeID = NeptunePageTypeEnum.EditOVTAArea;
    public mapHeight = window.innerHeight - window.innerHeight * 0.2 + "px";
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }
}
