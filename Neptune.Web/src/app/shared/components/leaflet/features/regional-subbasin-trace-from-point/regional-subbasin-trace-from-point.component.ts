import { Component, Input, Output, EventEmitter, HostListener, ChangeDetectorRef } from "@angular/core";
import * as L from "leaflet";
import * as LGU from "leaflet-geometryutil";
import "leaflet-arrowheads";
import "leaflet-legend";
import { RegionalSubbasinService } from "src/app/shared/generated/api/regional-subbasin.service";
import { CoordinateDto } from "src/app/shared/generated/model/coordinate-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";

@Component({
    selector: "regional-subbasin-trace-from-point",
    standalone: true,
    imports: [],
    templateUrl: "./regional-subbasin-trace-from-point.component.html",
    styleUrl: "./regional-subbasin-trace-from-point.component.scss",
})
export class RegionalSubbasinTraceFromPointComponent {
    @Input() map: any;
    @Input() layerControl: any;
    @Input() cursorStyle: string;
    defaultCursorStyle: string;
    @Output() cursorStyleChange = new EventEmitter();
    traceFromPointMode: boolean = false;

    rsbTraceLayer: L.Layers = null;
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

    constructor(private regionalSubbasinService: RegionalSubbasinService, private cdr: ChangeDetectorRef) {}

    clearAddedTraceLayers() {
        if (this.rsbTraceLayer) {
            this.map.removeLayer(this.rsbTraceLayer);
        }
        if (this.rsbTraceStartMarker) {
            this.map.removeLayer(this.rsbTraceStartMarker);
        }
    }

    getRSBTraceFromPoint(e: L.PointerEvent) {
        this.clearAddedTraceLayers();
        if (this.traceFromPointMode) {
            var latLng = new L.LatLng(e.latlng.lat, e.latlng.lng);
            this.rsbTraceStartMarker = L.marker(latLng, { icon: MarkerHelper.treatmentBMPMarker });
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
                                };
                            }

                            return depth >= 0 ? this.upstreamStyle : this.downstreamStyle;
                        },
                        arrowheads: this.arrowHeadsStyle,
                    }
                );
                this.rsbTraceLayer.addTo(this.map);
            });
        }
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
        L.DomEvent.on(this.map.getContainer(), "mouseleave", this.closeRSBTraceTooltip, this);
        L.DomEvent.on(this.map.getContainer(), "mouseenter", this.openRSBTraceTooltip, this);
        this.map.removeLayer(this.tooltip);
        this.tooltip = null;
    }

    enterTraceFromPointMode() {
        this.map.on("click", this.getRSBTraceFromPoint, this);
        this.setupRSBTraceTooltip();
        //setupLegend();
        this.defaultCursorStyle = this.cursorStyle;
        this.cursorStyle = "crosshair";
        this.cursorStyleChange.emit(this.cursorStyle);
        this.cdr.detectChanges();
    }

    exitTraceFromPointMode() {
        this.map.off("click", this.getRSBTraceFromPoint, this);
        this.tearDownRSBTraceTooltip();
        this.clearAddedTraceLayers();
        this.cursorStyle = this.defaultCursorStyle;
        this.cursorStyleChange.emit(this.cursorStyle);
        this.cdr.detectChanges();
    }
    // var rsbTraceLegend;

    // function setupLegend() {
    //     rsbTraceLegend = new L.Control.Legend({
    //         position: 'topleft',
    //         collapsed: false
    //     });
    //     this.map.addControl(rsbTraceLegend);
    //     jQuery(rsbTraceLegend.getContainer()).addClass("rsb-trace-legend-container")
    //     jQuery(".rsb-trace-legend-container").append( jQuery(".rsb-trace-legend") );
    //     jQuery(".rsb-trace-legend").css("display", "");
    // }

    // function tearDownLegend() {
    //     jQuery("#legendContainer").append( jQuery(".rsb-trace-legend") );
    //     this.map.removeControl(rsbTraceLegend);
    // }
}
