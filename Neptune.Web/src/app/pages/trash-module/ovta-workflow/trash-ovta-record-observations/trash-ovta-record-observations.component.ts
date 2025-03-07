import { Component } from "@angular/core";
import * as L from "leaflet";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import { OvtaAreaLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { Observable, switchMap } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-detail-dto";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import {
    OnlandVisualTrashAssessmentObservationWithPhotoDto,
    OnlandVisualTrashAssessmentObservationWithPhotoDtoForm,
    OnlandVisualTrashAssessmentObservationWithPhotoDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "../../../../shared/components/form-field/form-field.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";

@Component({
    selector: "trash-ovta-record-observations",
    standalone: true,
    imports: [
        PageHeaderComponent,
        NeptuneMapComponent,
        DropdownToggleDirective,
        OvtaAreaLayerComponent,
        NgIf,
        AsyncPipe,
        FormFieldComponent,
        ReactiveFormsModule,
        FormsModule,
        AlertDisplayComponent,
    ],
    templateUrl: "./trash-ovta-record-observations.component.html",
    styleUrl: "./trash-ovta-record-observations.component.scss",
})
export class TrashOvtaRecordObservationsComponent {
    public FormFieldType = FormFieldType;
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public isLoadingSubmit = false;
    public ovtaAreaID: number;
    public ovtaObservationLayer: L.GeoJSON<any>;
    public observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[] = [];
    //public selectedOVTAObservation: OnlandVisualTrashAssessmentObservationWithPhotoDto = null;
    public selectedOVTAObservation: FormGroup<OnlandVisualTrashAssessmentObservationWithPhotoDtoForm> = new FormGroup<any>({
        OnlandVisualTrashAssessmentObservationID: OnlandVisualTrashAssessmentObservationWithPhotoDtoFormControls.OnlandVisualTrashAssessmentObservationID(),
        OnlandVisualTrashAssessmentID: OnlandVisualTrashAssessmentObservationWithPhotoDtoFormControls.OnlandVisualTrashAssessmentID(),
        Note: OnlandVisualTrashAssessmentObservationWithPhotoDtoFormControls.Note(),
        FileResourceGUID: OnlandVisualTrashAssessmentObservationWithPhotoDtoFormControls.FileResourceGUID(),
        Longitude: OnlandVisualTrashAssessmentObservationWithPhotoDtoFormControls.Longitude(),
        Latitude: OnlandVisualTrashAssessmentObservationWithPhotoDtoFormControls.Latitude(),
    });

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private route: ActivatedRoute,
        private alertService: AlertService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService,
        private router: Router
    ) {}

    ngOnInit() {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaAreaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent, observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[]): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.observations = observations;
        this.addObservationPointsLayersToMap(this.observations);
    }

    public addObservationMarker() {
        this.map.on("click", (e: L.LeafletMouseEvent) => {
            this.addObservation(e.latlng);
            this.addObservationPointsLayersToMap(this.observations);
            this.map.off("click");
        });
    }

    public addObservation(latlng) {
        var observation = new OnlandVisualTrashAssessmentObservationWithPhotoDto();
        observation.OnlandVisualTrashAssessmentID = this.ovtaAreaID;
        observation.Latitude = latlng.lat;
        observation.Longitude = latlng.lng;
        this.observations = this.observations.concat(observation);
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
    }

    save(andContinue: boolean = false) {
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(this.ovtaAreaID, this.observations).subscribe(() => {
            this.isLoadingSubmit = false;
            this.alertService.clearAlerts();
            this.alertService.pushAlert(new Alert("Your observations were successfully updated.", AlertContext.Success));
            this.ovtaWorkflowProgressService.updateProgress(this.ovtaAreaID);
            if (andContinue) {
                this.router.navigate([`../../${this.ovtaAreaID}/review-and-finalize`], { relativeTo: this.route });
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

    public selectOVTAObservationImpl(observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[], observationID: number) {
        if (!this.map.hasLayer(this.ovtaObservationLayer)) {
            this.ovtaObservationLayer.addTo(this.map);
        }
        let selectedObservation = observations.find((x) => x.OnlandVisualTrashAssessmentObservationID == observationID);
        this.selectedOVTAObservation.controls.OnlandVisualTrashAssessmentObservationID.setValue(selectedObservation.OnlandVisualTrashAssessmentID);
        this.selectedOVTAObservation.controls.OnlandVisualTrashAssessmentID.setValue(selectedObservation.OnlandVisualTrashAssessmentID);
        // this.selectedOVTAObservation.controls.ObservationDatetime.setValue(selectedObservation.ObservationDatetime);
        this.selectedOVTAObservation.controls.Note.setValue(selectedObservation.Note);
        this.selectedOVTAObservation.controls.Latitude.setValue(selectedObservation.Latitude);
        this.selectedOVTAObservation.controls.Longitude.setValue(selectedObservation.Longitude);
        this.ovtaObservationLayer.eachLayer((layer) => {
            if (layer.feature.properties.OnlandVisualTrashAssessmentObservationID == selectedObservation.OnlandVisualTrashAssessmentObservationID) {
                if (!layer.feature.properties.DefaultZIndexOffset) {
                    layer.feature.properties.DefaultZIndexOffset = layer._zIndex;
                }
                layer.setZIndexOffset(10000);
                layer.setIcon(MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-red.png"));
            } else {
                layer.setIcon(MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-violet.png"));
            }
        });
    }

    public editObservationLocation() {
        this.map.on("click", (e: L.LeafletMouseEvent) => {
            var index = this.observations.findIndex((x) => x === this.selectedOVTAObservation);
            this.observations[index].Latitude = e.latlng.lat;
            this.observations[index].Longitude = e.latlng.lng;
            this.addObservationPointsLayersToMap(this.observations);
            this.map.off("click");
        });
    }

    public deleteObservation() {
        this.observations = this.observations.filter((x) => x !== this.selectedOVTAObservation);
        this.selectedOVTAObservation = null;
        this.addObservationPointsLayersToMap(this.observations);
    }
}
