import { Component, Input } from "@angular/core";
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

    constructor(private regionalSubbasinService: RegionalSubbasinService) {}

    toggleTraceFromPointMode() {
        this.traceFromPointMode = !this.traceFromPointMode;
        if (this.traceFromPointMode) {
            this.enterTraceFromPointMode();
            return;
        }

        this.exitTraceFromPointMode();
    }

    enterTraceFromPointMode() {
        this.map.on("click", (e: L.PointerEvent) => {
            if (this.rsbTraceLayer) {
                this.map.removeLayer(this.rsbTraceLayer);
            }
            if (this.rsbTraceStartMarker) {
                this.map.removeLayer(this.rsbTraceStartMarker);
            }
            if (this.traceFromPointMode) {
                var latLng = new L.LatLng(e.latlng.lat, e.latlng.lng);
                this.map.rsbTraceStartMarker = L.marker(latLng, { icon: MarkerHelper.treatmentBMPMarker });
                this.map.rsbTraceStartMarker.addTo(this.map);
                let coordDto = new CoordinateDto({
                    Latitude: e.latlng.lat,
                    Longitude: e.latlng.lng,
                });
                this.regionalSubbasinService.graphTraceAsFeatureCollectionFromPointPost(coordDto).subscribe((response: any) => {
                    var pause = "blah";
                    debugger;
                    this.map.rsbTraceLayer = L.geoJson(
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
                    this.map.rsbTraceLayer.addTo(this.map);
                });
            }
            //setupRSBTraceTooltip();
            //setupLegend();
            //jQuery("#" + mapDivID).css("cursor", "crosshair");
            //jQuery("#traceButtonDisplayAction").text("Clear");
            //event.stopPropagation();
        });
    }

    exitTraceFromPointMode() {
        return;
    }
    // var toolTip;
    // var defaultCursorStyle;
    // var rsbTraceLegend;

    // //To gain our map context and add a few variables, we'll extend Neptune Maps
    // NeptuneMaps.Map.prototype.setupTraceFromPoint = function() {
    //     this.rsbTraceLayer = null;
    //     this.rsbTraceStartMarker = null;
    //     this.traceFromPointMode = false;
    //     this.map = this;
    //     defaultCursorStyle = document.getElementById(mapDivID).style.cursor;
    // }

    // function enterOrExitTraceMode() {
    //     if (this.map.traceFromPointMode) {
    //         exitTraceFromPointMode();
    //         return;
    //     }

    //     enterTraceFromPointMode();
    // }

    // function updateTooltipPosition(e) {
    //     toolTip.setLatLng(this.map.map.layerPointToLatLng(e.layerPoint));
    // }

    // function openRSBTraceTooltip(e) {
    //     this.map.map.openTooltip(toolTip);
    // }

    // function closeRSBTraceTooltip(e) {
    //     this.map.map.closeTooltip(toolTip);
    // }

    // function setupRSBTraceTooltip() {
    //     toolTip = L.tooltip({sticky: true, permanent: true})
    //                 //requires a default starting position, otherwise it will throw an error
    //                .setLatLng([0,0])
    //                .setContent("Click on the map to see<br/>the Regional Subbasins<br/>upstream and downstream<br/>of the selected point")
    //                .addTo(this.map.map);
    //     this.map.map.on('mousemove', updateTooltipPosition);
    //     jQuery("#" + mapDivID).on('mouseleave', closeRSBTraceTooltip);
    //     jQuery("#" + mapDivID).on('mouseenter', openRSBTraceTooltip);
    // }

    // function tearDownRSBTraceTooltip() {
    //     this.map.map.off('mousemove', updateTooltipPosition);
    //     jQuery("#" + mapDivID).off('mouseleave', closeRSBTraceTooltip);
    //     jQuery("#" + mapDivID).off('mouseenter', openRSBTraceTooltip);
    //     this.map.removeLayerFromMap(toolTip);
    //     toolTip = null;
    // }

    // function setupLegend() {
    //     rsbTraceLegend = new L.Control.Legend({
    //         position: 'topleft',
    //         collapsed: false
    //     });
    //     this.map.map.addControl(rsbTraceLegend);
    //     jQuery(rsbTraceLegend.getContainer()).addClass("rsb-trace-legend-container")
    //     jQuery(".rsb-trace-legend-container").append( jQuery(".rsb-trace-legend") );
    //     jQuery(".rsb-trace-legend").css("display", "");
    // }

    // function tearDownLegend() {
    //     jQuery("#legendContainer").append( jQuery(".rsb-trace-legend") );
    //     this.map.map.removeControl(rsbTraceLegend);
    // }

    // function enterTraceFromPointMode() {
    //    this.map.traceFromPointMode = true;
    //    this.map.map.on("click", getRSBTraceFromPoint);
    //    setupRSBTraceTooltip();
    //    setupLegend();
    //    jQuery("#" + mapDivID).css('cursor', 'crosshair');
    //    jQuery("#traceButtonDisplayAction").text("Clear");
    //    event.stopPropagation();
    // }

    // function exitTraceFromPointMode() {
    //     this.map.traceFromPointMode = false;
    //     this.map.map.off("click", getRSBTraceFromPoint);
    //     tearDownRSBTraceTooltip();
    //     tearDownLegend();
    //     jQuery("#" + mapDivID).css('cursor', defaultCursorStyle);
    //     jQuery("#traceButtonDisplayAction").text("View");
    //     this.map.removeLayerFromMap(this.map.rsbTraceLayer);
    //     this.map.removeLayerFromMap(this.map.rsbTraceStartMarker);
    //     this.map.rsbTraceLayer = null;
    //     this.map.rsbTraceStartMarker = null;
    // }
}
