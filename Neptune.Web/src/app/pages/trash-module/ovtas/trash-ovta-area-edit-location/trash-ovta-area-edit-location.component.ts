import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import "leaflet-gesture-handling";
import "leaflet.fullscreen";
import "leaflet-loading";
import "leaflet.markercluster";
import { LandUseBlockLayerComponent } from "../../../../shared/components/leaflet/layers/land-use-block-layer/land-use-block-layer.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentAreaDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { TransectLineLayerComponent } from "../../../../shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";
import { SelectParcelLayerComponent } from "../../../../shared/components/leaflet/layers/select-parcel-layer/select-parcel-layer.component";
import { OnlandVisualTrashAssessmentAreaGeometryDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-geometry-dto";

@Component({
    selector: "trash-ovta-area-edit-location",
    standalone: true,
    imports: [PageHeaderComponent, NeptuneMapComponent, LandUseBlockLayerComponent, NgIf, AsyncPipe, TransectLineLayerComponent, SelectParcelLayerComponent, RouterLink],
    templateUrl: "./trash-ovta-area-edit-location.component.html",
    styleUrl: "./trash-ovta-area-edit-location.component.scss",
})
export class TrashOvtaAreaEditLocationComponent {
    public customRichTextTypeID = NeptunePageTypeEnum.EditOVTAArea;

    public onlandVisualTrashAssessmentArea$: Observable<OnlandVisualTrashAssessmentAreaDetailDto>;
    public mapHeight = window.innerHeight - window.innerHeight * 0.4 + "px";
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public bounds: any;

    public selectedParcels: number[] = [];

    public canPickParcels: boolean = false;

    public isPerformingDrawAction: boolean = false;
    public drawMapClicked: boolean = false;

    public drawnItems: L.featureGroup;
    public drawControl: L.Control;
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

    constructor(private router: Router, private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService, private route: ActivatedRoute) {}

    public handleMapReady(event: NeptuneMapInitEvent, geometry): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.addFeatureCollectionToFeatureGroup(JSON.parse(geometry), this.layer);
        this.setControl();

        this.layer.addTo(this.map);
        this.mapIsReady = true;
    }

    ngOnInit(): void {
        this.onlandVisualTrashAssessmentArea$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(
                    params[routeParams.onlandVisualTrashAssessmentAreaID]
                );
            })
        );
    }

    public handleLayerBoundsCalculated(bounds: any) {
        this.bounds = bounds;
    }

    public setSelectedParcels(event) {
        this.selectedParcels = event;
    }

    public save(ovtaAreaID) {
        var ovtaGeometryDto = new OnlandVisualTrashAssessmentAreaGeometryDto();
        ovtaGeometryDto.UsingParcels = this.canPickParcels;
        this.layer.eachLayer((layer) => {
            ovtaGeometryDto.Geometry = JSON.stringify(layer.toGeoJSON());
        });
        ovtaGeometryDto.ParcelIDs = this.selectedParcels;
        ovtaGeometryDto.OnlandVisualTrashAssessmentAreaID = ovtaAreaID;
        console.log(ovtaGeometryDto);
        this.onlandVisualTrashAssessmentAreaService
            .onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesPost(ovtaAreaID, ovtaGeometryDto)
            .subscribe((x) => {
                this.router.navigate(["../"], { relativeTo: this.route });
            });
    }

    public addOrRemoveDrawControl(turnOn: boolean) {
        if (turnOn) {
            var drawOptions = {
                draw: Object.assign({}, this.defaultDrawControlSpec),
                edit: Object.assign({}, this.defaultEditControlSpec),
            };
            // if (this.layer._layers == null) {
            //     drawOptions.edit = false;
            // } else {
            //     //drawOptions.draw = false;
            //     // drawOptions.edit.edit = false;
            // }
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
            .on(L.Draw.Event.DRAWSTART, () => {
                this.layer.clearLayers();
            })
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
}
