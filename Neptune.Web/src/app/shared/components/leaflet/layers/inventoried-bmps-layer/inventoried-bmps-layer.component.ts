import { CommonModule } from "@angular/common";
import { Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import "leaflet.markercluster";
import { MapLayerBase } from "../map-layer-base.component";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { TreatmentBMPDisplayDto } from "src/app/shared/generated/model/treatment-bmp-display-dto";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { Observable, tap } from "rxjs";
@Component({
    selector: "inventoried-bmps-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./inventoried-bmps-layer.component.html",
    styleUrls: ["./inventoried-bmps-layer.component.scss"],
})
export class InventoriedBMPsLayerComponent extends MapLayerBase implements OnChanges {
    public layer: L.markerClusterGroup = L.markerClusterGroup({
        iconCreateFunction: function (cluster) {
            var childCount = cluster.getChildCount();

            return new L.DivIcon({
                html: "<div><span>" + childCount + "</span></div>",
                className: "marker-cluster",
                iconSize: new L.Point(40, 40),
            });
        },
    });

    public treatmentBMPs$: Observable<TreatmentBMPDisplayDto[]>;

    constructor(private treatmentBMPService: TreatmentBMPService) {
        super();
    }

    ngAfterViewInit(): void {
        this.treatmentBMPs$ = this.treatmentBMPService.treatmentBMPsGet().pipe(
            tap((treatmentBMPs) => {
                const inventoriedTreatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(treatmentBMPs.filter((x) => x.ProjectID == null && x.InventoryIsVerified));
                const inventoriedTreatmentBMPsLayer = new L.GeoJSON(inventoriedTreatmentBMPGeoJSON, {
                    pointToLayer: (feature, latlng) => {
                        return L.marker(latlng, { icon: MarkerHelper.inventoriedTreatmentBMPMarker });
                    },
                    onEachFeature: (feature, layer) => {
                        layer.bindPopup(
                            `<b>Name:</b> <a target="_blank" href="${this.ocstBaseUrl()}/TreatmentBMP/Detail/${feature.properties.TreatmentBMPID}">${
                                feature.properties.TreatmentBMPName
                            }</a><br>` + `<b>Type:</b> ${feature.properties.TreatmentBMPTypeName}`
                        );
                    },
                });
                this.layer.addLayer(inventoriedTreatmentBMPsLayer);
                this.initLayer();
            })
        );
    }

    private mapTreatmentBMPsToGeoJson(treatmentBMPs: TreatmentBMPDisplayDto[]) {
        return {
            type: "FeatureCollection",
            features: treatmentBMPs.map((x) => {
                let treatmentBMPGeoJson = {
                    type: "Feature",
                    geometry: {
                        type: "Point",
                        coordinates: [x.Longitude ?? 0, x.Latitude ?? 0],
                    },
                    properties: {
                        TreatmentBMPID: x.TreatmentBMPID,
                        TreatmentBMPName: x.TreatmentBMPName,
                        TreatmentBMPTypeName: x.TreatmentBMPTypeName,
                        Latitude: x.Latitude,
                        Longitude: x.Longitude,
                    },
                };
                return treatmentBMPGeoJson;
            }),
        };
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }
}
