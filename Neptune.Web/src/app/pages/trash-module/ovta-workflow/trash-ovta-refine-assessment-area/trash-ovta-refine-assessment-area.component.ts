import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { NeptuneMapInitEvent, NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import * as L from "leaflet";
import "leaflet-gesture-handling";
import "leaflet.fullscreen";
import "leaflet-loading";
import "leaflet.markercluster";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { OvtaObservationLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-observation-layer/ovta-observation-layer.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { OnlandVisualTrashAssessmentRefineAreaDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-refine-area-dto";

@Component({
    selector: "trash-ovta-refine-assessment-area",
    standalone: true,
    imports: [PageHeaderComponent, NeptuneMapComponent, AlertDisplayComponent, OvtaObservationLayerComponent, AsyncPipe, NgIf],
    templateUrl: "./trash-ovta-refine-assessment-area.component.html",
    styleUrl: "./trash-ovta-refine-assessment-area.component.scss",
})
export class TrashOvtaRefineAssessmentAreaComponent {
    public customRichTextTypeID = NeptunePageTypeEnum.EditOVTAArea;
    public ovtaID: number;

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentRefineAreaDto>;
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
            allowIntersection: false, // Restricts shapes to simple polygons
            drawError: {
                color: "#E1E100", // Color the shape will turn when intersects
                message: "Self-intersecting polygons are not allowed.", // Message that will show when intersect
            },
        },
    };
    private defaultEditControlSpec: L.Control.DrawConstructorOptions = {
        featureGroup: this.layer,
        remove: true,
        edit: {
            featureGroup: this.layer,
        },
        poly: {
            allowIntersection: false, // Restricts shapes to simple polygons
            drawError: {
                color: "#E1E100", // Color the shape will turn when intersects
                message: "Self-intersecting polygons are not allowed.", // Message that will show when intersect
            },
        },
    };

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private route: ActivatedRoute, private router: Router) {}

    public handleMapReady(event: NeptuneMapInitEvent, geometry): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.addFeatureCollectionToFeatureGroup(JSON.parse(geometry), this.layer);
        this.setControl();

        this.layer.addTo(this.map);
        this.mapIsReady = true;
    }

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDRefineAreaGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            })
        );
    }

    public handleLayerBoundsCalculated(bounds: any) {
        this.bounds = bounds;
    }

    public save(andContinue = false) {
        this.isLoadingSubmit = true;
        var onlandVisualTrashAssessmentRefineArea = new OnlandVisualTrashAssessmentRefineAreaDto();
        onlandVisualTrashAssessmentRefineArea.OnlandVisualTrashAssessmentID = this.ovtaID;
        this.layer.eachLayer((layer) => {
            onlandVisualTrashAssessmentRefineArea.GeometryAsGeoJson = JSON.stringify(layer.toGeoJSON());
        });

        this.onlandVisualTrashAssessmentService
            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDRefineAreaPost(this.ovtaID, onlandVisualTrashAssessmentRefineArea)
            .subscribe((response) => {
                this.isLoadingSubmit = false;
                if (andContinue) {
                    this.router.navigate([`../../${this.ovtaID}/review-and-finalize`], { relativeTo: this.route });
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

    public addFeatureCollectionToFeatureGroup(featureJsons: any, featureGroup: L.FeatureGroup) {
        L.geoJson(featureJsons, {
            onEachFeature: (feature, layer) => {
                this.addLayersToFeatureGroup(layer, featureGroup);
                layer.on("click", (e) => {
                    this.selectFeatureImpl();
                });
            },
        });
    }

    private addLayersToFeatureGroup(layer: any, featureGroup: L.FeatureGroup) {
        if (layer.getLayers) {
            layer.getLayers().forEach((l) => {
                featureGroup.addLayer(l);
            });
        } else {
            featureGroup.addLayer(layer);
        }
    }

    public resetZoom() {
        const bounds = this.layer.getBounds();
        this.map.fitBounds(bounds);
    }
}
