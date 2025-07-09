import { Component, Input, OnChanges } from "@angular/core";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";
import { Observable, tap } from "rxjs";
import { OnlandVisualTrashAssessmentObservationLocationDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-location-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { AsyncPipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentObservationService } from "src/app/shared/generated/api/onland-visual-trash-assessment-observation.service";

@Component({
    selector: "ovta-observation-layer",
    imports: [AsyncPipe, NgIf],
    templateUrl: "./ovta-observation-layer.component.html",
    styleUrl: "./ovta-observation-layer.component.scss"
})
export class OvtaObservationLayerComponent extends MapLayerBase implements OnChanges {
    @Input() ovtaID: number;

    public onlandVisualTrashAssessmentObservations$: Observable<OnlandVisualTrashAssessmentObservationLocationDto[]>;
    constructor(private onlandVisualTrashAssessmentObservationService: OnlandVisualTrashAssessmentObservationService) {
        super();
    }
    public wmsOptions: L.WMSOptions;
    public layer;

    ngOnInit() {
        this.onlandVisualTrashAssessmentObservations$ = this.onlandVisualTrashAssessmentObservationService
            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsFeatureCollectionGet(this.ovtaID)
            .pipe(
                tap((locations) => {
                    const ovtaObservationGeoJSON = this.mapObservationsToGeoJson(locations);
                    this.layer = new L.GeoJSON(ovtaObservationGeoJSON, {
                        pointToLayer: (feature, latlng) => {
                            return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
                        },
                    });
                    this.layer.sortOrder = 100;
                    this.initLayer();
                    this.map.fitBounds(this.layer.getBounds());
                })
            );
    }

    private mapObservationsToGeoJson(locations) {
        return {
            type: "FeatureCollection",
            features: locations.map((x) => {
                let observationGeoJson = {
                    type: "Feature",
                    geometry: {
                        type: "Point",
                        coordinates: [x.Longitude ?? 0, x.Latitude ?? 0],
                    },
                    properties: {
                        OnlandVisualTrashAssessmentObservationID: x.OnlandVisualTrashAssessmentObservationID,
                        Latitude: x.Latitude,
                        Longitude: x.Longitude,
                    },
                };
                return observationGeoJson;
            }),
        };
    }
}
