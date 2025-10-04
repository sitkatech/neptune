import { Component, Input, OnChanges, AfterViewInit, Output, EventEmitter } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { WfsService } from "src/app/shared/services/wfs.service";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";

@Component({
    selector: "jurisdictions-layer",
    imports: [],
    templateUrl: "./jurisdictions-layer.component.html",
    styleUrls: ["./jurisdictions-layer.component.scss"],
})
export class JurisdictionsLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    private mapClickHandlerWired = false;
    @Input() selectedJurisdictionID?: number;
    @Input() layerControl: L.Control.Layers;
    @Input() map: L.Map;
    @Input() interactive: boolean = false;
    @Input() showLayerByDefault: boolean = false;
    @Output() jurisdictionSelected = new EventEmitter<number>();
    // 'layer' will be the default WMS layer
    public wfsLayer: L.FeatureGroup;
    public layer: L.Layer; // <-- Add this property
    private styleDictionary = {
        Default: {
            color: "#FF6C2D",
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
    private overlayAddedToControl = false;

    constructor(private wfsService: WfsService, private groupByPipe: GroupByPipe) {
        super();
    }

    private createWmsLayerIfNeeded() {
        if (!this.layer) {
            const wmsOptions: L.WMSOptions = {
                layers: "OCStormwater:Jurisdictions",
                transparent: true,
                format: "image/png",
                tiled: true,
                styles: "jurisdiction_orange",
                cql_filter: "1=1",
            } as any;
            this.layer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", wmsOptions);
        }
        // Add to layerControl only once, after both layer and layerControl are available
        if (this.layer && this.layerControl && !this.overlayAddedToControl) {
            try {
                this.layerControl.addOverlay(this.layer, "Jurisdictions");
                this.overlayAddedToControl = true;
            } catch (err) {
                console.error("[JurisdictionsLayer] Error adding overlay:", err, this.layer);
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
        if (changes.selectedJurisdictionID && !changes.selectedJurisdictionID.firstChange) {
            this.addSelectedJurisdictionVector(changes.selectedJurisdictionID.currentValue);
        }
    }

    private wireMapClickHandler() {
        if (this.interactive && this.map && !this.mapClickHandlerWired) {
            this.map.on("click", this.onMapClick.bind(this));
            this.mapClickHandlerWired = true;
        }
    }

    private addSelectedJurisdictionVector(jurisdictionID: number) {
        // Remove previous vector overlay
        if (this.wfsLayer && this.map) {
            this.map.removeLayer(this.wfsLayer);
        }
        this.wfsLayer = L.featureGroup();
        const cql_filter = `StormwaterJurisdictionID = ${jurisdictionID}`;
        this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:Jurisdictions", cql_filter, "StormwaterJurisdictionID").subscribe((response) => {
            if (response.length == 0) return;
            const featuresGrouped = this.groupByPipe.transform(response, "properties.StormwaterJurisdictionID");
            Object.keys(featuresGrouped).forEach((id) => {
                const geoJson = L.geoJSON(featuresGrouped[id], {
                    style: this.styleDictionary["Highlight"],
                });
                geoJson.on("click", () => {
                    this.jurisdictionSelected.emit(Number(id));
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
            layers: "OCStormwater:Jurisdictions",
            query_layers: "OCStormwater:Jurisdictions",
            styles: "jurisdiction_orange",
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
                const jurisdictionID = data.features[0].properties.StormwaterJurisdictionID;
                if (jurisdictionID) {
                    this.jurisdictionSelected.emit(jurisdictionID);
                }
            }
        } catch (err) {
            console.warn("WMS GetFeatureInfo failed:", err);
        }
    }
}
