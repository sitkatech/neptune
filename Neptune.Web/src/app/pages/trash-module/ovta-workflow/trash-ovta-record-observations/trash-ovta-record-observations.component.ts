import { Component } from "@angular/core";
import * as L from "leaflet";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import { AsyncPipe, NgFor, NgIf } from "@angular/common";
import { Observable, switchMap, tap } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-detail-dto";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "../../../../shared/components/form-field/form-field.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import {
    OnlandVisualTrashAssessmentObservationWithPhotoDto,
    OnlandVisualTrashAssessmentObservationWithPhotoDtoForm,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";
import { environment } from "src/environments/environment";
import { OnlandVisualTrashAssessmentObservationService } from "src/app/shared/generated/api/onland-visual-trash-assessment-observation.service";
import { WorkflowBodyComponent } from "src/app/shared/components/workflow-body/workflow-body.component";
import { WfsService } from "src/app/shared/services/wfs.service";
import { LandUseBlockLayerComponent } from "../../../../shared/components/leaflet/layers/land-use-block-layer/land-use-block-layer.component";
import { ParcelLayerComponent } from "../../../../shared/components/leaflet/layers/parcel-layer/parcel-layer.component";
import { OvtaAreaLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { TransectLineLayerComponent } from "src/app/shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";

@Component({
    selector: "trash-ovta-record-observations",
    standalone: true,
    imports: [
        PageHeaderComponent,
        NeptuneMapComponent,
        DropdownToggleDirective,
        NgIf,
        NgFor,
        AsyncPipe,
        FormFieldComponent,
        ReactiveFormsModule,
        FormsModule,
        AlertDisplayComponent,
        WorkflowBodyComponent,
        LandUseBlockLayerComponent,
        ParcelLayerComponent,
        TransectLineLayerComponent,
        OvtaAreaLayerComponent,
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
    public ovtaID: number;
    public ovtaObservationLayer: L.GeoJSON<any>;
    public uploadFormField: FormControl<Blob> = new FormControl<Blob>(null);
    public formGroup: FormGroup<OnlandVisualTrashAssessmentObservationsUpsertDtoCustomForm> = new FormGroup<OnlandVisualTrashAssessmentObservationsUpsertDtoCustomForm>({
        Observations: new FormArray<FormGroup<OnlandVisualTrashAssessmentObservationWithPhotoDtoForm>>([]),
    });

    public selectedOnlandVisualTrashAssessmentObservationID: number;
    public newObservationIDIndex: number = -1;

    public onlandVisualTrashAssessmentObservations$: Observable<OnlandVisualTrashAssessmentObservationWithPhotoDto[]>;

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentObservationService: OnlandVisualTrashAssessmentObservationService,
        private route: ActivatedRoute,
        private alertService: AlertService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService,
        private router: Router,
        private wfsService: WfsService,
        private formBuilder: FormBuilder
    ) {}

    ngOnInit() {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
        this.onlandVisualTrashAssessmentObservations$ = this.onlandVisualTrashAssessment$.pipe(
            switchMap((onlandVisualTrashAssessment) => {
                return this.onlandVisualTrashAssessmentObservationService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID
                );
            }),
            tap((onlandVisualTrashAssessmentObservations) => {
                const formArray = this.formGroup.controls.Observations as FormArray;
                onlandVisualTrashAssessmentObservations.forEach((onlandVisualTrashAssessmentObservation) => {
                    let observation = this.formBuilder.group<OnlandVisualTrashAssessmentObservationWithPhotoDto>({
                        OnlandVisualTrashAssessmentObservationID: onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationID,
                        OnlandVisualTrashAssessmentID: onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentID,
                        Note: onlandVisualTrashAssessmentObservation.Note,
                        Latitude: onlandVisualTrashAssessmentObservation.Latitude,
                        Longitude: onlandVisualTrashAssessmentObservation.Longitude,
                        FileResourceID: onlandVisualTrashAssessmentObservation.FileResourceID,
                        FileResourceGUID: onlandVisualTrashAssessmentObservation.FileResourceGUID,
                    });

                    formArray.push(observation);
                });
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent, onlandVisualTrashAssessment: OnlandVisualTrashAssessmentDetailDto): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        if (this.formGroup.controls.Observations.length > 0) {
            this.addObservationPointsLayersToMap();
            this.map.fitBounds(this.ovtaObservationLayer.getBounds());
        } else if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID !== null) {
            this.wfsService
                .getGeoserverWFSLayerWithCQLFilter(
                    "OCStormwater:OnlandVisualTrashAssessmentAreas",
                    `OnlandVisualTrashAssessmentAreaID = ${onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID}`,
                    "OnlandVisualTrashAssessmentAreaID"
                )
                .subscribe((response) => {
                    this.map.fitBounds(L.geoJson(response).getBounds());
                });
        } else {
            this.wfsService
                .getGeoserverWFSLayerWithCQLFilter(
                    "OCStormwater:Jurisdictions",
                    `StormwaterJurisdictionID = ${onlandVisualTrashAssessment.StormwaterJurisdictionID}`,
                    "StormwaterJurisdictionID"
                )
                .subscribe((response) => {
                    this.map.fitBounds(L.geoJson(response).getBounds());
                });
        }
    }

    public addObservationMarker() {
        this.map.on("click", (e: L.LeafletMouseEvent) => {
            this.addObservation(e.latlng);
            this.map.off("click");
        });
    }

    public addObservation(latlng: L.latlng) {
        this.uploadFormField.reset();
        let observation = this.formBuilder.group<OnlandVisualTrashAssessmentObservationWithPhotoDto>({
            OnlandVisualTrashAssessmentObservationID: this.newObservationIDIndex,
            OnlandVisualTrashAssessmentID: this.ovtaID,
            Note: null,
            Latitude: latlng.lat,
            Longitude: latlng.lng,
            FileResourceID: null,
            FileResourceGUID: null,
        });
        const formArray = this.formGroup.controls.Observations as FormArray;
        formArray.push(observation);
        this.selectedOnlandVisualTrashAssessmentObservationID = observation.controls.OnlandVisualTrashAssessmentObservationID.value;
        this.addObservationPointsLayersToMap();
        this.newObservationIDIndex--;
    }

    public addObservationPointsLayersToMap(): void {
        if (this.ovtaObservationLayer) {
            this.map.removeLayer(this.ovtaObservationLayer);
            this.layerControl.removeLayer(this.ovtaObservationLayer);
        }
        const ovtaObservationGeoJSON = this.mapObservationsToGeoJson();
        this.ovtaObservationLayer = new L.GeoJSON(ovtaObservationGeoJSON, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, {
                    icon:
                        feature.properties.OnlandVisualTrashAssessmentObservationID === this.selectedOnlandVisualTrashAssessmentObservationID
                            ? MarkerHelper.selectedMarker
                            : MarkerHelper.treatmentBMPMarker,
                });
            },
            onEachFeature: (feature, layer) => {
                layer.on("click", (e) => {
                    this.selectedOnlandVisualTrashAssessmentObservationID = feature.properties.OnlandVisualTrashAssessmentObservationID;
                    this.selectOnlandVisualTrashAssessmentObservation();
                });
            },
        });
        this.ovtaObservationLayer.sortOrder = 100;
        this.ovtaObservationLayer.addTo(this.map);
    }

    save(andContinue: boolean = false) {
        this.onlandVisualTrashAssessmentObservationService
            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(this.ovtaID, this.formGroup.value)
            .subscribe(() => {
                this.isLoadingSubmit = false;
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Your observations were successfully updated.", AlertContext.Success));
                this.ovtaWorkflowProgressService.updateProgress(this.ovtaID);

                if (andContinue) {
                    this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet(this.ovtaID).subscribe((response) => {
                        if (response.Steps.RefineAssessmentArea.Disabled) {
                            this.router.navigate([`../../${this.ovtaID}/review-and-finalize`], { relativeTo: this.route });
                        } else {
                            this.router.navigate([`../../${this.ovtaID}/add-or-remove-parcels`], { relativeTo: this.route });
                        }
                    });
                }
            });
    }

    private mapObservationsToGeoJson() {
        return {
            type: "FeatureCollection",
            features: this.formGroup.controls.Observations.value.map((x) => {
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

    public selectOnlandVisualTrashAssessmentObservation() {
        this.uploadFormField.reset();
        this.ovtaObservationLayer.eachLayer((layer) => {
            if (layer.feature.properties.OnlandVisualTrashAssessmentObservationID === this.selectedOnlandVisualTrashAssessmentObservationID) {
                layer.setIcon(MarkerHelper.selectedMarker);
            } else {
                layer.setIcon(MarkerHelper.treatmentBMPMarker);
            }
        });
    }

    public editObservationLocation(index: number) {
        this.map.on("click", (e: L.LeafletMouseEvent) => {
            const observation = this.formGroup.controls.Observations.controls[index].value;
            observation.Latitude = e.latlng.lat;
            observation.Longitude = e.latlng.lng;
            this.formGroup.controls.Observations.controls[index].patchValue(observation);
            this.addObservationPointsLayersToMap();
            this.map.off("click");
        });
    }

    public deleteObservation(index: number) {
        this.formGroup.controls.Observations.removeAt(index);
        this.selectedOnlandVisualTrashAssessmentObservationID = null;
        this.addObservationPointsLayersToMap();
    }

    public goToCurrentLocation() {
        this.map.locate({ setView: true }).on("locationerror", function (e) {
            alert("Location access has been denied.");
        });
    }

    public getFile(index: number) {
        if (typeof this.uploadFormField.value != typeof "string") {
            this.isLoadingSubmit = true;
            this.onlandVisualTrashAssessmentObservationService
                .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsObservationPhotoPost(this.ovtaID, this.uploadFormField.value)
                .subscribe((response) => {
                    const observation = this.formGroup.controls.Observations.controls[index].value;
                    observation.FileResourceID = response.FileResourceID;
                    observation.FileResourceGUID = response.FileResourceGUID;
                    this.formGroup.controls.Observations.controls[index].patchValue(observation);
                    this.isLoadingSubmit = false;
                });
        }
    }

    public getUrl(fileResourceGUID) {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
    }

    public deletePhotoFromSelectedObservation(index: number) {
        const observation = this.formGroup.controls.Observations.controls[index].value;
        if (observation.FileResourceID) {
            this.onlandVisualTrashAssessmentObservationService
                .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsObservationPhotosFileResourceIDDelete(this.ovtaID, observation.FileResourceID)
                .subscribe((x) => {
                    observation.FileResourceID = null;
                    observation.FileResourceGUID = null;
                    this.formGroup.controls.Observations.controls[index].patchValue(observation);
                });
        }
    }
}

export class OnlandVisualTrashAssessmentObservationsUpsertDtoCustomForm {
    Observations: FormArray<FormGroup<OnlandVisualTrashAssessmentObservationWithPhotoDtoForm>>;
}
