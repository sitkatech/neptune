import { Component, Input, OnChanges, AfterViewInit, Output, EventEmitter } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { WfsService } from "src/app/shared/services/wfs.service";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";

@Component({
    selector: "regional-subbasins-layer",
    imports: [],
    templateUrl: "./regional-subbasins-layer.component.html",
    styleUrls: ["./regional-subbasins-layer.component.scss"],
})
export class RegionalSubbasinsLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    private mapClickHandlerWired = false;
    private overlayAddedToControl = false;
    @Input() selectedRegionalSubbasinID?: number;
    @Input() layerControl: L.Control.Layers;
    @Input() map: L.Map;
    @Input() interactive: boolean = false;
    @Input() showLayerByDefault: boolean = false;
    @Output() regionalSubbasinSelected = new EventEmitter<number>();
    public wfsLayer: L.FeatureGroup;
    public layer: L.Layer;
    private styleDictionary = {
        Default: {
            color: "#2D6CFF",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
        Highlight: {
            color: "#fcfc12",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
    };

    constructor(private wfsService: WfsService, private groupByPipe: GroupByPipe) {
        super();
    }

    private createWmsLayerIfNeeded() {
        if (!this.layer) {
            const wmsOptions: L.WMSOptions = {
                layers: "OCStormwater:RegionalSubbasins",
                transparent: true,
                format: "image/png",
                tiled: true,
                styles: "regional_subbasin",
                cql_filter: "1=1",
            } as any;
            this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", wmsOptions);
        }
        // Add to layerControl only once, after both layer and layerControl are available
        if (this.layer && this.layerControl && !this.overlayAddedToControl) {
            try {
                this.layerControl.addOverlay(this.layer, "Regional Subbasins");
                this.overlayAddedToControl = true;
            } catch (err) {
                console.error("[RegionalSubbasinsLayer] Error adding overlay:", err, this.layer);
            }
        }
    }
    ngAfterViewInit(): void {
        // Initialization is now handled in ngOnChanges
    }

    ngOnChanges(changes: any): void {
        this.createWmsLayerIfNeeded();
        // Only add to map if showLayerByDefault is true
        if (this.layer && this.map && this.showLayerByDefault && !this.map.hasLayer(this.layer)) {
            this.layer.addTo(this.map);
        }
        // Do NOT add to map by default if showLayerByDefault is false
        this.wireMapClickHandler();
        if (changes.selectedRegionalSubbasinID && !changes.selectedRegionalSubbasinID.firstChange) {
            this.addSelectedRegionalSubbasinVector(changes.selectedRegionalSubbasinID.currentValue);
        }
    }

    private wireMapClickHandler() {
        if (this.interactive && this.map && !this.mapClickHandlerWired) {
            this.map.on("click", this.onMapClick.bind(this));
            this.mapClickHandlerWired = true;
        }
    }

    private addSelectedRegionalSubbasinVector(regionalSubbasinID: number) {
        // Remove previous vector overlay
        if (this.wfsLayer && this.map) {
            this.map.removeLayer(this.wfsLayer);
        }
        this.wfsLayer = L.featureGroup();
        const cql_filter = `RegionalSubbasinID = ${regionalSubbasinID}`;
        this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:RegionalSubbasins", cql_filter, "RegionalSubbasinID").subscribe((response) => {
            if (response.length == 0) return;
            const featuresGrouped = this.groupByPipe.transform(response, "properties.RegionalSubbasinID");
            Object.keys(featuresGrouped).forEach((id) => {
                const geoJson = L.geoJSON(featuresGrouped[id], {
                    style: this.styleDictionary["Highlight"],
                });
                geoJson.on("click", () => {
                    this.regionalSubbasinSelected.emit(Number(id));
                });
                geoJson.addTo(this.wfsLayer);
                // Zoom to bounds
                if ("getBounds" in geoJson && typeof geoJson.getBounds === "function" && this.map) {
                    this.map.fitBounds(geoJson.getBounds());
                }
            });
            if (this.map) {
                this.wfsLayer.addTo(this.map);
            }
        });
    }

    private async onMapClick(e: L.LeafletMouseEvent) {
        // Build WMS GetFeatureInfo request
        const wmsUrl = environment.geoserverMapServiceUrl + "/wms";
        const params = {
            service: "WMS",
            version: "1.1.1",
            request: "GetFeatureInfo",
            layers: "OCStormwater:RegionalSubbasins",
            query_layers: "OCStormwater:RegionalSubbasins",
            styles: "regional_subbasin",
            bbox: this.map.getBounds().toBBoxString(),
            width: this.map.getSize().x,
            height: this.map.getSize().y,
            srs: "EPSG:4326",
            format: "image/png",
            info_format: "application/json",
            x: Math.round(this.map.layerPointToContainerPoint(e.layerPoint).x),
            y: Math.round(this.map.layerPointToContainerPoint(e.layerPoint).y),
        };
        const urlParams = new URLSearchParams(params as any).toString();
        const url = `${wmsUrl}?${urlParams}`;
        try {
            const response = await fetch(url);
            const data = await response.json();
            if (data.features && data.features.length > 0) {
                const subbasinID = data.features[0].properties.RegionalSubbasinID;
                if (subbasinID) {
                    this.regionalSubbasinSelected.emit(subbasinID);
                }
            }
        } catch (err) {
            console.warn("WMS GetFeatureInfo failed:", err);
        }
    }
}
