import { ChangeDetectorRef, Component } from "@angular/core";
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
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "../../../../shared/components/form-field/form-field.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import {
    OnlandVisualTrashAssessmentObservationUpsertDto,
    OnlandVisualTrashAssessmentObservationUpsertDtoForm,
    OnlandVisualTrashAssessmentObservationUpsertDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-upsert-dto";
import { environment } from "src/environments/environment";
import { OnlandVisualTrashAssessmentObservationPhotoStagingDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-photo-staging-dto";

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
    public ovtaID: number;
    public ovtaObservationLayer: L.GeoJSON<any>;
    public observations: OnlandVisualTrashAssessmentObservationUpsertDto[] = [];
    public uploadFormField: FormControl<Blob> = new FormControl<Blob>(null);
    public selectedOVTAObservation: FormGroup<OnlandVisualTrashAssessmentObservationUpsertDtoForm> = new FormGroup<any>({
        OnlandVisualTrashAssessmentObservationID: OnlandVisualTrashAssessmentObservationUpsertDtoFormControls.OnlandVisualTrashAssessmentObservationID(),
        OnlandVisualTrashAssessmentID: OnlandVisualTrashAssessmentObservationUpsertDtoFormControls.OnlandVisualTrashAssessmentID(),
        Note: OnlandVisualTrashAssessmentObservationUpsertDtoFormControls.Note(),
        FileResourceID: OnlandVisualTrashAssessmentObservationUpsertDtoFormControls.FileResourceID(),
        FileResourceGUID: OnlandVisualTrashAssessmentObservationUpsertDtoFormControls.FileResourceGUID(),
        Longitude: OnlandVisualTrashAssessmentObservationUpsertDtoFormControls.Longitude(),
        Latitude: OnlandVisualTrashAssessmentObservationUpsertDtoFormControls.Latitude(),
    });

    public onlandVisualTrashAssessmentObservations$: Observable<OnlandVisualTrashAssessmentObservationUpsertDto[]>;

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private route: ActivatedRoute,
        private alertService: AlertService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService,
        private router: Router,
        private cdr: ChangeDetectorRef
    ) {}

    ngOnInit() {
        this.onlandVisualTrashAssessmentObservations$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            })
        );
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
    }

    ngOnDestroy() {
        this.cdr.detach();
    }

    public handleMapReady(event: NeptuneMapInitEvent, observations: OnlandVisualTrashAssessmentObservationUpsertDto[]): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.observations = observations;
        if (observations.length > 0) {
            this.addObservationPointsLayersToMap();
        }
    }

    public addObservationMarker() {
        this.map.on("click", (e: L.LeafletMouseEvent) => {
            this.addObservation(e.latlng);

            this.map.off("click");
        });
    }

    public addObservation(latlng) {
        var observation = new OnlandVisualTrashAssessmentObservationUpsertDto();
        observation.OnlandVisualTrashAssessmentID = this.ovtaID;
        observation.Latitude = latlng.lat;
        observation.Longitude = latlng.lng;
        this.observations = this.observations.concat(observation);
        this.addObservationPointsLayersToMap();
    }

    public addObservationPointsLayersToMap(): void {
        if (this.ovtaObservationLayer) {
            this.map.removeLayer(this.ovtaObservationLayer);
            this.layerControl.removeLayer(this.ovtaObservationLayer);
        }
        const ovtaObservationGeoJSON = this.mapObservationsToGeoJson();
        this.ovtaObservationLayer = new L.GeoJSON(ovtaObservationGeoJSON, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                layer.on("click", (e) => {
                    this.selectOVTAObservationImpl(feature.geometry.coordinates);
                });
            },
        });
        this.ovtaObservationLayer.sortOrder = 100;
        this.ovtaObservationLayer.addTo(this.map);
    }

    save(andContinue: boolean = false) {
        if (this.selectedOVTAObservation.controls.Latitude.getRawValue() != null) {
            let selectedObservationIndex = this.observations.findIndex(
                (x) => x.Latitude == this.selectedOVTAObservation.controls.Latitude.getRawValue() && x.Longitude == this.selectedOVTAObservation.controls.Longitude.getRawValue()
            );
            this.observations[selectedObservationIndex] = this.selectedOVTAObservation.getRawValue();
        }
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(this.ovtaID, this.observations).subscribe(() => {
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
            features: this.observations.map((x) => {
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

    public selectOVTAObservationImpl(latlng: number[]) {
        if (!this.map.hasLayer(this.ovtaObservationLayer)) {
            this.ovtaObservationLayer.addTo(this.map);
        }
        if (this.selectedOVTAObservation.controls.Latitude.getRawValue() != null) {
            let selectedObservationID = this.observations.findIndex(
                (x) => x.Latitude == this.selectedOVTAObservation.controls.Latitude.getRawValue() && x.Longitude == this.selectedOVTAObservation.controls.Longitude.getRawValue()
            );
            this.observations[selectedObservationID] = this.selectedOVTAObservation.getRawValue();
        }
        let newObservation = this.observations.find((x) => x.Latitude == latlng[1] && x.Longitude == latlng[0]);
        //this.uploadFormField.setValue(null);
        this.selectedOVTAObservation.controls.OnlandVisualTrashAssessmentID.setValue(newObservation.OnlandVisualTrashAssessmentID);
        this.selectedOVTAObservation.controls.Note.setValue(newObservation.Note);
        this.selectedOVTAObservation.controls.Latitude.setValue(newObservation.Latitude);
        this.selectedOVTAObservation.controls.Longitude.setValue(newObservation.Longitude);
        this.selectedOVTAObservation.controls.FileResourceID.setValue(newObservation.FileResourceID);
        this.selectedOVTAObservation.controls.FileResourceGUID.setValue(newObservation.FileResourceGUID);
        this.selectedOVTAObservation.controls.OnlandVisualTrashAssessmentObservationID.setValue(newObservation.OnlandVisualTrashAssessmentObservationID);
        this.ovtaObservationLayer.eachLayer((layer) => {
            if (layer.feature.geometry.coordinates[0] == newObservation.Longitude && layer.feature.geometry.coordinates[1] == newObservation.Latitude) {
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
            var index = this.observations.findIndex(
                (x) => x.Latitude == this.selectedOVTAObservation.controls.Latitude.getRawValue() && x.Longitude == this.selectedOVTAObservation.controls.Longitude.getRawValue()
            );
            this.observations[index].Latitude = e.latlng.lat;
            this.observations[index].Longitude = e.latlng.lng;
            this.addObservationPointsLayersToMap();
            this.map.off("click");
        });
    }

    public deleteObservation() {
        const index = this.observations.findIndex(
            (x) => x.Latitude == this.selectedOVTAObservation.controls.Latitude.getRawValue() && x.Longitude == this.selectedOVTAObservation.controls.Longitude.getRawValue()
        );
        this.observations.splice(index, 1);
        this.selectedOVTAObservation.setValue = null;
        this.addObservationPointsLayersToMap();
    }

    public goToCurrentLocation() {
        this.map.locate({ setView: true }).on("locationerror", function (e) {
            console.log(e);
            alert("Location access has been denied.");
        });
    }

    public getFile() {
        if (typeof this.uploadFormField.getRawValue() != typeof "string") {
            this.onlandVisualTrashAssessmentService
                .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationPhotoStagingPost(this.ovtaID, this.uploadFormField.getRawValue())
                .subscribe((response) => {
                    this.selectedOVTAObservation.controls.FileResourceID.setValue(response.FileResourceID);
                    this.selectedOVTAObservation.controls.FileResourceGUID.setValue(response.FileResourceGUID);
                    this.selectedOVTAObservation.controls.PhotoStagingID.setValue(response.PhotoStagingID);
                });
        }
    }

    public getUrl(fileResourceGUID) {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
    }

    public deletePhotoFromSelectedObservation() {
        var onlandVisualTrashAssessmentObservationPhotoStagingDto = new OnlandVisualTrashAssessmentObservationPhotoStagingDto();
        onlandVisualTrashAssessmentObservationPhotoStagingDto.FileResourceGUID = this.selectedOVTAObservation.controls.FileResourceGUID.getRawValue();
        onlandVisualTrashAssessmentObservationPhotoStagingDto.FileResourceID = this.selectedOVTAObservation.controls.FileResourceID.getRawValue();
        onlandVisualTrashAssessmentObservationPhotoStagingDto.OnlandVisualTrashAssessmentObservationID =
            this.selectedOVTAObservation.controls.OnlandVisualTrashAssessmentObservationID.getRawValue();
        onlandVisualTrashAssessmentObservationPhotoStagingDto.FileResourceGUID = this.selectedOVTAObservation.controls.FileResourceGUID.getRawValue();
        this.onlandVisualTrashAssessmentService
            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationPhotoDelete(this.ovtaID, onlandVisualTrashAssessmentObservationPhotoStagingDto)
            .subscribe((x) => {
                this.selectedOVTAObservation.controls.FileResourceID.setValue(null);
                this.selectedOVTAObservation.controls.FileResourceGUID.setValue(null);
                this.selectedOVTAObservation.controls.PhotoStagingID.setValue(null);
            });
    }
}
