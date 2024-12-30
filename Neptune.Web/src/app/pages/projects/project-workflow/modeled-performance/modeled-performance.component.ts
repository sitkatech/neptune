import { ApplicationRef, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/authentication.service";
import {
    BoundingBoxDto,
    DelineationUpsertDto,
    PersonDto,
    ProjectNetworkSolveHistorySimpleDto,
    TreatmentBMPUpsertDto,
    ProjectDto,
    TreatmentBMPDisplayDto,
} from "src/app/shared/generated/model/models";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import * as L from "leaflet";
import "leaflet.fullscreen";
import * as esri from "esri-leaflet";
import { GestureHandling } from "leaflet-gesture-handling";
import { forkJoin } from "rxjs";
import { environment } from "src/environments/environment";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { ProjectNetworkSolveHistoryStatusTypeEnum } from "src/app/shared/generated/enum/project-network-solve-history-status-type-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { GrantScoresComponent } from "../../../../shared/components/projects/grant-scores/grant-scores.component";
import { ModelResultsComponent } from "../../../../shared/components/projects/model-results/model-results.component";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { NgIf, NgFor } from "@angular/common";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";

declare var $: any;

@Component({
    selector: "hippocamp-modeled-performance",
    templateUrl: "./modeled-performance.component.html",
    styleUrls: ["./modeled-performance.component.scss"],
    standalone: true,
    imports: [CustomRichTextComponent, NgIf, FieldDefinitionComponent, NgFor, ModelResultsComponent, GrantScoresComponent],
})
export class ModeledPerformanceComponent implements OnInit {
    private currentUser: PersonDto;
    public ProjectNetworkHistoryStatusTypeEnum = ProjectNetworkSolveHistoryStatusTypeEnum;
    public projectNetworkSolveHistories: ProjectNetworkSolveHistorySimpleDto[];

    @ViewChild("mapDiv") mapDiv: ElementRef;
    public mapID: string = "modeledPerformanceMap";
    public treatmentBMPs: Array<TreatmentBMPDisplayDto>;
    public delineations: DelineationUpsertDto[];
    public zoomMapToDefaultExtent: boolean = true;
    public mapHeight: string = "750px";
    public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
    public onEachFeatureCallback?: (feature, layer) => void;
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public tileLayers: { [key: string]: any } = {};
    public overlayLayers: { [key: string]: any } = {};
    private boundingBox: BoundingBoxDto;
    public selectedTreatmentBMP: TreatmentBMPDisplayDto;
    public treatmentBMPsLayer: L.GeoJSON<any>;
    public delineationsLayer: L.GeoJSON<any>;
    private delineationDefaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 1,
    };
    private delineationSelectedStyle = {
        color: "yellow",
        fillOpacity: 0.2,
        opacity: 1,
    };

    public projectID: number;
    public project: ProjectDto;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampModeledPerformance;

    public projectTreatmentBMPs: Array<TreatmentBMPDisplayDto>;
    private inventoriedTreatmentBMPOverlayName =
        "<span>Inventoried BMP Locations<br /> <img src='./assets/main/map-icons/marker-icon-orange.png' style='height:17px; margin:3px'> BMP (Verified)</span>";
    private inventoriedTreatmentBMPsLayer: L.GeoJSON<any>;

    constructor(
        private cdr: ChangeDetectorRef,
        private authenticationService: AuthenticationService,
        private projectService: ProjectService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService,
        private treatmentBMPService: TreatmentBMPService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private route: ActivatedRoute,
        private router: Router,
        private alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;

            const projectID = this.route.snapshot.paramMap.get("projectID");
            if (projectID) {
                this.projectID = parseInt(projectID);
                forkJoin({
                    project: this.projectService.projectsProjectIDGet(this.projectID),
                    treatmentBMPs: this.treatmentBMPService.treatmentBMPsGet(),
                    delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
                    boundingBox: this.stormwaterJurisdictionService.jurisdictionsProjectIDGetBoundingBoxByProjectIDGet(this.projectID),
                    projectNetworkSolveHistories: this.projectService.projectsProjectIDProjectNetworkSolveHistoriesGet(this.projectID),
                }).subscribe(({ project, treatmentBMPs, delineations, boundingBox, projectNetworkSolveHistories }) => {
                    // redirect to review step if project is shared with OCTA grant program
                    if (project.ShareOCTAM2Tier2Scores) {
                        this.router.navigateByUrl(`projects/edit/${projectID}/review-and-share`);
                    }
                    this.projectTreatmentBMPs = treatmentBMPs.filter((x) => x.ProjectID == this.projectID);
                    if (this.projectTreatmentBMPs.length == 0) {
                        this.router.navigateByUrl(`/projects/edit/${this.projectID}`);
                    }

                    this.project = project;
                    this.treatmentBMPs = treatmentBMPs;
                    this.delineations = delineations;
                    this.boundingBox = boundingBox;
                    this.projectNetworkSolveHistories = projectNetworkSolveHistories;
                    this.initializeMap();
                });
            }
        });

        this.tileLayers = Object.assign(
            {},
            {
                Aerial: L.tileLayer("https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}", {
                    attribution: "Aerial",
                    maxZoom: 22,
                    maxNativeZoom: 18,
                }),
                Street: L.tileLayer("https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}", {
                    attribution: "Street",
                    maxZoom: 22,
                    maxNativeZoom: 18,
                }),
                Terrain: L.tileLayer("https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}", {
                    attribution: "Terrain",
                    maxZoom: 22,
                    maxNativeZoom: 18,
                }),
            },
            this.tileLayers
        );

        let regionalSubbasinsWMSOptions = {
            layers: "OCStormwater:RegionalSubbasins",
            transparent: true,
            format: "image/png",
            tiled: true,
        } as L.WMSOptions;

        let jurisdictionsWMSOptions = {
            layers: "OCStormwater:Jurisdictions",
            transparent: true,
            format: "image/png",
            tiled: true,
            styles: "jurisdiction_orange",
        } as L.WMSOptions;

        let WQMPsWMSOptions = {
            layers: "OCStormwater:WaterQualityManagementPlans",
            transparent: true,
            format: "image/png",
            tiled: true,
        } as L.WMSOptions;

        let verifiedDelineationsWMSOptions = {
            layers: "OCStormwater:Delineations",
            transparent: true,
            format: "image/png",
            tiled: true,
            cql_filter: "DelineationStatus = 'Verified'",
        } as L.WMSOptions;

        this.overlayLayers = Object.assign(
            {
                "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    regionalSubbasinsWMSOptions
                ),
                "<span>Stormwater Network <br/> <img src='../../assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({
                    url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
                }),
                "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    jurisdictionsWMSOptions
                ),
                "<img src='./assets/main/map-legend-images/wqmpBoundary.png' style='height:12px; margin-bottom:4px'> WQMPs": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    WQMPsWMSOptions
                ),
                "<span>Inventoried BMP Delineations</br><img src='./assets/main/map-legend-images/delineationVerified.png' style='margin-bottom:3px'></span>": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    verifiedDelineationsWMSOptions
                ),
            },
            this.overlayLayers
        );

        this.compileService.configure(this.appRef);
    }

    public getAboutModelingPerformanceURL(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Home/AboutModelingBMPPerformance`;
    }

    public initializeMap(): void {
        const mapOptions: L.MapOptions = {
            minZoom: 9,
            maxZoom: 22,
            layers: [this.tileLayers["Terrain"]],
            fullscreenControl: true,
            gestureHandling: true,
        } as L.MapOptions;
        this.map = L.map(this.mapID, mapOptions);
        L.Map.addInitHook("addHandler", "gestureHandling", GestureHandling);

        const delineationGeoJson = this.mapDelineationsToGeoJson(this.delineations);
        this.delineationsLayer = new L.GeoJSON(delineationGeoJson, {
            onEachFeature: (feature, layer) => {
                layer.setStyle(this.delineationDefaultStyle);
                layer.on("click", (e) => {
                    this.selectFeatureImpl(feature.properties.TreatmentBMPID);
                });
            },
        });
        this.delineationsLayer.addTo(this.map);

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                layer.on("click", (e) => {
                    this.selectFeatureImpl(feature.properties.TreatmentBMPID);
                });
            },
        });
        this.treatmentBMPsLayer.addTo(this.map);
        this.setControl();
        this.registerClickEvents();

        // add inventoried BMPs layer
        this.addInventoriedBMPsLayer();

        let tempFeatureGroup = new L.FeatureGroup([this.treatmentBMPsLayer, this.delineationsLayer]);
        this.map.fitBounds(tempFeatureGroup.getBounds(), { padding: new L.Point(50, 50) });
    }

    public setControl(): void {
        this.layerControl = new L.Control.Layers(this.tileLayers, this.overlayLayers, { collapsed: false }).addTo(this.map);
    }

    public registerClickEvents(): void {
        var leafletControlLayersSelector = ".leaflet-control-layers";
        var closeButtonClass = "leaflet-control-layers-close";

        var closem = L.DomUtil.create("a", closeButtonClass);
        closem.innerHTML = "Close";
        L.DomEvent.on(closem, "click", function () {
            $(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");
        });

        $(leafletControlLayersSelector).append(closem);
        $(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");

        this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
            this.selectFeatureImpl(event.propagatedFrom.feature.properties.TreatmentBMPID);
        });
    }

    private addInventoriedBMPsLayer() {
        const inventoriedTreatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs.filter((x) => x.ProjectID == null && x.InventoryIsVerified));
        this.inventoriedTreatmentBMPsLayer = new L.GeoJSON(inventoriedTreatmentBMPGeoJSON, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.inventoriedTreatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                layer.bindPopup(
                    `<b>Name:</b> <a target="_blank" href="${this.ocstBaseUrl()}/TreatmentBMP/Detail/${feature.properties.TreatmentBMPID}">${
                        feature.properties.TreatmentBMPName
                    }</a><br>` + `<b>Type:</b> ${feature.properties.TreatmentBMPTypeName}`
                );
            },
        });

        var clusteredInventoriedBMPLayer = L.markerClusterGroup({
            iconCreateFunction: function (cluster) {
                var childCount = cluster.getChildCount();

                return new L.DivIcon({
                    html: "<div><span>" + childCount + "</span></div>",
                    className: "marker-cluster",
                    iconSize: new L.Point(40, 40),
                });
            },
        });
        clusteredInventoriedBMPLayer.addLayer(this.inventoriedTreatmentBMPsLayer);
        this.layerControl.addOverlay(clusteredInventoriedBMPLayer, this.inventoriedTreatmentBMPOverlayName);
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

    public selectFeatureImpl(treatmentBMPID: number) {
        this.selectedTreatmentBMP = this.treatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
        let hasFlownToSelectedObject = false;
        this.delineationsLayer?.eachLayer((layer) => {
            if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
                layer.setStyle(this.delineationDefaultStyle);
                return;
            }
            layer.setStyle(this.delineationSelectedStyle);
            this.map.flyToBounds(layer.getBounds(), { padding: new L.Point(50, 50) });
            hasFlownToSelectedObject = true;
        });
        this.treatmentBMPsLayer.eachLayer((layer) => {
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
    }

    getDelineationAcreageForTreatmentBMP(treatmentBMPID: number): string {
        let delineation = this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID);
        if (delineation == null) {
            return "Not provided";
        }

        return `${delineation.DelineationArea} ac`;
    }

    triggerModelRunForProject(): void {
        this.projectService.projectsProjectIDModeledPerformancePost(this.projectID).subscribe(
            (results) => {
                this.alertService.pushAlert(new Alert("Model run was successfully started and will run in the background.", AlertContext.Success, true));
                window.scroll(0, 0);
                this.projectService.projectsProjectIDProjectNetworkSolveHistoriesGet(this.projectID).subscribe((result) => {
                    this.projectNetworkSolveHistories = result;
                });
            },
            (error) => {
                window.scroll(0, 0);
                this.cdr.detectChanges();
            }
        );
    }

    showModelResultsPanel(): boolean {
        return this.projectID && this.project?.HasModeledResults;
    }

    getModelResultsLastCalculatedText(): string {
        if (this.projectNetworkSolveHistories == null || this.projectNetworkSolveHistories == undefined || this.projectNetworkSolveHistories.length == 0) {
            return "";
        }

        //These will be ordered by date by the api
        var successfulResults = this.projectNetworkSolveHistories.filter((x) => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded);

        if (successfulResults == null || successfulResults.length == 0) {
            return "";
        }

        return `Results last calculated at ${new Date(successfulResults[0].LastUpdated).toLocaleString()}`;
    }

    isMostRecentHistoryOfType(type: ProjectNetworkSolveHistoryStatusTypeEnum): boolean {
        return (
            this.projectNetworkSolveHistories != null &&
            this.projectNetworkSolveHistories.length > 0 &&
            this.projectNetworkSolveHistories[0].ProjectNetworkSolveHistoryStatusTypeID == type
        );
    }

    continueToNextStep() {
        this.router.navigateByUrl(`/projects/edit/${this.projectID}/attachments`);
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }
}
