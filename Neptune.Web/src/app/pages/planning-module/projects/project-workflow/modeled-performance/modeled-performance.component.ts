import { ApplicationRef, ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { BoundingBoxDto, DelineationUpsertDto, ProjectNetworkSolveHistorySimpleDto, ProjectDto, TreatmentBMPDisplayDto } from "src/app/shared/generated/model/models";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import * as L from "leaflet";
import "leaflet.fullscreen";
import { combineLatest, map, Observable, pipe, switchMap, tap } from "rxjs";
import { environment } from "src/environments/environment";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { ProjectNetworkSolveHistoryStatusTypeEnum } from "src/app/shared/generated/enum/project-network-solve-history-status-type-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { GrantScoresComponent } from "src/app/pages/planning-module/projects/grant-scores/grant-scores.component";
import { ModelResultsComponent } from "src/app/pages/planning-module/projects/model-results/model-results.component";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { NgIf, NgFor, AsyncPipe } from "@angular/common";
import { CustomRichTextComponent } from "src/app/shared/components/custom-rich-text/custom-rich-text.component";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { WorkflowBodyComponent } from "src/app/shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { routeParams } from "src/app/app.routes";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { InventoriedBMPsLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-layer/inventoried-bmps-layer.component";

@Component({
    selector: "modeled-performance",
    templateUrl: "./modeled-performance.component.html",
    styleUrls: ["./modeled-performance.component.scss"],
    standalone: true,
    imports: [
        CustomRichTextComponent,
        NgIf,
        AsyncPipe,
        FieldDefinitionComponent,
        NgFor,
        ModelResultsComponent,
        GrantScoresComponent,
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
export class ModeledPerformanceComponent implements OnInit {
    public ProjectNetworkHistoryStatusTypeEnum = ProjectNetworkSolveHistoryStatusTypeEnum;
    public projectNetworkSolveHistories$: Observable<ProjectNetworkSolveHistorySimpleDto[]>;

    public mapIsReady: boolean = false;

    public delineations: DelineationUpsertDto[];
    public mapHeight: string = "750px";
    public onEachFeatureCallback?: (feature, layer) => void;
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public boundingBox$: Observable<BoundingBoxDto>;
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
    public project$: Observable<ProjectDto>;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampModeledPerformance;

    public projectTreatmentBMPs: Array<TreatmentBMPDisplayDto>;

    constructor(
        private cdr: ChangeDetectorRef,
        private projectService: ProjectService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService,
        private route: ActivatedRoute,
        private router: Router,
        private alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.project$ = this.route.params.pipe(
            switchMap((params) => {
                this.projectID = parseInt(params[routeParams.projectID]);
                return this.projectService.projectsProjectIDGet(this.projectID);
            }),
            pipe(
                tap((project) => {
                    // redirect to review step if project is shared with OCTA grant program
                    if (project.ShareOCTAM2Tier2Scores) {
                        this.router.navigateByUrl(`/planning/projects/edit/${project.ProjectID}/review-and-share`);
                    }
                })
            )
        );
        this.projectNetworkSolveHistories$ = this.project$.pipe(
            switchMap((project) => {
                return combineLatest({
                    TreatmentBMPs: this.projectService.projectsProjectIDTreatmentBmpsGet(project.ProjectID),
                    Delineations: this.projectService.projectsProjectIDDelineationsGet(project.ProjectID),
                    ProjectNetworkSolveHistories: this.projectService.projectsProjectIDProjectNetworkSolveHistoriesGet(project.ProjectID),
                });
            }),
            pipe(
                tap((data) => {
                    if (data.TreatmentBMPs.length == 0) {
                        this.router.navigateByUrl(`/planning/projects/edit/${this.projectID}`);
                    }

                    this.projectTreatmentBMPs = data.TreatmentBMPs;
                    this.delineations = data.Delineations;
                })
            ),
            map((data) => {
                return data.ProjectNetworkSolveHistories;
            })
        );

        this.boundingBox$ = this.project$.pipe(
            switchMap((project) => {
                return this.projectService.projectsProjectIDBoundingBoxGet(project.ProjectID);
            })
        );

        this.compileService.configure(this.appRef);
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.initializeMap();
        this.registerClickEvents();
    }

    public getAboutModelingPerformanceURL(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Home/AboutModelingBMPPerformance`;
    }

    public initializeMap(): void {
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

        let tempFeatureGroup = new L.FeatureGroup([this.treatmentBMPsLayer, this.delineationsLayer]);
        this.map.fitBounds(tempFeatureGroup.getBounds(), { padding: new L.Point(50, 50) });
    }

    public registerClickEvents(): void {
        this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
            this.selectFeatureImpl(event.propagatedFrom.feature.properties.TreatmentBMPID);
        });
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
        this.selectedTreatmentBMP = this.projectTreatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
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
                this.projectNetworkSolveHistories$ = this.projectService.projectsProjectIDProjectNetworkSolveHistoriesGet(this.projectID);
            },
            (error) => {
                window.scroll(0, 0);
                this.cdr.detectChanges();
            }
        );
    }

    getModelResultsLastCalculatedText(projectNetworkSolveHistories: ProjectNetworkSolveHistorySimpleDto[]): string {
        if (projectNetworkSolveHistories == null || projectNetworkSolveHistories == undefined || projectNetworkSolveHistories.length == 0) {
            return "";
        }

        //These will be ordered by date by the api
        var successfulResults = projectNetworkSolveHistories.filter((x) => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded);

        if (successfulResults == null || successfulResults.length == 0) {
            return "";
        }

        return `Results last calculated at ${new Date(successfulResults[0].LastUpdated).toLocaleString()}`;
    }

    isMostRecentHistoryOfType(projectNetworkSolveHistories: ProjectNetworkSolveHistorySimpleDto[], type: ProjectNetworkSolveHistoryStatusTypeEnum): boolean {
        return projectNetworkSolveHistories != null && projectNetworkSolveHistories.length > 0 && projectNetworkSolveHistories[0].ProjectNetworkSolveHistoryStatusTypeID == type;
    }

    continueToNextStep() {
        this.router.navigateByUrl(`/planning/projects/edit/${this.projectID}/attachments`);
    }
}
