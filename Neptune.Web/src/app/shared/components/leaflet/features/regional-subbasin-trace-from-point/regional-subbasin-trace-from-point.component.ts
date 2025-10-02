import { Component, Input, Output, EventEmitter, ChangeDetectorRef, Renderer2 } from "@angular/core";
import * as L from "leaflet";
import "leaflet-geometryutil";
import "leaflet-arrowheads";
import { RegionalSubbasinService } from "src/app/shared/generated/api/regional-subbasin.service";
import { CoordinateDto } from "src/app/shared/generated/model/coordinate-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { LegendItem } from "src/app/shared/models/legend-item";

@Component({
    selector: "regional-subbasin-trace-from-point",
    imports: [],
    templateUrl: "./regional-subbasin-trace-from-point.component.html",
    styleUrl: "./regional-subbasin-trace-from-point.component.scss",
})
export class RegionalSubbasinTraceFromPointComponent {
    @Input() map: any;
    @Input() layerControl: any;
    @Input() cursorStyle: string;
    @Output() cursorStyleChange = new EventEmitter();
    @Input() legendItems: LegendItem[];
    @Output() legendItemsChange = new EventEmitter<LegendItem[]>();

    defaultCursorStyle: string;
    traceFromPointMode: boolean = false;

    rsbTraceLegendItemGroupText: string = "RSBTraceLegendItem";
    rsbTraceLegendItems: LegendItem[] = [
        new LegendItem({
            Group: this.rsbTraceLegendItemGroupText,
            Title: "Drainage Areas",
        }),
        new LegendItem({
            Group: this.rsbTraceLegendItemGroupText,
            Text: "Drainage Area Point of Interest",
            Icon: "MapMarker",
            IconColor: "#f2bbe0",
        }),
        new LegendItem({
            Group: this.rsbTraceLegendItemGroupText,
            Text: "Direction of Flow",
            Icon: "FlowArrow",
            IconColor: "#0099ab",
        }),
        new LegendItem({
            Group: this.rsbTraceLegendItemGroupText,
            Text: "Upstream Regional Subbasin",
            Icon: "Square",
            IconColor: "#5600ff",
            IconFillOpacity: 0.5,
        }),
        new LegendItem({
            Group: this.rsbTraceLegendItemGroupText,
            Text: "Downstream Regional Subbasin",
            Icon: "Square",
            IconColor: "#ff4345",
            IconFillOpacity: 0.5,
        }),
    ];

    rsbTraceLayer: L.LayerGroup = null;
    rsbTraceStartMarker: L.Layer = null;
    tooltip: L.Tooltip;

    private arrowHeadsStyle = {
        frequency: "100px",
        size: "10px",
        fill: true,
        color: "#0099ab",
        fillColor: "#FFFFFF",
    };

    private upstreamStyle = {
        fillColor: "#5600FF",
        fill: true,
        fillOpacity: 0.4,
        color: "#5600FF",
        weight: 1,
        stroke: true,
    };

    private downstreamStyle = {
        fillColor: "#FF4345",
        fill: true,
        fillOpacity: 0.4,
        color: "#FF4345",
        weight: 1,
        stroke: true,
    };

    constructor(private regionalSubbasinService: RegionalSubbasinService, private cdr: ChangeDetectorRef, private renderer: Renderer2) {}

    clearAddedTraceLayers() {
        if (this.rsbTraceLayer) {
            this.map.removeLayer(this.rsbTraceLayer);
        }
        if (this.rsbTraceStartMarker) {
            this.map.removeLayer(this.rsbTraceStartMarker);
        }
    }

