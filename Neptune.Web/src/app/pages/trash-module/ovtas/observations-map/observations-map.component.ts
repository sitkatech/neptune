import { ApplicationRef, Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import * as L from "leaflet";
import "leaflet.fullscreen";
import { forkJoin } from "rxjs";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { FieldDefinitionTypeEnum } from "src/app/shared/generated/enum/field-definition-type-enum";
import { NgIf, NgFor, NgClass } from "@angular/common";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { OvtaAreaLayerComponent } from "src/app/shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { TransectLineLayerComponent } from "src/app/shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";
import { OnlandVisualTrashAssessmentObservationWithPhotoDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";
import { environment } from "src/environments/environment";
import { ActivatedRoute, Router } from "@angular/router";

declare var $: any;

//This component could use a fair amount of cleanup. It should likely be sent in the treatment bmps and delineations instead of grabbing them itself
@Component({
    selector: "observations-map",
    templateUrl: "./observations-map.component.html",
    styleUrls: ["./observations-map.component.scss"],
    standalone: true,
    imports: [NgIf, NgFor, NgClass, NeptuneMapComponent, OvtaAreaLayerComponent, TransectLineLayerComponent],
})
export class ObservationsMapComponent {
    @Input("observations") onlandVisualTrashAssessmentObservations: Array<OnlandVisualTrashAssessmentObservationWithPhotoDto>;
    @Input() jurisdictionID: number;
    @Input() onlandVisualTrashAssessmentAreaID: number;
    @Input() onlandVisualTrashAssessmentAreaName: string;
    @Input() boundingBox: BoundingBoxDto;

    public mapIsReady: boolean = false;
    public visibleOnlandVisualTrashAssessmentObservationStyle: string = "onlandVisualTrashAssessmentObservation_purple_outline_only";
    public selectedOnlandVisualTrashAssessmentObservationStyle: string = "onlandVisualTrashAssessmentObservation_yellow";
    public mapHeight: string = "750px";

    @Output()
    public afterSetControl: EventEmitter<L.Control.Layers> = new EventEmitter();

    @Output()
    public afterLoadMap: EventEmitter<L.LeafletEvent> = new EventEmitter();

    @Output()
    public onMapMoveEnd: EventEmitter<L.LeafletEvent> = new EventEmitter();

    public map: L.Map;
    public layerControl: L.Control.Layers;
    public selectedListItem: number;
    public selectedListItemDetails: { [key: string]: any } = {};
    public selectedObjectMarker: L.Layer;
    public selectedOnlandVisualTrashAssessmentObservation: OnlandVisualTrashAssessmentObservationWithPhotoDto;
    public onlandVisualTrashAssessmentObservationsLayer: L.GeoJSON<any>;
    private ovtaObservationOverlayName = "<img src='./assets/main/map-icons/marker-icon-violet.png' style='height:17px'> Observations";

    public ovtaObservationLayer: L.GeoJSON<any>;

    constructor(private router: Router, private route: ActivatedRoute) {}

    public ngOnInit(): void {
        console.log(this.onlandVisualTrashAssessmentObservations);
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.addObservationPointsLayersToMap();
    }

    public addObservationPointsLayersToMap(): void {
        if (this.ovtaObservationLayer) {
            this.map.removeLayer(this.ovtaObservationLayer);
            this.layerControl.removeLayer(this.ovtaObservationLayer);
        }
        const ovtaObservationGeoJSON = this.mapObservationsToGeoJson(this.onlandVisualTrashAssessmentObservations);
        this.ovtaObservationLayer = new L.GeoJSON(ovtaObservationGeoJSON, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                layer.on("click", (e) => {
                    this.selectOnlandVisualTrashAssessmentObservation(feature.properties.OnlandVisualTrashAssessmentObservationID);
                });
            },
        });
        this.ovtaObservationLayer.sortOrder = 100;
        this.ovtaObservationLayer.addTo(this.map);
        this.layerControl.addOverlay(this.ovtaObservationLayer, this.ovtaObservationOverlayName);
    }

    private mapObservationsToGeoJson(observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[]) {
        return {
            type: "FeatureCollection",
            features: observations.map((x) => {
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

    public selectOnlandVisualTrashAssessmentObservation(onlandVisualTrashAssessmentObservationID: number) {
        if (!this.map.hasLayer(this.ovtaObservationLayer)) {
            this.ovtaObservationLayer.addTo(this.map);
        }

        this.selectedOnlandVisualTrashAssessmentObservation = this.onlandVisualTrashAssessmentObservations.find(
            (x) => x.OnlandVisualTrashAssessmentObservationID == onlandVisualTrashAssessmentObservationID
        );
        this.ovtaObservationLayer.eachLayer((layer) => {
            if (layer.feature.properties.OnlandVisualTrashAssessmentObservationID == this.selectedOnlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationID) {
                if (!layer.feature.properties.DefaultZIndexOffset) {
                    layer.feature.properties.DefaultZIndexOffset = layer._zIndex;
                }
                layer.setZIndexOffset(10000);
                layer.setIcon(MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-red.png"));
                this.router.navigate([], {
                    relativeTo: this.route,
                    fragment: `${layer.feature.properties.OnlandVisualTrashAssessmentObservationID}`,
                    queryParamsHandling: "preserve",
                    replaceUrl: true,
                });
            } else {
                layer.setIcon(MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-violet.png"));
            }
        });
    }

    private clearSelectedItem() {
        if (this.selectedListItem) {
            this.selectedListItem = null;
            this.selectedListItemDetails = {};
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = null;
        }
    }

    public getFileResourceUrl(fileResourceGUID) {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
    }
}
