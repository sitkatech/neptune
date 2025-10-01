import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, switchMap, tap } from "rxjs";
import { NeptuneMapInitEvent, NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import "@geoman-io/leaflet-geoman-free";
import * as L from "leaflet";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { OvtaObservationLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-observation-layer/ovta-observation-layer.component";
import { AsyncPipe } from "@angular/common";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { WorkflowBodyComponent } from "../../../../shared/components/workflow-body/workflow-body.component";
import { LandUseBlockLayerComponent } from "../../../../shared/components/leaflet/layers/land-use-block-layer/land-use-block-layer.component";
import { ParcelLayerComponent } from "../../../../shared/components/leaflet/layers/parcel-layer/parcel-layer.component";
import { TransectLineLayerComponent } from "src/app/shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";
import { IFeature, OnlandVisualTrashAssessmentDetailDto, OnlandVisualTrashAssessmentRefineAreaDto } from "src/app/shared/generated/model/models";
import { WfsService } from "src/app/shared/services/wfs.service";
import { IconComponent } from "src/app/shared/components/icon/icon.component";

@Component({
    selector: "trash-ovta-refine-assessment-area",
    imports: [
        PageHeaderComponent,
        NeptuneMapComponent,
        AlertDisplayComponent,
        OvtaObservationLayerComponent,
        AsyncPipe,
        WorkflowBodyComponent,
        LandUseBlockLayerComponent,
        ParcelLayerComponent,
        TransectLineLayerComponent,
        IconComponent,
    ],
    templateUrl: "./trash-ovta-refine-assessment-area.component.html",
    styleUrl: "./trash-ovta-refine-assessment-area.component.scss",
})
export class TrashOvtaRefineAssessmentAreaComponent {
    @Input() onlandVisualTrashAssessmentID!: number;
    public customRichTextTypeID = NeptunePageTypeEnum.EditOVTAArea;
    public isLoading: boolean = false;

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;
    public onlandVisualTrashAssessmentArea$: Observable<IFeature[]>;
    public mapHeight = window.innerHeight - window.innerHeight * 0.4 + "px";
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public bounds: any;
    public isLoadingSubmit: boolean = false;

    // Geoman does not use drawControl or drawnItems
    public isPerformingDrawAction: boolean = false;
    public layer: L.FeatureGroup = new L.FeatureGroup();

    private defaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private router: Router,
        private alertService: AlertService,
        private wfsService: WfsService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService
    ) {}

    ngOnInit(): void {
        this.isLoading = true;
        this.onlandVisualTrashAssessment$ = this.onlandVisualTrashAssessmentService
            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(this.onlandVisualTrashAssessmentID)
            .pipe(tap(() => (this.isLoading = false)));

        this.onlandVisualTrashAssessmentArea$ = this.onlandVisualTrashAssessment$.pipe(
            switchMap(() => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDAreaAsFeatureCollectionGet(
                    this.onlandVisualTrashAssessmentID
                );
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent, featureCollection: any): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.addFeatureCollectionToFeatureGroup(featureCollection, this.layer);
        this.setControl();

        this.layer.addTo(this.map);
        this.mapIsReady = true;
    }

    public save(andContinue = false) {
        var onlandVisualTrashAssessmentRefineArea = new OnlandVisualTrashAssessmentRefineAreaDto();
        onlandVisualTrashAssessmentRefineArea.OnlandVisualTrashAssessmentID = this.onlandVisualTrashAssessmentID;
        this.layer.eachLayer((layer: L.Layer & { toGeoJSON: () => GeoJSON.Feature }) => {
            onlandVisualTrashAssessmentRefineArea.GeometryAsGeoJson = JSON.stringify(layer.toGeoJSON());
        });
        debugger;
        this.onlandVisualTrashAssessmentService
            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDRefineAreaPost(this.onlandVisualTrashAssessmentID, onlandVisualTrashAssessmentRefineArea)
            .subscribe(() => {
                this.isLoadingSubmit = false;
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Successfully updated Assessment Area.", AlertContext.Success));
                this.ovtaWorkflowProgressService.updateProgress(this.onlandVisualTrashAssessmentID);
                if (andContinue) {
                    this.router.navigate([`/trash/onland-visual-trash-assessments/edit/${this.onlandVisualTrashAssessmentID}/review-and-finalize`]);
                }
            });
    }

    public setControl(): void {
        this.map
            .on("pm:create", (event: { shape: string; layer: L.Polygon & { toGeoJSON: () => GeoJSON.Feature } }) => {
                this.isPerformingDrawAction = false;
                const layer = event.layer;
                this.layer.clearLayers();
                this.layer.addLayer(layer);
                this.selectFeatureImpl();
            })
            .on("pm:globaleditmodetoggled", (e: any) => {
                if (e.enabled) {
                    return;
                }
                // Edit mode exited, sync geometry
                this.syncGeometryFromLayer();
                this.selectFeatureImpl();
            })
            .on("pm:globalremovalmodetoggled", (e: any) => {
                if (e.enabled) {
                    // Remove geometry
                    this.layer.clearLayers();
                    this.syncGeometryFromLayer();
                    this.map.pm.toggleGlobalRemovalMode();
                    return;
                }

                this.syncGeometryFromLayer();
                this.selectFeatureImpl();
            });
        this.addOrRemoveGeomanControl(true);
    }

    public addOrRemoveGeomanControl(turnOn: boolean) {
        if (turnOn) {
            const hasPolygon = this.layer.getLayers().length > 0;
            this.map.pm.addControls({
                position: "topleft",
                drawMarker: false,
                drawText: false,
                drawCircleMarker: false,
                drawPolyline: false,
                drawRectangle: false,
                drawPolygon: !hasPolygon,
                drawCircle: false,
                editMode: hasPolygon,
                removalMode: hasPolygon,
                cutPolygon: false,
                dragMode: false,
                rotateMode: false,
                snappingOption: true,
                showCancelButton: true,
            });
            this.map.pm.setGlobalOptions({ allowSelfIntersection: false });
            this.map.pm.setLang(
                "en",
                {
                    buttonTitles: {
                        drawPolyButton: "Add Assessment Area",
                        editButton: "Edit Assessment Area",
                        deleteButton: "Delete Assessment Area",
                    },
                },
                "en"
            );
            return;
        }
        this.map.pm.removeControls();
    }

    public syncGeometryFromLayer() {
        // Sync the geometry from the layer to the DTO
        if (this.layer.getLayers().length > 0) {
            const layer = this.layer.getLayers()[0] as L.Layer & { toGeoJSON: () => GeoJSON.Feature };
            // Save geometry to DTO (if needed)
            // This can be extended if DTO is stored in component
        }

        // No geometry present
    }

    public selectFeatureImpl() {
        if (this.isPerformingDrawAction) {
            return;
        }
        this.map.pm.removeControls();
        this.layer.setStyle(this.defaultStyle).bringToFront();
        this.addOrRemoveGeomanControl(true);
    }

    public addFeatureCollectionToFeatureGroup(featureCollection: any, featureGroup: L.FeatureGroup) {
        console.log(featureCollection);
        if (featureCollection.features.length > 0) {
            L.geoJson(featureCollection, {
                onEachFeature: function (feature, layer) {
                    featureGroup.addLayer(layer);
                },
            });
            // if (featureCollection.geometry.type === "MultiPolygon") {
            //     featureCollection.geometry.coordinates.forEach(function (shapeCoords, i) {
            //         var polygon = { type: "Polygon", coordinates: shapeCoords };
            //         L.geoJson(polygon, {
            //             onEachFeature: function (feature, layer) {
            //                 featureGroup.addLayer(layer);
            //             },
            //         });
            //     });
            // } else {
            //     L.geoJson(featureCollection, {
            //         onEachFeature: (feature, layer) => {
            //             if (layer.getLayers) {
            //                 layer.getLayers().forEach((l) => {
            //                     featureGroup.addLayer(l);
            //                 });
            //             } else {
            //                 featureGroup.addLayer(layer);
            //             }
            //         },
            //     });
            // }
        }
    }
}
