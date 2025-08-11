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
import { AsyncPipe } from "@angular/common";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentAreaDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { TransectLineLayerComponent } from "../../../../shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";
import { OnlandVisualTrashAssessmentAreaGeometryDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-geometry-dto";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { WfsService } from "src/app/shared/services/wfs.service";

@Component({
    selector: "trash-ovta-area-edit-location",
    imports: [PageHeaderComponent, NeptuneMapComponent, LandUseBlockLayerComponent, AsyncPipe, TransectLineLayerComponent, RouterLink],
    templateUrl: "./trash-ovta-area-edit-location.component.html",
    styleUrl: "./trash-ovta-area-edit-location.component.scss"
})
export class TrashOvtaAreaEditLocationComponent {
    public customRichTextTypeID = NeptunePageTypeEnum.EditOVTAArea;

    public onlandVisualTrashAssessmentArea$: Observable<OnlandVisualTrashAssessmentAreaDetailDto>;
    public mapHeight = window.innerHeight - window.innerHeight * 0.4 + "px";
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public bounds: any;

    public selectedParcelIDs: number[] = [];

    public canPickParcels: boolean = false;
    public buttonText = "Pick Parcels";

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

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };
    private wqmpStyle = {
        color: "#ff6ba9",
        weight: 2,
        opacity: 0.9,
        fillOpacity: 0.1,
    };
    private noWQMPsStyle = {
        color: "#bbbbbb",
        weight: 1,
        opacity: 0.7,
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

    constructor(
        private router: Router,
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService,
        private route: ActivatedRoute,
        private wfsService: WfsService,
        private groupByPipe: GroupByPipe
    ) {}

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
        this.selectedParcelIDs = event;
    }

    public save(ovtaAreaID) {
        var ovtaGeometryDto = new OnlandVisualTrashAssessmentAreaGeometryDto();
        ovtaGeometryDto.UsingParcels = this.canPickParcels;
        this.layer.eachLayer((layer) => {
            ovtaGeometryDto.GeometryAsGeoJson = JSON.stringify(layer.toGeoJSON());
        });
        ovtaGeometryDto.ParcelIDs = this.selectedParcelIDs;
        ovtaGeometryDto.OnlandVisualTrashAssessmentAreaID = ovtaAreaID;
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

    public setCanPickParcels(ovtaAreaID, boundingBox, geometry) {
        this.canPickParcels = !this.canPickParcels;
        this.layer.clearLayers();
        if (this.canPickParcels) {
            this.drawControl.remove();
            this.addOVTAAreaToLayer(ovtaAreaID);
            this.addParcelsToLayer(boundingBox);
            this.buttonText = "Draw OVTA Areas";
        } else {
            this.addFeatureCollectionToFeatureGroup(JSON.parse(geometry), this.layer);
            this.addOrRemoveDrawControl(true);
            this.buttonText = "Pick Parcels";
        }
        const bounds = this.layer.getBounds();
        this.map.fitBounds(bounds);
    }

    public resetZoom() {
        const bounds = this.layer.getBounds();
        this.map.fitBounds(bounds);
    }

    private addOVTAAreaToLayer(ovtaAreaID) {
        this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesGet(ovtaAreaID).subscribe((parcels) => {
            this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:Parcels", `ParcelID in (${parcels.map((x) => x.ParcelID)})`, "ParcelID").subscribe((response) => {
                const geoJson = L.geoJSON(response, { style: this.highlightStyle });
                geoJson.addTo(this.layer);
                this.selectedParcelIDs = parcels.map((x) => x.ParcelID);
            });
        });
    }

    private addParcelsToLayer(boundingBox) {
        const bbox = boundingBox != null ? `${boundingBox.Bottom},${boundingBox.Right},${boundingBox.Top},${boundingBox.Left}` : null;
        this.wfsService.getGeoserverWFSLayer("OCStormwater:Parcels", "ParcelID", bbox).subscribe((response) => {
            if (response.length == 0) return;
            const featuresGroupedByParcelID = this.groupByPipe.transform(response, "properties.ParcelID");
            Object.keys(featuresGroupedByParcelID).forEach((parcelID) => {
                const geoJson = L.geoJSON(featuresGroupedByParcelID[parcelID], {
                    style: featuresGroupedByParcelID[parcelID][0].properties.WQMPCount > 0 ? this.wqmpStyle : this.noWQMPsStyle,
                });
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.1 });
                });

                geoJson.on("click", (e) => {
                    this.onParcelSelected(Number(parcelID));
                });
                geoJson.addTo(this.layer);
            });
        });
    }

    private onParcelSelected(parcelID: number) {
        if (this.selectedParcelIDs.length > 0 && this.selectedParcelIDs.find((x) => x == parcelID)) {
            this.selectedParcelIDs = this.selectedParcelIDs.filter((x) => x != parcelID);
        } else {
            this.selectedParcelIDs.push(parcelID);
        }
        this.highlightSelectedParcel(parcelID);
    }

    private highlightSelectedParcel(parcelID) {
        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.ParcelID == parcelID) {
                if (geoJsonLayers[0].options.color == this.highlightStyle.color) {
                    layer.setStyle(geoJsonLayers[0].feature.properties.WQMPCount > 0 ? this.wqmpStyle : this.noWQMPsStyle);
                } else {
                    layer.setStyle(this.highlightStyle);
                }

                this.map.fitBounds(layer.getBounds());
            }
        });
    }
}
