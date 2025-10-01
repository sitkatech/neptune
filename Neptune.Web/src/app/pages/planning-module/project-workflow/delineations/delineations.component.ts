import * as turf from "@turf/turf";
import { ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from "@angular/core";
import { Router } from "@angular/router";
import { Input } from "@angular/core";
import L, { PM } from "leaflet";
import "@geoman-io/leaflet-geoman-free";
import { forkJoin, Observable, switchMap } from "rxjs";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { environment } from "src/environments/environment";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { DelineationTypeEnum } from "src/app/shared/generated/enum/delineation-type-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { TreatmentBMPDisplayDto } from "src/app/shared/generated/model/treatment-bmp-display-dto";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { AsyncPipe } from "@angular/common";
import { CustomRichTextComponent } from "src/app/shared/components/custom-rich-text/custom-rich-text.component";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { WorkflowBodyComponent } from "src/app/shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { ProjectWorkflowProgressService } from "src/app/shared/services/project-workflow-progress.service";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { InventoriedBMPsLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-layer/inventoried-bmps-layer.component";

@Component({
    selector: "delineations",
    templateUrl: "./delineations.component.html",
    styleUrls: ["./delineations.component.scss"],
    imports: [
        CustomRichTextComponent,
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
    @Input() projectID!: number;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampDelineations;

    public isPerformingDrawAction: boolean = false;
    private newDelineationID: number = -1;
    public isLoadingSubmit: boolean = false;
    public isEditingLocation = false;

    public projectTreatmentBMPs: Array<TreatmentBMPDisplayDto>;

    // 1. Initialization & Lifecycle
    constructor(
        private treatmentBMPService: TreatmentBMPService,
        private router: Router,
        private alertService: AlertService,
        private cdr: ChangeDetectorRef,
        private projectWorkflowProgressService: ProjectWorkflowProgressService,
        private projectService: ProjectService
    ) {}

    public ngOnInit(): void {
        this.boundingBox$ = this.projectService.projectsProjectIDBoundingBoxGet(this.projectID);
    }

    ngOnDestroy() {
        this.cdr.detach();
    }

    canExit() {
        let currentDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));
        return this.originalDelineations == currentDelineations;
    }

    // 2. Map Setup & Control
    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;

        if (this.projectID) {
            forkJoin({
                project: this.projectService.projectsProjectIDGet(this.projectID),
                treatmentBMPs: this.projectService.projectsProjectIDTreatmentBmpsGet(this.projectID),
                delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
            }).subscribe(({ project, treatmentBMPs, delineations }) => {
                // redirect to review step if project is shared with OCTA grant program
                if (project.ShareOCTAM2Tier2Scores) {
                    this.router.navigateByUrl(`/planning/projects/edit/${this.projectID}/review-and-share`);
                }
                if (treatmentBMPs.length == 0) {
                    this.router.navigateByUrl(`/planning/projects/edit/${this.projectID}`);
                }

                this.projectTreatmentBMPs = treatmentBMPs;
                this.delineations = delineations;
                this.originalDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));

                this.initializeMap();
                this.cdr.detectChanges();
            });
        }

        this.registerClickEvents();
        this.cdr.detectChanges();
    }

    public initializeMap(): void {
        this.delineationFeatureGroup.addTo(this.map);
        this.resetDelineationFeatureGroups();

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson as any, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
        });
        this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
        this.treatmentBMPsLayer.addTo(this.map);
        this.setControl();
        this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
    }

    public setControl(): void {
        // Only keep global pm:create for new delineations
        this.map
            .on("pm:create", (event: { shape: PM.SUPPORTED_SHAPES; layer: L.Polygon & { toGeoJSON: () => GeoJSON.Feature } }) => {
                this.isPerformingDrawAction = false;
                const layer = event.layer;
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
                // Use turf.area for geodesic area calculation
                const turfArea = turf.area(layer.toGeoJSON());
                delineationUpsertDto.DelineationArea = +(turfArea / this.squareMetersToAcreDivisor).toFixed(2);
                // Remove the drawn layer to prevent duplicates
                if (layer && layer.remove) {
                    layer.remove();
                }
                this.resetDelineationFeatureGroups();

                this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID, true);
            })
            .on("pm:globaleditmodetoggled", (e: any) => {
                if (e.enabled) {
                    return;
                }
                // Edit mode was just exited, user is done editing
                // Sync delineation data with edited layers
                this.getFullyQualifiedJSONGeometryForDelineations(this.delineations);
                this.resetDelineationFeatureGroups();
                if (this.selectedTreatmentBMP) {
                    this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
                }
            })
            .on("pm:globalremovalmodetoggled", (e: any) => {
                if (e.enabled) {
                    var delineationUpsertDto = this.delineations.find((x) => this.selectedTreatmentBMP.TreatmentBMPID == x.TreatmentBMPID);
                    delineationUpsertDto.Geometry = null;
                    delineationUpsertDto.DelineationArea = null;
                    delineationUpsertDto.DelineationTypeID = null;
                    this.resetDelineationFeatureGroups();
                    this.map.pm.toggleGlobalRemovalMode();
                }
                // Delete mode was just exited, user is done editing
                // Sync delineation data with edited layers
                this.getFullyQualifiedJSONGeometryForDelineations(this.delineations);
                this.resetDelineationFeatureGroups();
                if (this.selectedTreatmentBMP) {
                    this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
                }
            });
        this.addOrRemoveGeomanControl(true);
        this.afterSetControl.emit(this.layerControl);
    }

    // 3. Data Mapping & Layer Management
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
        return delineations.filter((x) => x.Geometry != null).map((x) => JSON.parse(x.Geometry));
    }

    public addFeatureCollectionToFeatureGroup(featureJsons: any, featureGroup: L.FeatureGroup) {
        if (!featureJsons || featureJsons.length == 0) {
            return;
        }
        L.geoJson(featureJsons, {
            onEachFeature: (feature, layer) => {
                this.addLayersToFeatureGroup(layer, featureGroup);
                layer.on("click", (e) => {
                    if (this.isEditingLocation || this.map.pm.globalRemovalModeEnabled() || this.map.pm.globalEditModeEnabled()) {
                        return;
                    }
                    this.selectFeatureImpl(feature.properties.TreatmentBMPID);
                });
                // Attach Geoman events per-layer
                layer.on("pm:edit", (event) => {
                    const editedLayer = event.layer as L.Polygon;
                    var delineationUpsertDto = this.delineations.find((x) => editedLayer.feature.properties.TreatmentBMPID == x.TreatmentBMPID);
                    // Always set delineation type to distributed when edited
                    delineationUpsertDto.DelineationTypeID = DelineationTypeEnum.Distributed;
                    this.selectedDelineation.DelineationTypeID = DelineationTypeEnum.Distributed;
                    delineationUpsertDto.Geometry = JSON.stringify(editedLayer.toGeoJSON());
                    // Use turf.area for geodesic area calculation
                    const turfArea = turf.area(editedLayer.toGeoJSON());
                    delineationUpsertDto.DelineationArea = +(turfArea / this.squareMetersToAcreDivisor).toFixed(2);
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

    private resetDelineationFeatureGroups() {
        this.delineationFeatureGroup.clearLayers();
        this.addFeatureCollectionToFeatureGroup(this.mapDelineationsToGeoJson(this.delineations), this.delineationFeatureGroup);
    }

    public toggleIsEditingLocation() {
        if (this.isEditingLocation) {
            this.save();
        }
        this.isEditingLocation = !this.isEditingLocation;
        document.querySelector(".leaflet-interactive").setAttribute("style", "cursor: crosshair");
        this.updateTreatmentBMPsLayer();
        this.isEditingLocation ? this.addOrRemoveGeomanControl(false) : this.addOrRemoveGeomanControl(true);
    }

    // 4. Selection & Interaction
    public selectFeatureImpl(treatmentBMPID: number, skipDrawCheck: boolean = false) {
        if (this.isPerformingGeomanAction(skipDrawCheck)) {
            return;
        }

        let hasFlownToSelectedObject = false;

        this.map.pm.removeControls();

        if (this.selectedListItem) {
            this.selectedListItem = null;
        }

        if (this.selectedDelineation) {
            this.selectedDelineation = null;
        }

        this.selectedDelineation = this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID);
        this.delineationFeatureGroup.eachLayer((layer: L.Polygon) => {
            if (this.selectedDelineation == null || this.selectedDelineation.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
                layer.setStyle(this.delineationDefaultStyle);
                if (layer.pm) layer.pm.disable();
                return;
            }
            layer.setStyle(this.delineationSelectedStyle).bringToFront();
            this.map.flyToBounds(layer.getBounds(), { padding: new L.Point(50, 50) });
            hasFlownToSelectedObject = true;
        });

        this.selectedListItem = treatmentBMPID;
        this.selectedTreatmentBMP = this.projectTreatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
        this.treatmentBMPsLayer?.eachLayer((layer: L.Marker) => {
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
        this.addOrRemoveGeomanControl(true);

        if (this.drawMapClicked) {
            if (this.isEditingLocation) {
                return;
            }
            if (this.selectedDelineation?.Geometry != null) {
                const button = document.querySelector(".leaflet-pm-icon-edit") as HTMLButtonElement;
                if (button) {
                    button.click();
                }
            } else {
                const button = document.querySelector(".leaflet-pm-icon-polygon") as HTMLButtonElement;
                if (button) {
                    button.click();
                }
            }
        }
        this.drawMapClicked = false;
    }

    public registerClickEvents(): void {
        this.map.on("click", (event: L.LeafletMouseEvent) => {
            if (!this.isEditingLocation || this.isPerformingGeomanAction()) {
                return;
            }
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = L.marker(event.latlng, { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 });

            this.selectedObjectMarker.addTo(this.map);

            this.selectedTreatmentBMP.Latitude = event.latlng.lat;
            this.selectedTreatmentBMP.Longitude = event.latlng.lng;
            this.updateTreatmentBMPsLayer();
        });
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
            this.selectedListItemDetails.title = `${selectedNumber}`;
            this.selectedListItemDetails.attributes = selectedAttributes;
        }
    }

    // 5. Delineation Actions
    public drawDelineationForTreatmentBMP(treatmentBMPID: number) {
        this.drawMapClicked = true;

        let delineationForTreatmentBMP = this.delineations.find((x) => x.TreatmentBMPID == treatmentBMPID);
        if (delineationForTreatmentBMP) {
            this.selectFeatureImpl(treatmentBMPID);
            return;
        }

        var newDelineation = new DelineationUpsertDto({
            DelineationTypeID: DelineationTypeEnum.Distributed,
            TreatmentBMPID: treatmentBMPID,
        });

        this.delineations = this.delineations.concat(newDelineation);
        this.selectFeatureImpl(treatmentBMPID);
    }

    public getFullyQualifiedJSONGeometryForDelineations(delineations: DelineationUpsertDto[]) {
        //We need a fully qualified geojson string and above we are just getting the geometry
        //Possible can remove the update above if we are always going to do it here
        this.delineationFeatureGroup.eachLayer((layer: L.Polygon) => {
            var delineationUpsertDto = delineations.find((x) => x.TreatmentBMPID == layer.feature.properties.TreatmentBMPID);
            delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON());
        });
    }

    public getUpstreamRSBCatchmentForTreatmentBMP(treatmentBMPID: number) {
        this.treatmentBMPService.treatmentBmpsTreatmentBMPIDUpstreamRSBCatchmentGeoJSONGet(treatmentBMPID).subscribe((result) => {
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

    public save(continueToNextStep?: boolean) {
        this.isLoadingSubmit = true;
        this.alertService.clearAlerts();
        this.getFullyQualifiedJSONGeometryForDelineations(this.delineations);
        var updatedDelineations = this.delineations.filter((x) => x.Geometry != null);
        forkJoin({
            delineations: this.projectService.projectsProjectIDDelineationsPut(this.projectID, updatedDelineations),
            treatmentBMPs: this.projectService.projectsProjectIDTreatmentBmpsUpdateLocationsPut(this.projectID, this.projectTreatmentBMPs),
        }).subscribe({
            next: ({ delineations, treatmentBMPs }) => {
                window.scroll(0, 0);
                this.isLoadingSubmit = false;
                this.projectWorkflowProgressService.updateProgress(this.projectID);
                this.projectService.projectsProjectIDDelineationsGet(this.projectID).subscribe((delineations) => {
                    this.delineations = delineations;
                    this.originalDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));
                    this.resetDelineationFeatureGroups();
                    this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);

                    if (continueToNextStep) {
                        this.router.navigateByUrl(`/planning/projects/edit/${this.projectID}/stormwater-treatments/modeled-performance-and-metrics`).then((x) => {
                            this.alertService.pushAlert(new Alert("Your Delineation changes have been saved.", AlertContext.Success, true));
                        });
                        return;
                    }

                    this.alertService.pushAlert(new Alert("Your Delineation changes have been saved.", AlertContext.Success, true));
                });
            },
            error: (error) => {
                this.isLoadingSubmit = false;
                window.scroll(0, 0);
                this.cdr.detectChanges();
            },
        });
    }

    // 6. Utility & State
    public isPerformingGeomanAction(skipDrawCheck: boolean = false): boolean {
        //MP 10/1/25 - Added skipDrawCheck because the global draw mode remains enabled momentarily after drawing a shape is complete
        return (this.map?.pm?.globalDrawModeEnabled() && !skipDrawCheck) || this.map?.pm?.globalEditModeEnabled() || this.map?.pm?.globalRemovalModeEnabled();
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }

    // 7. Getters
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

    public treatmentBMPHasDelineation(treatmentBMPID: number) {
        return this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID) != null;
    }

    public updateTreatmentBMPsLayer() {
        if (this.treatmentBMPsLayer) {
            this.map.removeLayer(this.treatmentBMPsLayer);
            this.treatmentBMPsLayer = null;
        }
        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson as any, {
            pointToLayer: (feature, latlng) => {
                if (feature.properties.TreatmentBMPID == this.selectedTreatmentBMP?.TreatmentBMPID) {
                    return L.marker(latlng, { icon: MarkerHelper.selectedMarker });
                }
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            onEachFeature: (feature, layer: L.Marker) => {
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

    public addOrRemoveGeomanControl(turnOn: boolean) {
        if (turnOn) {
            // Show delete only if selected TreatmentBMP has a delineation
            const bmpSelected = !!this.selectedTreatmentBMP;
            const hasDelineation = bmpSelected && !!(this.selectedDelineation && this.selectedDelineation.Geometry);
            const allowDraw = bmpSelected && (!this.selectedDelineation || !this.selectedDelineation.Geometry);
            const allowDelete = hasDelineation;
            this.map.pm.addControls({
                position: "topleft",
                drawMarker: false,
                drawText: false,
                drawCircleMarker: false,
                drawPolyline: false,
                drawRectangle: false,
                drawPolygon: allowDraw,
                drawCircle: false,
                editMode: !allowDraw,
                removalMode: allowDelete,
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
                        drawPolyButton: "Add Delineation",
                        editButton: "Edit Delineation",
                        deleteButton: "Delete Delineation",
                    },
                },
                "en"
            );
            return;
        }
        this.map.pm.removeControls();
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
}
