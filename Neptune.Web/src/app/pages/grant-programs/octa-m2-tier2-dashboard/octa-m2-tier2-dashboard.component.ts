import { ApplicationRef, ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { combineLatest, Observable, tap } from "rxjs";
import * as L from "leaflet";
import "leaflet-gesture-handling";
import "leaflet.fullscreen";
import "leaflet-loading";
import { AuthenticationService } from "src/app/services/authentication.service";
import { BoundingBoxDto, DelineationDto, ProjectDto, TreatmentBMPDisplayDto } from "src/app/shared/generated/model/models";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import { environment } from "src/environments/environment";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { PrioritizationMetric } from "src/app/shared/models/prioritization-metric";
import { WfsService } from "src/app/shared/services/wfs.service";
import { OctaPrioritizationDetailPopupComponent } from "src/app/shared/components/octa-prioritization-detail-popup/octa-prioritization-detail-popup.component";
import { ColDef, GridApi, GridReadyEvent } from "ag-grid-community";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { AgGridModule } from "ag-grid-angular";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { RouterLink } from "@angular/router";
import { FieldDefinitionComponent } from "../../../shared/components/field-definition/field-definition.component";
import { NgIf, NgSwitch, NgSwitchCase, NgSwitchDefault, NgFor, AsyncPipe } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { NgSelectModule } from "@ng-select/ng-select";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";
import { NeptuneGridComponent } from "../../../shared/components/neptune-grid/neptune-grid.component";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";
import { IconComponent } from "../../../shared/components/icon/icon.component";
import { ExpandCollapseDirective } from "src/app/shared/directives/expand-collapse.directive";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";

declare var $: any;

@Component({
    selector: "octa-m2-tier2-dashboard",
    templateUrl: "./octa-m2-tier2-dashboard.component.html",
    styleUrls: ["./octa-m2-tier2-dashboard.component.scss"],
    standalone: true,
    imports: [
        AlertDisplayComponent,
        NgSelectModule,
        FormsModule,
        NgIf,
        NgSwitch,
        NgSwitchCase,
        FieldDefinitionComponent,
        NgSwitchDefault,
        NgFor,
        RouterLink,
        AgGridModule,
        NeptuneGridComponent,
        PageHeaderComponent,
        IconComponent,
        ExpandCollapseDirective,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        DelineationsLayerComponent,
        JurisdictionsLayerComponent,
        WqmpsLayerComponent,
        StormwaterNetworkLayerComponent,
        AsyncPipe,
    ],
})
export class OCTAM2Tier2DashboardComponent implements OnInit {
    public mapIsReady: boolean = false;
    public customRichTextTypeID = NeptunePageTypeEnum.OCTAM2Tier2GrantProgramDashboard;

    public octaM2Tier2MapInitData$: Observable<OCTAM2Tier2MapInitData>;
    public projects: Array<ProjectDto>;
    private treatmentBMPs: Array<TreatmentBMPDisplayDto>;
    private delineations: Array<DelineationDto>;
    public selectedTreatmentBMP: TreatmentBMPDisplayDto;
    public relatedTreatmentBMPs: Array<TreatmentBMPDisplayDto>;
    public relateedTreatmentBMPsToDisplay: Array<TreatmentBMPDisplayDto>;
    public selectedDelineation: DelineationDto;
    public selectedProject: ProjectDto;
    public gridApi: GridApi;

    public mapHeight = window.innerHeight - window.innerHeight * 0.2 + "px";
    public map: L.Map;
    public plannedProjectTreatmentBMPsLayer: L.GeoJSON<any>;
    public inventoriedTreatmentBMPsLayer: L.GeoJSON<any>;
    public selectedProjectDelineationsLayer: L.GeoJSON<any>;
    private boundingBox: BoundingBoxDto;
    public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
    public layerControl: L.Control.Layers;

    //this is needed to allow binding to the static class
    public PrioritizationMetric = PrioritizationMetric;
    public prioritizationMetrics = Object.values(PrioritizationMetric);
    public selectedPrioritizationMetric = PrioritizationMetric.NoMetric;
    public prioritizationMetricOverlayLayer: L.Layers;

    private plannedTreatmentBMPOverlayName = "<img src='./assets/main/map-icons/marker-icon-violet.png' style='height:17px; margin-bottom:3px'> Project BMPs";
    private inventoriedTreatmentBMPOverlayName = "<img src='./assets/main/map-icons/marker-icon-orange.png' style='height:17px; margin-bottom:3px'> Inventoried BMPs (Verified)";
    private delineationDefaultStyle = {
        color: "#51F6F8",
        fillOpacity: 0.2,
        opacity: 1,
    };

    private viewInitialized: boolean = false;

    public columnDefs: ColDef[];
    public paginationPageSize: number = 100;

    constructor(
        private authenticationService: AuthenticationService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private cdr: ChangeDetectorRef,
        private projectService: ProjectService,
        private treatmentBMPService: TreatmentBMPService,
        private wfsService: WfsService,
        private utilityFunctionsService: UtilityFunctionsService,
        private alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Project Name", "ProjectName", "ProjectID", {
                InRouterLink: "/projects/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Project Description", "ProjectDescription"),
            this.utilityFunctionsService.createBasicColumnDef("Organization", "Organization.OrganizationName"),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdiction.Organization.OrganizationName"),
            this.utilityFunctionsService.createDateColumnDef("Last Shared On", "OCTAM2Tier2ScoresLastSharedDate", "short", { Width: 140 }),
            this.utilityFunctionsService.createDecimalColumnDef("Area Treated (ac)", "Area"),
            this.utilityFunctionsService.createDecimalColumnDef("Impervious Area Treated (ac)", "ImperviousArea", { Width: 220 }),
            this.utilityFunctionsService.createDecimalColumnDef("SEA Score", "SEA", { Width: 90 }),
            this.utilityFunctionsService.createDecimalColumnDef("TPI Score", "TPI", { Width: 90 }),
            this.utilityFunctionsService.createDecimalColumnDef("Dry Weather WQLRI", "DryWeatherWQLRI"),
            this.utilityFunctionsService.createDecimalColumnDef("Wet Weather WQLRI", "WetWeatherWQLRI"),
        ];

        this.compileService.configure(this.appRef);
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;

        this.registerClickEvents();

        this.map.on("overlayadd overlayremove", (e) => {
            if (e.name != this.plannedTreatmentBMPOverlayName) {
                return;
            }
            if (e.type == "overlayremove") {
                this.plannedProjectTreatmentBMPsLayer.eachLayer((layer) => {
                    layer.disablePermanentHighlight();
                });
            }
        });

        this.octaM2Tier2MapInitData$ = combineLatest({
            BoundingBox: this.stormwaterJurisdictionService.jurisdictionsBoundingBoxGet(),
            Projects: this.projectService.projectsOCTAM2Tier2GrantProgramGet(),
            TreatmentBMPs: this.treatmentBMPService.treatmentBMPsGet(),
            Delineations: this.projectService.projectsDelineationsGet(),
        }).pipe(
            tap((data) => {
                this.projects = data.Projects;
                this.treatmentBMPs = data.TreatmentBMPs;
                this.delineations = data.Delineations;
                this.boundingBox = data.BoundingBox;
                this.initializePanes();
                this.map.fitBounds(
                    [
                        [this.boundingBox.Bottom, this.boundingBox.Left],
                        [this.boundingBox.Top, this.boundingBox.Right],
                    ],
                    this.defaultFitBoundsOptions
                );

                this.addTreatmentBMPLayersToMap();
            })
        );
    }

    public initializePanes(): void {
        let hippocampChoroplethPane = this.map.createPane("hippocampChoroplethPane");
        hippocampChoroplethPane.style.zIndex = 300;
    }

    public registerClickEvents(): void {
        const wfsService = this.wfsService;
        const self = this;
        this.map.on("click", (event: L.LeafletMouseEvent): void => {
            wfsService.getOCTAPrioritizationMetricsByCoordinate(event.latlng.lng, event.latlng.lat).subscribe((octaPrioritizationFeatureCollection: L.FeatureCollection) => {
                octaPrioritizationFeatureCollection.features.forEach((feature: L.Feature) => {
                    new L.Popup({
                        minWidth: 500,
                        autoPanPadding: new L.Point(100, 100),
                    })
                        .setLatLng(event.latlng)
                        .setContent(
                            this.compileService.compile(OctaPrioritizationDetailPopupComponent, (c) => {
                                c.instance.feature = feature;
                            })
                        )
                        .openOn(self.map);
                });
            });
        });
    }

    public addTreatmentBMPLayersToMap(): void {
        if (this.plannedProjectTreatmentBMPsLayer) {
            this.map.removeLayer(this.plannedProjectTreatmentBMPsLayer);
            this.layerControl.removeLayer(this.plannedProjectTreatmentBMPsLayer);
        }
        if (this.inventoriedTreatmentBMPsLayer) {
            this.map.removeLayer(this.inventoriedTreatmentBMPsLayer);
            this.layerControl.removeLayer(this.inventoriedTreatmentBMPsLayer);
        }

        // add inventoried BMPs layer
        this.addInventoriedBMPsLayer();

        // add planned project BMPs layer
        const projectIDs = this.projects.map((x) => x.ProjectID);
        const projectTreatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs.filter((x) => projectIDs.includes(x.ProjectID)));
        this.plannedProjectTreatmentBMPsLayer = new L.GeoJSON(projectTreatmentBMPGeoJSON, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                layer.on("click", (e) => {
                    this.selectTreatmentBMPImpl(feature.properties.TreatmentBMPID);
                });
            },
        });
        this.plannedProjectTreatmentBMPsLayer.addTo(this.map);
        this.layerControl.addOverlay(this.plannedProjectTreatmentBMPsLayer, this.plannedTreatmentBMPOverlayName);

        this.map.fireEvent("dataload");
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

    private mapDelineationsToGeoJson(delineations: DelineationDto[]) {
        return delineations.map((x) => JSON.parse(x.Geometry));
    }

    public selectTreatmentBMPImpl(treatmentBMPID: number) {
        let selectedTreatmentBMP = this.treatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
        this.selectProjectImpl(selectedTreatmentBMP.ProjectID);
        this.selectedTreatmentBMP = selectedTreatmentBMP;
        this.plannedProjectTreatmentBMPsLayer.eachLayer((layer) => {
            if (!layer.feature.properties.DefaultZIndexOffset) {
                layer.feature.properties.DefaultZIndexOffset = layer._zIndex;
            }
            //Doing this here as well feels redundant, but if we dont
            //whenever we set the icon it puts the highlight in a weird state.
            //So just disable and enable as needed
            layer.disablePermanentHighlight();
            if (this.selectedTreatmentBMP == null || treatmentBMPID != layer.feature.properties.TreatmentBMPID) {
                layer.setIcon(MarkerHelper.treatmentBMPMarker);
                layer.setZIndexOffset(layer.feature.properties.DefaultZIndexOffset);
                if (this.relatedTreatmentBMPs.some((x) => x.TreatmentBMPID == layer.feature.properties.TreatmentBMPID)) {
                    layer.enablePermanentHighlight();
                }
                return;
            }
            layer.setZIndexOffset(10000);
            layer.setIcon(MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-red.png"));
        });
        this.selectedDelineation = this.delineations.find((x) => x.TreatmentBMPID == treatmentBMPID);
    }

    public selectProjectImpl(projectID: number) {
        if (this.selectedProjectDelineationsLayer) {
            this.map.removeLayer(this.selectedProjectDelineationsLayer);
            this.selectedProjectDelineationsLayer = null;
        }

        this.selectedDelineation = null;
        this.selectedTreatmentBMP = null;

        this.selectedProject = this.projects.find((x) => x.ProjectID == projectID);
        this.relatedTreatmentBMPs = this.treatmentBMPs.filter((x) => x.ProjectID == projectID);
        let relatedTreatmentBMPIDs = this.relatedTreatmentBMPs.map((x) => x.TreatmentBMPID);
        let featureGroupForZoom = new L.featureGroup();

        let relatedDelineations = this.delineations.filter((x) => relatedTreatmentBMPIDs.includes(x.TreatmentBMPID));
        if (relatedDelineations != null && relatedDelineations.length > 0) {
            this.selectedProjectDelineationsLayer = new L.GeoJSON(this.mapDelineationsToGeoJson(relatedDelineations), {
                style: (feature) => {
                    return this.delineationDefaultStyle;
                },
            });
            this.selectedProjectDelineationsLayer.addTo(this.map);
            this.selectedProjectDelineationsLayer.addTo(featureGroupForZoom);
        }

        this.plannedProjectTreatmentBMPsLayer.eachLayer((layer) => {
            layer.disablePermanentHighlight();
            layer.setIcon(MarkerHelper.treatmentBMPMarker);
            if (relatedTreatmentBMPIDs.includes(layer.feature.properties.TreatmentBMPID)) {
                layer.addTo(featureGroupForZoom);
                layer.enablePermanentHighlight();
            }
        });
        this.map.fitBounds(featureGroupForZoom.getBounds(), { padding: new L.Point(50, 50) });

        this.gridApi.forEachNode((node) => {
            if (node.data.ProjectID === projectID) {
                node.setSelected(true);
                var rowIndex = node.rowIndex;
                //I am honestly kind of flabbergasted that ag-grid doesn't tell me what page the node is on
                this.gridApi.paginationGoToPage(Math.floor(rowIndex / this.paginationPageSize));
                this.gridApi.ensureIndexVisible(node.rowIndex);
            }
        });
    }

    public applyPrioritizationMetricOverlay(): void {
        if (!this.map) {
            return null;
        }

        if (this.prioritizationMetricOverlayLayer) {
            this.map.removeLayer(this.prioritizationMetricOverlayLayer);
            this.prioritizationMetricOverlayLayer = null;
        }

        if (this.selectedPrioritizationMetric == PrioritizationMetric.NoMetric) {
            return null;
        }

        let prioritizationMetricsWMSOptions = {
            layers: "Neptune:OCTAPrioritization",
            transparent: true,
            format: "image/png",
            tiled: true,
            styles: this.selectedPrioritizationMetric.geoserverStyle,
            pane: "hippocampChoroplethPane",
        } as L.WMSOptions;

        this.prioritizationMetricOverlayLayer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", prioritizationMetricsWMSOptions);
        this.prioritizationMetricOverlayLayer.addTo(this.map);
    }

    public onSelectionChanged() {
        const selectedNode = this.gridApi.getSelectedNodes()[0];
        // If we have no selected node or our node has already been selected so we can stop infinite looping
        if (!selectedNode || (this.selectedProject != null && this.selectedProject.ProjectID == selectedNode.data.ProjectID)) {
            return;
        }
        let treatmentBMPID = this.treatmentBMPs.filter((x) => x.ProjectID == selectedNode.data.ProjectID).map((x) => x.TreatmentBMPID)[0];

        if (treatmentBMPID != null) {
            this.selectTreatmentBMPImpl(treatmentBMPID);
            return;
        }

        this.selectProjectImpl(selectedNode.data.ProjectID);
    }

    public getRelatedBMPsToShow() {
        let selectedTreatmentBMPID = this.selectedTreatmentBMP?.TreatmentBMPID;
        return this.relatedTreatmentBMPs.filter((x) => x.TreatmentBMPID != selectedTreatmentBMPID);
    }

    public downloadProjectModelResults() {
        this.projectService.projectsOCTAM2Tier2GrantProgramDownloadGet().subscribe(
            (csv) => {
                //Create a fake object for us to click and download
                var a = document.createElement("a");
                a.href = URL.createObjectURL(csv);
                a.download = `OCTA-M2-Tier2-project-modeled-results.csv`;
                document.body.appendChild(a);
                a.click();
                //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
                URL.revokeObjectURL(a.href);
                document.body.removeChild(a);
            },
            () => {
                this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
            }
        );
    }

    public downloadTreatmentBMPModelResults() {
        this.projectService.projectsOCTAM2Tier2GrantProgramTreatmentBMPsDownloadGet().subscribe(
            (csv) => {
                //Create a fake object for us to click and download
                var a = document.createElement("a");
                a.href = URL.createObjectURL(csv);
                a.download = `OCTA-M2-Tier2-BMP-modeled-results.csv`;
                document.body.appendChild(a);
                a.click();
                //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
                URL.revokeObjectURL(a.href);
                document.body.removeChild(a);
            },
            () => {
                this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
            }
        );
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }

    public onGridReady(event: GridReadyEvent) {
        this.gridApi = event.api;
    }
}

export interface OCTAM2Tier2MapInitData {
    TreatmentBMPs: Array<TreatmentBMPDisplayDto>;
    Delineations: Array<DelineationDto>;
    Projects: Array<ProjectDto>;
    BoundingBox: BoundingBoxDto;
}
