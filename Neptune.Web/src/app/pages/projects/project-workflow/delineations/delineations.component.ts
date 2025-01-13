import { ApplicationRef, ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import * as L from "leaflet";
import "leaflet-draw";
import "leaflet.fullscreen";
import { forkJoin, Observable, switchMap } from "rxjs";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import { environment } from "src/environments/environment";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { DelineationTypeEnum } from "src/app/shared/generated/enum/delineation-type-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { TreatmentBMPDisplayDto } from "src/app/shared/generated/model/treatment-bmp-display-dto";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { AsyncPipe, NgFor, NgIf } from "@angular/common";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { WorkflowBodyComponent } from "../../../../shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { ProjectWorkflowProgressService } from "src/app/shared/services/project-workflow-progress.service";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { routeParams } from "src/app/app.routes";
import { InventoriedBMPsLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-layer/inventoried-bmps-layer.component";

declare var $: any;

@Component({
    selector: "delineations",
    templateUrl: "./delineations.component.html",
    styleUrls: ["./delineations.component.scss"],
    standalone: true,
    imports: [
        CustomRichTextComponent,
        NgFor,
        NgIf,
        AsyncPipe,
        FieldDefinitionComponent,
        PageHeaderComponent,
        WorkflowBodyComponent,
        AlertDisplayComponent,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        DelineationsLayerComponent,
        JurisdictionsLayerComponent,
        WqmpsLayerComponent,
        StormwaterNetworkLayerComponent,
        InventoriedBMPsLayerComponent,
    ],
})
export class DelineationsComponent implements OnInit {
    public mapIsReady: boolean = false;

    public drawMapClicked: boolean = false;
    public delineations: DelineationUpsertDto[];
    private originalDelineations: string;
    public zoomMapToDefaultExtent: boolean = true;
    public mapHeight: string = "750px";
    public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
    public onEachFeatureCallback?: (feature, layer) => void;

    @Output()
    public afterSetControl: EventEmitter<L.Control.Layers> = new EventEmitter();

    @Output()
    public afterLoadMap: EventEmitter<L.LeafletEvent> = new EventEmitter();

    @Output()
    public onMapMoveEnd: EventEmitter<L.LeafletEvent> = new EventEmitter();

    public map: L.Map;
    public featureLayer: any;
    public delineationFeatureGroup: L.FeatureGroup = new L.FeatureGroup();
    public editableDelineationFeatureGroup: L.FeatureGroup = new L.FeatureGroup();
    private preStartEditingEditableDelineationFeatureGroup: string;
    public layerControl: L.Control.Layers;
    public boundingBox$: Observable<BoundingBoxDto>;
    private squareMetersToAcreDivisor: number = 4047;
    public selectedListItem: number;
    public selectedObjectMarker: L.Layer;
    public selectedDelineation: DelineationUpsertDto;
    public selectedTreatmentBMP: TreatmentBMPDisplayDto;
    public treatmentBMPsLayer: L.GeoJSON<any>;
    public selectedListItemDetails: { [key: string]: any } = {};
    private delineationDefaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };
    private delineationSelectedStyle = {
        color: "yellow",
        fillOpacity: 0.2,
        opacity: 1,
    };
    private delineationTransparentStyle = {
        fillOpacity: 0,
        opacity: 0,
    };
    public projectID: number;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampDelineations;

    public isPerformingDrawAction: boolean = false;
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
        featureGroup: this.editableDelineationFeatureGroup,
        remove: true,
        poly: {
            allowIntersection: false, // Restricts shapes to simple polygons
            drawError: {
                color: "#E1E100", // Color the shape will turn when intersects
                message: "Self-intersecting polygons are not allowed.", // Message that will show when intersect
            },
        },
    };
    private drawControl: L.Control.Draw;
    private newDelineationID: number = -1;
    public isLoadingSubmit: boolean = false;
    public isEditingLocation = false;

    public projectTreatmentBMPs: Array<TreatmentBMPDisplayDto>;

    constructor(
        private treatmentBMPService: TreatmentBMPService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService,
        private route: ActivatedRoute,
        private router: Router,
        private alertService: AlertService,
        private cdr: ChangeDetectorRef,
        private projectWorkflowProgressService: ProjectWorkflowProgressService,
        private projectService: ProjectService
    ) {}

    canExit() {
        let currentDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));
        if (this.isPerformingDrawAction) {
            return (
                this.originalDelineations == currentDelineations &&
                this.preStartEditingEditableDelineationFeatureGroup == JSON.stringify(this.editableDelineationFeatureGroup.getLayers().map((x) => x.getLatLngs))
            );
        }

        return this.originalDelineations == currentDelineations;
    }

    public ngOnInit(): void {
        this.boundingBox$ = this.route.params.pipe(
            switchMap((params) => {
                this.projectID = parseInt(params[routeParams.projectID]);
                return this.stormwaterJurisdictionService.jurisdictionsProjectIDGetBoundingBoxByProjectIDGet(this.projectID);
            })
        );
        this.compileService.configure(this.appRef);
    }

    ngOnDestroy() {
        this.cdr.detach();
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;

        const projectID = this.route.snapshot.paramMap.get("projectID");
        if (projectID) {
            this.projectID = parseInt(projectID);

            forkJoin({
                project: this.projectService.projectsProjectIDGet(this.projectID),
                treatmentBMPs: this.treatmentBMPService.treatmentBMPsGet(),
                delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
            }).subscribe(({ project, treatmentBMPs, delineations }) => {
                // redirect to review step if project is shared with OCTA grant program
                if (project.ShareOCTAM2Tier2Scores) {
                    this.router.navigateByUrl(`projects/edit/${projectID}/review-and-share`);
                }
                this.projectTreatmentBMPs = treatmentBMPs.filter((x) => x.ProjectID == this.projectID);
                if (this.projectTreatmentBMPs.length == 0) {
                    this.router.navigateByUrl(`/projects/edit/${this.projectID}`);
                }

                this.delineations = delineations;
                this.originalDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));

                this.initializeMap();
            });
        }

        this.registerClickEvents();
    }

    public initializeMap(): void {
        this.delineationFeatureGroup.addTo(this.map);
        this.editableDelineationFeatureGroup.addTo(this.map);
        this.resetDelineationFeatureGroups();

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
        });
        this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
        this.treatmentBMPsLayer.addTo(this.map);
        this.setControl();
    }

    private mapTreatmentBMPsToGeoJson(treatmentBMPs: TreatmentBMPDisplayDto[]) {
        return {
            type: "FeatureCollection",
            features: treatmentBMPs.map((x) => {
                let treatmentBMPGeoJson = {
                    type: "Feature",
                    geometry: {
                        type: "Point",
                        coordinates: [x.Longitude ?? 0, x.Latitude ?? 0],
                    },
                    properties: {
                        TreatmentBMPID: x.TreatmentBMPID,
                        TreatmentBMPName: x.TreatmentBMPName,
                        TreatmentBMPTypeName: x.TreatmentBMPTypeName,
                        Latitude: x.Latitude,
                        Longitude: x.Longitude,
                    },
                };
                return treatmentBMPGeoJson;
            }),
        };
    }

    private mapDelineationsToGeoJson(delineations: DelineationUpsertDto[]) {
        return delineations.map((x) => JSON.parse(x.Geometry));
    }

    public addFeatureCollectionToFeatureGroup(featureJsons: any, featureGroup: L.FeatureGroup) {
        L.geoJson(featureJsons, {
            onEachFeature: (feature, layer) => {
                this.addLayersToFeatureGroup(layer, featureGroup);
                layer.on("click", (e) => {
                    if (this.isEditingLocation) {
                        return;
                    }
                    this.selectFeatureImpl(feature.properties.TreatmentBMPID);
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

    public addOrRemoveDrawControl(turnOn: boolean) {
        if (turnOn) {
            var drawOptions = {
                draw: Object.assign({}, this.defaultDrawControlSpec),
                edit: Object.assign({}, this.defaultEditControlSpec),
            };
            if (this.selectedDelineation?.Geometry == null || this.drawMapClicked) {
                drawOptions.edit = false;
            } else {
                drawOptions.draw = false;
                if (this.selectedDelineation.DelineationTypeID == DelineationTypeEnum.Centralized) {
                    drawOptions.edit.edit = false;
                }
            }
            this.drawControl = new L.Control.Draw(drawOptions);
            this.drawControl.addTo(this.map);
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
                var delineationUpsertDto = this.delineations.find((x) => this.selectedTreatmentBMP.TreatmentBMPID == x.TreatmentBMPID);
                if (delineationUpsertDto == null) {
                    delineationUpsertDto = new DelineationUpsertDto({
                        DelineationID: this.newDelineationID,
                        TreatmentBMPID: this.selectedTreatmentBMP.TreatmentBMPID,
                    });
                    this.delineations = this.delineations.concat(delineationUpsertDto);
                    this.newDelineationID--;
                }
                delineationUpsertDto.DelineationTypeID = DelineationTypeEnum.Distributed;
                const geometry = layer.toGeoJSON();
                geometry.properties = {
                    TreatmentBMPID: this.selectedTreatmentBMP.TreatmentBMPID,
                    DelineationID: this.newDelineationID,
                };
                delineationUpsertDto.Geometry = JSON.stringify(geometry);
                delineationUpsertDto.DelineationArea = +(L.GeometryUtil.geodesicArea(layer.getLatLngs()[0]) / this.squareMetersToAcreDivisor).toFixed(2);
                this.resetDelineationFeatureGroups();
                this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
            })
            .on(L.Draw.Event.EDITED, (event) => {
                this.isPerformingDrawAction = false;
                const layers = (event as L.DrawEvents.Edited).layers;
                layers.eachLayer((layer) => {
                    var delineationUpsertDto = this.delineations.find((x) => layer.feature.properties.TreatmentBMPID == x.TreatmentBMPID);
                    delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON());
                    delineationUpsertDto.DelineationArea = +(L.GeometryUtil.geodesicArea(layer.getLatLngs()[0]) / this.squareMetersToAcreDivisor).toFixed(2);
                });
                this.resetDelineationFeatureGroups();
                this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
            })
            .on(L.Draw.Event.DELETED, (event) => {
                this.isPerformingDrawAction = false;
                const layers = (event as L.DrawEvents.Deleted).layers;
                layers.eachLayer((layer) => {
                    var delineationUpsertDto = this.delineations.find((x) => layer.feature.properties.TreatmentBMPID == x.TreatmentBMPID);
                    delineationUpsertDto.Geometry = null;
                    delineationUpsertDto.DelineationArea = null;
                });
                this.resetDelineationFeatureGroups();
                this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
            })
            .on(L.Draw.Event.DRAWSTART, () => {
                if (this.selectedDelineation != null && this.selectedDelineation.DelineationTypeID == DelineationTypeEnum.Centralized) {
                    this.editableDelineationFeatureGroup.clearLayers();
                }
            })
            .on(L.Draw.Event.TOOLBAROPENED, () => {
                this.isPerformingDrawAction = true;
                this.preStartEditingEditableDelineationFeatureGroup = JSON.stringify(this.editableDelineationFeatureGroup.getLayers().map((x) => x.getLatLngs()));
            })
            .on(L.Draw.Event.TOOLBARCLOSED, () => {
                this.isPerformingDrawAction = false;
                this.preStartEditingEditableDelineationFeatureGroup = "";
            });
        this.addOrRemoveDrawControl(true);
        this.afterSetControl.emit(this.layerControl);
    }

    public registerClickEvents(): void {
        this.map.on("click", (event: L.LeafletEvent) => {
            if (!this.isEditingLocation) {
                return;
            }
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = new L.marker(event.latlng, { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 });

            this.selectedObjectMarker.addTo(this.map);

            this.selectedTreatmentBMP.Latitude = event.latlng.lat;
            this.selectedTreatmentBMP.Longitude = event.latlng.lng;
            this.updateTreatmentBMPsLayer();
        });
    }

    public selectFeatureImpl(treatmentBMPID: number) {
        if (this.isPerformingDrawAction) {
            return;
        }

        let hasFlownToSelectedObject = false;

        this.drawControl.remove();
        if (this.selectedListItem) {
            this.selectedListItem = null;
        }

        if (this.selectedDelineation) {
            this.editableDelineationFeatureGroup.clearLayers();
            this.selectedDelineation = null;
        }

        this.selectedDelineation = this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID);
        this.delineationFeatureGroup.eachLayer((layer) => {
            if (this.selectedDelineation == null || this.selectedDelineation.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
                layer.setStyle(this.delineationDefaultStyle);
                return;
            }
            this.addFeatureCollectionToFeatureGroup(layer.toGeoJSON(), this.editableDelineationFeatureGroup);
            this.editableDelineationFeatureGroup.eachLayer((l) => {
                l.setStyle(this.delineationSelectedStyle).bringToFront();
            });
            layer.setStyle(this.delineationTransparentStyle);
            this.map.flyToBounds(layer.getBounds(), { padding: new L.Point(50, 50) });
            hasFlownToSelectedObject = true;
        });

        this.selectedListItem = treatmentBMPID;
        this.selectedTreatmentBMP = this.projectTreatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
        this.treatmentBMPsLayer?.eachLayer((layer) => {
            if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
                layer.setIcon(MarkerHelper.treatmentBMPMarker).setZIndexOffset(1000);
                return;
            }
            layer.setIcon(MarkerHelper.selectedMarker);
            layer.setZIndexOffset(10000);
            if (!hasFlownToSelectedObject) {
                this.map.flyTo(layer.getLatLng(), 18);
            }
        });
        this.addOrRemoveDrawControl(true);

        if (this.drawMapClicked) {
            if (this.isEditingLocation) {
                return;
            }
            if (this.selectedDelineation?.Geometry != null && this.selectedDelineation?.DelineationTypeID != DelineationTypeEnum.Centralized) {
                $(".leaflet-draw-edit-edit").get(0).click();
            } else {
                $(".leaflet-draw-draw-polygon").get(0).click();
            }
        }
        this.drawMapClicked = false;
    }

    public treatmentBMPHasDelineationGeometry(treatmentBMPID: number) {
        return this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID)?.Geometry;
    }

    public getTreatmentBMPDelineation(treatmentBMPID: number) {
        return this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID);
    }

    public getDelineationAreaForTreatmentBMP(treatmentBMPID: number) {
        let delineation = this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID);

        if (delineation?.DelineationArea == null) {
            return "Not provided yet";
        }

        return `${delineation.DelineationArea} ac`;
    }

    public drawDelineationForTreatmentBMP(treatmentBMPID: number) {
        this.drawMapClicked = true;

        if (this.delineations.some((x) => x.TreatmentBMPID == treatmentBMPID)) {
            return;
        }

        var newDelineation = new DelineationUpsertDto({
            DelineationTypeID: DelineationTypeEnum.Distributed,
            TreatmentBMPID: treatmentBMPID,
        });

        this.delineations = this.delineations.concat(newDelineation);
    }

    public save(continueToNextStep?: boolean) {
        this.isLoadingSubmit = true;
        this.alertService.clearAlerts();
        this.getFullyQualifiedJSONGeometryForDelineations(this.delineations);
        var updatedDelineations = this.delineations.filter((x) => x.Geometry != null);
        forkJoin({
            delineations: this.projectService.projectsProjectIDDelineationsPut(this.projectID, updatedDelineations),
            treatmentBMPs: this.treatmentBMPService.treatmentBMPsProjectIDUpdateLocationsPut(this.projectID, this.projectTreatmentBMPs),
        }).subscribe(
            ({ delineations, treatmentBMPs }) => {
                window.scroll(0, 0);
                this.isLoadingSubmit = false;
                this.projectWorkflowProgressService.updateProgress(this.projectID);
                this.projectService.projectsProjectIDDelineationsGet(this.projectID).subscribe((delineations) => {
                    this.delineations = delineations;
                    this.originalDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));
                    this.resetDelineationFeatureGroups();
                    this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);

                    if (continueToNextStep) {
                        this.router.navigateByUrl(`/projects/edit/${this.projectID}/stormwater-treatments/modeled-performance-and-metrics`).then((x) => {
                            this.alertService.pushAlert(new Alert("Your Delineation changes have been saved.", AlertContext.Success, true));
                        });
                        return;
                    }

                    this.alertService.pushAlert(new Alert("Your Delineation changes have been saved.", AlertContext.Success, true));
                });
            },
            (error) => {
                this.isLoadingSubmit = false;
                window.scroll(0, 0);
                this.cdr.detectChanges();
            }
        );
    }

    public getFullyQualifiedJSONGeometryForDelineations(delineations: DelineationUpsertDto[]) {
        //We need a fully qualified geojson string and above we are just getting the geometry
        //Possible can remove the update above if we are always going to do it here
        this.delineationFeatureGroup.eachLayer((layer) => {
            var delineationUpsertDto = delineations.find((x) => x.TreatmentBMPID == layer.feature.properties.TreatmentBMPID);
            delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON());
        });
    }

    private resetDelineationFeatureGroups() {
        this.editableDelineationFeatureGroup.clearLayers();
        this.preStartEditingEditableDelineationFeatureGroup = "";
        this.delineationFeatureGroup.clearLayers();
        this.addFeatureCollectionToFeatureGroup(this.mapDelineationsToGeoJson(this.delineations), this.delineationFeatureGroup);
    }

    public getUpstreamRSBCatchmentForTreatmentBMP(treatmentBMPID: number) {
        this.treatmentBMPService.treatmentBMPsTreatmentBMPIDUpstreamRSBCatchmentGeoJSONGet(treatmentBMPID).subscribe((result) => {
            let currentDelineationForTreatmentBMP = this.delineations.find((x) => x.TreatmentBMPID == treatmentBMPID);
            if (currentDelineationForTreatmentBMP == null) {
                currentDelineationForTreatmentBMP = new DelineationUpsertDto({
                    TreatmentBMPID: treatmentBMPID,
                });
                this.delineations = this.delineations.concat(currentDelineationForTreatmentBMP);
            }
            currentDelineationForTreatmentBMP.DelineationTypeID = DelineationTypeEnum.Centralized;
            currentDelineationForTreatmentBMP.Geometry = result.GeometryGeoJSON;
            currentDelineationForTreatmentBMP.DelineationArea = result.Area;
            this.resetDelineationFeatureGroups();
            this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
        });
    }
    public toggleIsEditingLocation() {
        if (this.isEditingLocation) {
            this.save();
        }
        this.isEditingLocation = !this.isEditingLocation;
        $(".leaflet-interactive").css("cursor", "crosshair");
        this.updateTreatmentBMPsLayer();
    }

    public treatmentBMPHasDelineation(treatmentBMPID: number) {
        return this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID) != null;
    }

    public updateTreatmentBMPsLayer() {
        if (this.treatmentBMPsLayer) {
            this.map.removeLayer(this.treatmentBMPsLayer);
            this.treatmentBMPsLayer = null;
        }

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                if (this.selectedTreatmentBMP != null) {
                    if (layer.feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID) {
                        return;
                    }
                    this.map.flyTo(layer.getLatLng(), 18);
                }
            },
        });
        this.treatmentBMPsLayer.addTo(this.map);

        this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
            if (this.isEditingLocation) {
                return;
            }
            this.selectFeatureImpl(event.propagatedFrom.feature.properties.TreatmentBMPID);
        });
    }

    private clearSelectedItem() {
        if (this.selectedListItem) {
            this.selectedListItem = null;
            this.selectedListItemDetails = {};
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = null;
        }
    }

    public selectTreatmentBMP(treatmentBMPID: number) {
        this.isEditingLocation = false;
        this.selectTreatmentBMPImpl(treatmentBMPID);
        this.updateTreatmentBMPsLayer();
    }

    private selectTreatmentBMPImpl(treatmentBMPID: number) {
        this.clearSelectedItem();

        this.selectedListItem = treatmentBMPID;
        let selectedNumber = null;
        let selectedAttributes = null;
        this.selectedTreatmentBMP = this.projectTreatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
        selectedAttributes = [
            `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
            `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
            `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`,
        ];

        if (this.selectedTreatmentBMP && this.selectedTreatmentBMP.Latitude && this.selectedTreatmentBMP.Longitude) {
            this.selectedObjectMarker = new L.marker(
                { lat: this.selectedTreatmentBMP.Latitude, lng: this.selectedTreatmentBMP.Longitude },
                { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 }
            );

            this.selectedObjectMarker.addTo(this.map);
            this.selectedListItemDetails.title = `${selectedNumber}`;
            this.selectedListItemDetails.attributes = selectedAttributes;
        }
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }
}
