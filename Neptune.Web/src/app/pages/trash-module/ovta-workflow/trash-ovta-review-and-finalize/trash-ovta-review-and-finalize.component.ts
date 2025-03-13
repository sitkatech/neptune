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
import { NeptuneMapInitEvent, NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { OnlandVisualTrashAssessmentObservationWithPhotoDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { environment } from "src/environments/environment";
import { PreliminarySourceIdentificationTypeSimpleDto } from "src/app/shared/generated/model/preliminary-source-identification-type-simple-dto";
import { PreliminarySourceIdentificationCategories } from "src/app/shared/generated/enum/preliminary-source-identification-category-enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import {
    OnlandVisualTrashAssessmentReviewAndFinalizeDto,
    OnlandVisualTrashAssessmentReviewAndFinalizeDtoForm,
    OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-review-and-finalize-dto";
import { OnlandVisualTrashAssessmentStatusEnum } from "src/app/shared/generated/enum/onland-visual-trash-assessment-status-enum";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";

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
        NgFor,
        AlertDisplayComponent,
        LoadingDirective,
    ],
    templateUrl: "./trash-ovta-review-and-finalize.component.html",
    styleUrl: "./trash-ovta-review-and-finalize.component.scss",
})
export class TrashOvtaReviewAndFinalizeComponent {
    public isLoadingSubmit = false;
    public FormFieldType = FormFieldType;
    public PreliminarySourceIdentificationCategories = PreliminarySourceIdentificationCategories;
    public ovtaObservationLayer: L.GeoJSON<any>;
    public selectedOVTAObservation: OnlandVisualTrashAssessmentObservationWithPhotoDto;
    public isLoadingMap: boolean = false;

    public ovtaID: number;

    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;
    public transectLineLayer: L.FeatureGroup = new L.FeatureGroup();

    private defaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    private transectLineStyle = {
        color: "#f70a0a",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentReviewAndFinalizeDto>;
    public preliminarySourceIdentificationTypeSimpleDto$: Observable<PreliminarySourceIdentificationTypeSimpleDto[]>;

    public onlandVisualTrashAssessmentScoreDropdown = OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions;

    public ProgressScoreOptions: FormInputOption[] = [
        { Value: false, Label: "Baseline", Disabled: false },
        { Value: true, Label: "Progress", Disabled: false },
    ];

    public formGroup: FormGroup<OnlandVisualTrashAssessmentReviewAndFinalizeDtoForm> = new FormGroup<any>({
        OnlandVisualTrashAssessmentID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentID(),
        OnlandVisualTrashAssessmentAreaID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentAreaID(),
        OnlandVisualTrashAssessmentAreaName: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentAreaName(),
        StormwaterJurisdictionID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.StormwaterJurisdictionID(),
        AssessmentAreaDescription: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.AssessmentAreaDescription(),
        AssessmentDate: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.AssessmentDate(),
        OnlandVisualTrashAssessmentBaselineScoreID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentBaselineScoreID(),
        IsProgressAssessment: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.IsProgressAssessment(),
        Notes: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.Notes(),
        BoundingBox: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.BoundingBox(),
        PreliminarySourceIdentificationTypeIDs: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.PreliminarySourceIdentificationTypeIDs(),
        OnlandVisualTrashAssessmentStatusID: OnlandVisualTrashAssessmentReviewAndFinalizeDtoFormControls.OnlandVisualTrashAssessmentStatusID(),
    });

    constructor(
        private route: ActivatedRoute,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private router: Router,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService,
        private alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.isLoadingMap = true;
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDReviewAndFinalizeGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            }),
            tap((ovta) => {
                this.ovtaID = ovta.OnlandVisualTrashAssessmentID;
                this.formGroup.controls.OnlandVisualTrashAssessmentID.setValue(ovta.OnlandVisualTrashAssessmentID);
                this.formGroup.controls.OnlandVisualTrashAssessmentAreaID.setValue(ovta.OnlandVisualTrashAssessmentAreaID);
                this.formGroup.controls.OnlandVisualTrashAssessmentAreaName.setValue(ovta.OnlandVisualTrashAssessmentAreaName);
                this.formGroup.controls.AssessmentAreaDescription.setValue(ovta.AssessmentAreaDescription);
                this.formGroup.controls.AssessmentDate.setValue(new Date(ovta.AssessmentDate).toISOString().split("T")[0]);
                this.formGroup.controls.OnlandVisualTrashAssessmentBaselineScoreID.setValue(ovta.OnlandVisualTrashAssessmentBaselineScoreID);
                this.formGroup.controls.IsProgressAssessment.setValue(ovta.IsProgressAssessment);
                this.formGroup.controls.Notes.setValue(ovta.Notes);
                this.formGroup.controls.PreliminarySourceIdentificationTypeIDs.setValue(ovta.PreliminarySourceIdentificationTypeIDs);
            })
        );
        this.preliminarySourceIdentificationTypeSimpleDto$ = this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsPreliminarySourceIdentificationTypesGet();
    }
    public handleMapReady(event: NeptuneMapInitEvent, observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[], geometry, transectLine): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.addFeatureCollectionToFeatureGroup(JSON.parse(geometry), this.transectLineLayer, this.defaultStyle);
        this.addFeatureCollectionToFeatureGroup(JSON.parse(transectLine), this.transectLineLayer, this.transectLineStyle);
        this.transectLineLayer.addTo(this.map);
        this.addObservationPointsLayersToMap(observations);
        this.isLoadingMap = false;
    }

    filterByCategory(preliminarySourceIdentificationTypeSimpleDto, categoryID) {
        return preliminarySourceIdentificationTypeSimpleDto.filter((x) => x.PreliminarySourceIdentificationCategoryID == categoryID);
    }

    getUrl(fileResourceGUID) {
        if (fileResourceGUID) {
            return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
        } else {
            return null;
        }
    }

    save(andContinue: boolean = false) {
        this.formGroup.controls.OnlandVisualTrashAssessmentStatusID.setValue(
            andContinue ? OnlandVisualTrashAssessmentStatusEnum.Complete : OnlandVisualTrashAssessmentStatusEnum.InProgress
        );
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsReviewAndFinalizePut(this.formGroup.getRawValue()).subscribe(() => {
            this.alertService.clearAlerts();
            this.alertService.pushAlert(new Alert("Your observations were successfully updated.", AlertContext.Success));
            this.ovtaWorkflowProgressService.updateProgress(this.ovtaID);
            if (andContinue) {
                this.router.navigate([`../../../${this.ovtaID}`], { relativeTo: this.route });
            }
        });
    }

    public addFeatureCollectionToFeatureGroup(featureJsons: any, featureGroup: L.FeatureGroup, style) {
        L.geoJson(featureJsons, {
            onEachFeature: (feature, layer) => {
                layer.setStyle(style);
                if (layer.getLayers) {
                    layer.getLayers().forEach((l) => {
                        featureGroup.addLayer(l);
                    });
                } else {
                    featureGroup.addLayer(layer);
                }
            },
        });
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

    public onCheckBoxChanged(event) {
        console.log(event.target.id);
        console.log(event.target.checked);
        // this.formGroup.controls.PreliminarySourceIdentificationTypeWorkflowDtos.getRawValue()[event.target.id].IsInOnlandAssessmentArea =
        //     !this.formGroup.controls.PreliminarySourceIdentificationTypeWorkflowDtos.getRawValue()[event.target.id].IsInOnlandAssessmentArea;
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
