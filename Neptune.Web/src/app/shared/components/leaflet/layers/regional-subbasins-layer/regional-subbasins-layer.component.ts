import { Component, Input, Output, EventEmitter } from "@angular/core";
import { GenericWmsWfsLayerComponent } from "../generic-wms-wfs-layer/generic-wms-wfs-layer.component";
import { OverlayMode } from "../generic-wms-wfs-layer/overlay-mode.enum";

@Component({
    selector: "regional-subbasins-layer",
    templateUrl: "./regional-subbasins-layer.component.html",
    styleUrls: ["./regional-subbasins-layer.component.scss"],
    imports: [GenericWmsWfsLayerComponent],
})
export class RegionalSubbasinsLayerComponent {
    /**
     * OverlayMode usage patterns:
     * - Single: Show a single feature (WFS only, not in layer control)
     * - ReferenceOnly: Show all features via WMS (reference only, no interactivity)
     * - ReferenceWithInteractivity: Show all features via WMS, with selection/highlighting via WFS and map/grid interactivity
     */

    // Constants
    readonly WFS_FEATURE_TYPE = "OCStormwater:RegionalSubbasins";
    readonly WMS_LAYER_NAME = "OCStormwater:RegionalSubbasins";
    readonly IDENTIFIER_PROPERTY = "RegionalSubbasinID";
    readonly OVERLAY_LABEL = "Regional Subbasins";
    readonly WMS_STYLE = "regional_subbasin";
    readonly DEFAULT_SELECTED_STYLE: L.PathOptions = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    // Inputs/Outputs
    @Input() mode: OverlayMode = OverlayMode.ReferenceOnly;
    @Input() map: L.Map;
    @Input() layerControl: any;
    @Input() sortOrder: number = 1;
    @Input() selectedID: number;
    @Input() filterToIDs: number[];
    @Input() displayOnLoad: boolean;
    @Output() selected = new EventEmitter<number>();

    // Internal state for generic layer
    wfsFeatureType: string = this.WFS_FEATURE_TYPE;
    identifierProperty: string = this.IDENTIFIER_PROPERTY;
    overlayLabel: string = this.OVERLAY_LABEL;
    wmsStyle: string = this.WMS_STYLE;
    selectedStyle: L.PathOptions = this.DEFAULT_SELECTED_STYLE;
    cqlFilter: string;
    interactive: boolean = false;
    addToLayerControl: boolean = true;
    wmsLayerName: string = this.WMS_LAYER_NAME;

    ngOnChanges(): void {
        switch (this.mode) {
            case OverlayMode.Single:
                this.displayOnLoad = true;
                this.interactive = false;
                this.addToLayerControl = false;
                this.wmsLayerName = null;
                this.cqlFilter = "1=0";
                break;
            case OverlayMode.ReferenceOnly:
                // Only override displayOnLoad if not set externally
                if (this.displayOnLoad === undefined || this.displayOnLoad === null) {
                    this.displayOnLoad = false;
                }
                this.interactive = false;
                this.addToLayerControl = true;
                this.wmsLayerName = this.WMS_LAYER_NAME;
                if (this.filterToIDs && this.filterToIDs.length > 0) {
                    this.cqlFilter = `${this.IDENTIFIER_PROPERTY} in (${this.filterToIDs.join(",")})`;
                } else {
                    this.cqlFilter = "1=1";
                }
                break;
            case OverlayMode.ReferenceWithInteractivity:
                this.displayOnLoad = true;
                this.interactive = true;
                this.addToLayerControl = true;
                this.wmsLayerName = this.WMS_LAYER_NAME;
                if (this.filterToIDs && this.filterToIDs.length > 0) {
                    this.cqlFilter = `${this.IDENTIFIER_PROPERTY} in (${this.filterToIDs.join(",")})`;
                } else {
                    this.cqlFilter = "1=1";
                }
                break;
        }
    }
}
