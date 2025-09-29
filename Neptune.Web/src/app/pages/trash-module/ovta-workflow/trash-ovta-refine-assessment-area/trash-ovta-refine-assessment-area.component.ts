import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, switchMap, tap } from "rxjs";
import { NeptuneMapInitEvent, NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import "leaflet-draw";
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

    public drawnItems: L.featureGroup;
    public drawControl: L.Control;
    public isPerformingDrawAction: boolean = false;
    public layer: L.FeatureGroup = new L.FeatureGroup();

    private defaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    private defaultDrawControlSpec: L.Control.DrawConstructorOptions = {
        polyline: false,
        rectangle: false,
        circle: false,
        marker: false,
        circlemarker: false,
        polygon: {
            allowIntersection: true, // Restricts shapes to simple polygons
        },
    };
    private defaultEditControlSpec: L.Control.DrawConstructorOptions = {
        featureGroup: this.layer,
        remove: true,
        edit: {
            featureGroup: this.layer,
        },
        poly: {
            allowIntersection: true, // Restricts shapes to simple polygons
        },
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
        this.layer.eachLayer((layer) => {
            onlandVisualTrashAssessmentRefineArea.GeometryAsGeoJson = JSON.stringify(layer.toGeoJSON());
        });

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

    public addOrRemoveDrawControl(turnOn: boolean) {
        if (turnOn) {
            var drawOptions = {
                draw: Object.assign({}, this.defaultDrawControlSpec),
                edit: Object.assign({}, this.defaultEditControlSpec),
            };
            this.drawControl = new L.Control.Draw(drawOptions);
            this.map.addControl(this.drawControl);
            return;
        }
        this.drawControl.remove();
    }

    public setControl(): void {
        L.EditToolbar.Delete.include({
            removeAllLayers: false,
        });

        this.map
            .on(L.Draw.Event.CREATED, (event) => {
                this.isPerformingDrawAction = false;
                const layer = (event as L.DrawEvents.Created).layer;
                this.layer.addLayer(layer);
                this.selectFeatureImpl();
            })
            .on(L.Draw.Event.EDITED, (event) => {
                this.isPerformingDrawAction = false;
                const layers = (event as L.DrawEvents.Edited).layers;
                this.selectFeatureImpl();
            })
            .on(L.Draw.Event.DELETED, (event) => {
                this.isPerformingDrawAction = false;
                const layers = (event as L.DrawEvents.Deleted).layers;
                this.selectFeatureImpl();
            })
            .on(L.Draw.Event.DRAWSTART, () => {})
            .on(L.Draw.Event.TOOLBAROPENED, () => {
                this.isPerformingDrawAction = true;
            })
            .on(L.Draw.Event.TOOLBARCLOSED, () => {
                this.isPerformingDrawAction = false;
            });
        this.addOrRemoveDrawControl(true);
    }

    public selectFeatureImpl() {
        if (this.isPerformingDrawAction) {
            return;
        }
        this.map.removeControl(this.drawControl);
        this.layer.setStyle(this.defaultStyle).bringToFront();
        this.addOrRemoveDrawControl(true);
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
