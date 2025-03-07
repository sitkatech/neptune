import { Component } from "@angular/core";
import { FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FormFieldComponent, FormFieldType, FormInputOption } from "../../../../shared/components/form-field/form-field.component";
import { OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions } from "src/app/shared/generated/enum/onland-visual-trash-assessment-score-enum";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable, switchMap, tap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { AsyncPipe, NgClass, NgFor, NgIf } from "@angular/common";
import * as L from "leaflet";
import {
    OnlandVisualTrashAssessmentWorkflowDto,
    OnlandVisualTrashAssessmentWorkflowDtoForm,
    OnlandVisualTrashAssessmentWorkflowDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-workflow-dto";
import { NeptuneMapInitEvent, NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { OnlandVisualTrashAssessmentObservationWithPhotoDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { environment } from "src/environments/environment";
import { OvtaAreaLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { TransectLineLayerComponent } from "../../../../shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";

@Component({
    selector: "trash-ovta-review-and-finalize",
    standalone: true,
    imports: [
        PageHeaderComponent,
        FormFieldComponent,
        ReactiveFormsModule,
        NgIf,
        AsyncPipe,
        FormsModule,
        NgFor,
        NgClass,
        NeptuneMapComponent,
        OvtaAreaLayerComponent,
        TransectLineLayerComponent,
    ],
    templateUrl: "./trash-ovta-review-and-finalize.component.html",
    styleUrl: "./trash-ovta-review-and-finalize.component.scss",
})
export class TrashOvtaReviewAndFinalizeComponent {
    public isLoadingSubmit = false;
    public FormFieldType = FormFieldType;
    public ovtaObservationLayer: L.GeoJSON<any>;
    public selectedOVTAObservation: OnlandVisualTrashAssessmentObservationWithPhotoDto;

    public ovtaID: number;

    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentWorkflowDto>;

    public onlandVisualTrashAssessmentScoreDropdown = OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions;

    public ProgressScoreOptions: FormInputOption[] = [
        { Value: false, Label: "Baseline", Disabled: false },
        { Value: true, Label: "Progress", Disabled: false },
    ];

    public formGroup: FormGroup<OnlandVisualTrashAssessmentWorkflowDtoForm> = new FormGroup<any>({
        OnlandVisualTrashAssessmentID: OnlandVisualTrashAssessmentWorkflowDtoFormControls.OnlandVisualTrashAssessmentID(),
        OnlandVisualTrashAssessmentAreaID: OnlandVisualTrashAssessmentWorkflowDtoFormControls.OnlandVisualTrashAssessmentAreaID(-1),
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentWorkflowDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        StormwaterJurisdictionID: OnlandVisualTrashAssessmentWorkflowDtoFormControls.StormwaterJurisdictionID(),
        StormwaterJurisdictionName: OnlandVisualTrashAssessmentWorkflowDtoFormControls.StormwaterJurisdictionName(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentWorkflowDtoFormControls.AssessmentAreaDescription(),
        LastAssessmentDate: OnlandVisualTrashAssessmentWorkflowDtoFormControls.LastAssessmentDate(),
        OnlandVisualTrashAssessmentBaselineScoreID: OnlandVisualTrashAssessmentWorkflowDtoFormControls.OnlandVisualTrashAssessmentBaselineScoreID(),
        IsProgressAssessment: OnlandVisualTrashAssessmentWorkflowDtoFormControls.IsProgressAssessment(),
        Notes: OnlandVisualTrashAssessmentWorkflowDtoFormControls.Notes(),
        BoundingBox: OnlandVisualTrashAssessmentWorkflowDtoFormControls.BoundingBox(),
        Geometry: OnlandVisualTrashAssessmentWorkflowDtoFormControls.Geometry(),
    });

    constructor(private route: ActivatedRoute, private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private router: Router) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDWorkflowGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            }),
            tap((ovta) => {
                this.formGroup.controls.OnlandVisualTrashAssessmentAreaName.setValue(ovta.OnlandVisualTrashAssessmentAreaName);
                this.formGroup.controls.AssessmentAreaDescription.setValue(ovta.AssessmentAreaDescription);
                this.formGroup.controls.LastAssessmentDate.setValue(ovta.LastAssessmentDate);
                this.formGroup.controls.OnlandVisualTrashAssessmentBaselineScoreID.setValue(ovta.OnlandVisualTrashAssessmentBaselineScoreID);
                this.formGroup.controls.IsProgressAssessment.setValue(ovta.IsProgressAssessment);
                this.formGroup.controls.Notes.setValue(ovta.Notes);
            })
        );
    }

    getUrl(fileResourceGUID) {
        if (fileResourceGUID) {
            return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
        } else {
            return null;
        }
    }

    save(andContinue: boolean = false) {}

    public handleMapReady(event: NeptuneMapInitEvent, observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[]): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.addObservationPointsLayersToMap(observations);
    }

    public addObservationPointsLayersToMap(observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[]): void {
        if (this.ovtaObservationLayer) {
            this.map.removeLayer(this.ovtaObservationLayer);
            this.layerControl.removeLayer(this.ovtaObservationLayer);
        }
        const ovtaObservationGeoJSON = this.mapObservationsToGeoJson(observations);
        this.ovtaObservationLayer = new L.GeoJSON(ovtaObservationGeoJSON, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                layer.on("click", (e) => {
                    this.selectOVTAObservationImpl(observations, feature.properties.OnlandVisualTrashAssessmentObservationID);
                });
            },
        });
        this.ovtaObservationLayer.sortOrder = 100;
        this.ovtaObservationLayer.addTo(this.map);
        //this.layerControl.addOverlay(this.ovtaObservationLayer, this.ovtaObservationOverlayName);
    }

    public selectOVTAObservationImpl(observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[], observationID: number) {
        if (!this.map.hasLayer(this.ovtaObservationLayer)) {
            this.ovtaObservationLayer.addTo(this.map);
        }

        let selectedObservation = observations.find((x) => x.OnlandVisualTrashAssessmentObservationID == observationID);
        this.selectedOVTAObservation = selectedObservation;
        this.ovtaObservationLayer.eachLayer((layer) => {
            if (layer.feature.properties.OnlandVisualTrashAssessmentObservationID == this.selectedOVTAObservation.OnlandVisualTrashAssessmentObservationID) {
                if (!layer.feature.properties.DefaultZIndexOffset) {
                    layer.feature.properties.DefaultZIndexOffset = layer._zIndex;
                }
                layer.setZIndexOffset(10000);
                layer.setIcon(MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-red.png"));
                // this.router.navigate([`/trash/onland-visual-trash-assessment/${this.ovtaID}`], {
                //     fragment: `${layer.feature.properties.OnlandVisualTrashAssessmentObservationID}`,
                // });
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
}