    getRSBTraceFromPoint(e: L.LeafletMouseEvent) {
        this.clearAddedTraceLayers();
        if (this.traceFromPointMode) {
            var latLng = new L.LatLng(e.latlng.lat, e.latlng.lng);
            this.rsbTraceStartMarker = L.marker(latLng, { icon: MarkerHelper.pinkMarker });
            this.rsbTraceStartMarker.addTo(this.map);
            let coordDto = new CoordinateDto({
                Latitude: e.latlng.lat,
                Longitude: e.latlng.lng,
            });
            this.regionalSubbasinService.graphTraceAsFeatureCollectionFromPointPost(coordDto).subscribe((response: any) => {
                this.rsbTraceLayer = L.geoJson(
                    response.features.sort((a, b) => b.geometry.type.localeCompare(a.geometry.type)),
                    {
                        style: (feature) => {
                            var depth = feature.properties.Depth;
                            if (feature.geometry.type == "LineString") {
                                var depthForWeight = Math.abs(depth) * 0.1;
                                return {
                                    weight: depthForWeight > 2.5 ? 0.5 : 3 - depthForWeight,
                                    color: "#0099ab",
                                };
                            }

                            return depth >= 0 ? this.upstreamStyle : this.downstreamStyle;
                        },
                        arrowheads: this.arrowHeadsStyle,
                        interactive: false,
                    } as any
                );
                this.rsbTraceLayer.addTo(this.map);
            });
        }
    }

    updateTooltipPosition(e) {
        if (!this.tooltip) {
            return;
        }

        this.tooltip.setLatLng(this.map.layerPointToLatLng(e.layerPoint));
    }

    openRSBTraceTooltip() {
        this.map.openTooltip(this.tooltip);
    }

    closeRSBTraceTooltip() {
        this.map.closeTooltip(this.tooltip);
    }

    setupRSBTraceTooltip() {
        this.tooltip = L.tooltip({ sticky: true, permanent: true })
            //requires a default starting position, otherwise it will throw an error
            .setLatLng([0, 0])
            .setContent("Click on the map to see<br/>the Regional Subbasins<br/>upstream and downstream<br/>of the selected point")
            .addTo(this.map);
        this.map.on("mousemove", this.updateTooltipPosition, this);
        L.DomEvent.on(this.map.getContainer(), "mouseleave", this.closeRSBTraceTooltip, this);
        L.DomEvent.on(this.map.getContainer(), "mouseenter", this.openRSBTraceTooltip, this);
    }

    tearDownRSBTraceTooltip() {
        this.map.off("mousemove", this.updateTooltipPosition);
        L.DomEvent.off(this.map.getContainer(), "mouseleave", this.closeRSBTraceTooltip, this);
        L.DomEvent.off(this.map.getContainer(), "mouseenter", this.openRSBTraceTooltip, this);
        this.map.removeLayer(this.tooltip);
        this.tooltip = null;
    }

    setupLegend() {
        this.legendItems = this.legendItems.concat(this.rsbTraceLegendItems);
        this.legendItemsChange.emit(this.legendItems);
    }

    tearDownLegend() {
        this.legendItems = this.legendItems.filter((x) => x.Group !== this.rsbTraceLegendItemGroupText);
        this.legendItemsChange.emit(this.legendItems);
    }

    enterTraceFromPointMode() {
        this.map.on("click", this.getRSBTraceFromPoint, this);
        this.setupRSBTraceTooltip();
        this.setupLegend();
        this.defaultCursorStyle = this.cursorStyle;
        this.cursorStyle = "crosshair";
        this.cursorStyleChange.emit(this.cursorStyle);
        this.cdr.detectChanges();
    }

    exitTraceFromPointMode() {
        this.map.off("click", this.getRSBTraceFromPoint, this);
        this.tearDownRSBTraceTooltip();
        this.tearDownLegend();
        this.clearAddedTraceLayers();
        this.cursorStyle = this.defaultCursorStyle;
        this.cursorStyleChange.emit(this.cursorStyle);
        this.cdr.detectChanges();
    }

    toggleTraceFromPointMode(event: any) {
        this.traceFromPointMode = !this.traceFromPointMode;
        if (this.traceFromPointMode) {
            this.enterTraceFromPointMode();
            event.stopPropagation();
            return;
        }

        this.exitTraceFromPointMode();
    }
}
