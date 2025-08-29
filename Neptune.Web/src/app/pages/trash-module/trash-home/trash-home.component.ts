import { Component, OnInit, OnDestroy, ChangeDetectorRef } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { RoleEnum } from "src/app/shared/generated/enum/role-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { CustomRichTextComponent } from "src/app/shared/components/custom-rich-text/custom-rich-text.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { AsyncPipe, DatePipe, DecimalPipe } from "@angular/common";
import { BehaviorSubject, Observable, switchMap, tap } from "rxjs";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import "leaflet-draw";
import "leaflet.fullscreen";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { LandUseBlockLayerComponent } from "src/app/shared/components/leaflet/layers/land-use-block-layer/land-use-block-layer.component";
import { TrashGeneratingUnitLayerComponent } from "src/app/shared/components/leaflet/layers/trash-generating-unit-layer/trash-generating-unit-layer.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { AreaBasedAcreCalculationsDto } from "src/app/shared/generated/model/area-based-acre-calculations-dto";
import { LoadResultsDto } from "src/app/shared/generated/model/load-results-dto";
import { OVTAResultsDto } from "src/app/shared/generated/model/ovta-results-dto";
import { LeafletHelperService } from "src/app/shared/services/leaflet-helper.service";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { IFeature, StormwaterJurisdictionDto, TrashGeneratingUnitDto } from "src/app/shared/generated/model/models";
import { WqmpsTrashCaptureLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-trash-capture-layer/wqmps-trash-capture-layer.component";
import { OvtaAreasLayerComponent } from "src/app/shared/components/leaflet/layers/ovta-areas-layer/ovta-areas-layer.component";
import { TrashGeneratingUnitLoadsLayerComponent } from "src/app/shared/components/leaflet/layers/trash-generating-unit-loads-layer/trash-generating-unit-loads-layer.component";
import { TrashGeneratingUnitByStormwaterJurisdictionService } from "src/app/shared/generated/api/trash-generating-unit-by-stormwater-jurisdiction.service";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { WfsService } from "src/app/shared/services/wfs.service";
import { TrashGeneratingUnitService } from "src/app/shared/generated/api/trash-generating-unit.service";
import { ModalService, ModalSizeEnum, ModalThemeEnum } from "src/app/shared/services/modal/modal.service";
import { ScoreDescriptionsComponent } from "../ovtas/score-descriptions/score-descriptions.component";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TrashCaptureStatusTypeEnum } from "src/app/shared/generated/enum/trash-capture-status-type-enum";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { PermitTypeLayerComponent } from "src/app/shared/components/leaflet/layers/permit-type-layer/permit-type-layer.component";

@Component({
    selector: "trash-home",
    templateUrl: "./trash-home.component.html",
    styleUrls: ["./trash-home.component.scss"],
    imports: [
        AlertDisplayComponent,
        CustomRichTextComponent,
        AsyncPipe,
        RouterLink,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        DelineationsLayerComponent,
        JurisdictionsLayerComponent,
        WqmpsTrashCaptureLayerComponent,
        LandUseBlockLayerComponent,
        PermitTypeLayerComponent,
        TrashGeneratingUnitLayerComponent,
        TrashGeneratingUnitLoadsLayerComponent,
        OvtaAreasLayerComponent,
        NgSelectModule,
        FormsModule,
        ReactiveFormsModule,
        FieldDefinitionComponent,
        DecimalPipe,
        DatePipe,
        LoadingDirective,
    ],
})
export class TrashHomeComponent implements OnInit, OnDestroy {
    public watchUserChangeSubscription: any;
    public currentUser$: Observable<PersonDto>;

    public richTextTypeID: number = NeptunePageTypeEnum.TrashHomePage;

    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;

    public stormwaterJurisdictions$: Observable<Array<StormwaterJurisdictionDto>>;
    public currentStormwaterJurisdiction: StormwaterJurisdictionDto;
    private stormwaterJurisdictionSubject = new BehaviorSubject<StormwaterJurisdictionDto | null>(null);
    public stormwaterJurisdiction$ = this.stormwaterJurisdictionSubject.asObservable();

    public selectedStormwaterJurisdictionLayer: L.GeoJSON<any>;
    private selectedJurisdictionStyle = {
        color: "#FF6C2D",
        weight: 5,
        fill: false,
    };

    public currentResultType: string = "Area-Based Results";
    public resultTypes = ["Area-Based Results", "Load-Based Results (Current)", "Load-Based (Net Change)", "OVTA-Based Results", "No Metric, Map Overlay"];

    public areaBasedAcreCalculationsDto$: Observable<AreaBasedAcreCalculationsDto>;
    public loadResultsDto$: Observable<LoadResultsDto>;
    public ovtaResultsDto$: Observable<OVTAResultsDto>;
    public boundingBox$: Observable<BoundingBoxDto>;

    public isLoading: boolean;

    public tguDto$: Observable<TrashGeneratingUnitDto>;
    public tguLayer: L.GeoJSON<any>;
    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    public lastUpdateDate$: Observable<string>;

    public treatmentBMPClusterLayer: L.markerClusterGroup = L.markerClusterGroup({
        iconCreateFunction: function (cluster) {
            var childCount = cluster.getChildCount();

            return new L.DivIcon({
                html: "<div><span>" + childCount + "</span></div>",
                className: "treatment-bmp-cluster",
                iconSize: new L.Point(40, 40),
            });
        },
    });

    public treatmentBMPs$: Observable<IFeature[]>;

    constructor(
        private authenticationService: AuthenticationService,
        private router: Router,
        private route: ActivatedRoute,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private trashResultsByJurisdictionService: TrashGeneratingUnitByStormwaterJurisdictionService,
        private leafletHelperService: LeafletHelperService,
        private wfsService: WfsService,
        private trashGeneratingUnitService: TrashGeneratingUnitService,
        private treatmentBMPService: TreatmentBMPService,
        private modalService: ModalService,
        private cdr: ChangeDetectorRef
    ) {}

    public ngOnInit(): void {
        this.currentUser$ = this.authenticationService.getCurrentUser();

        this.route.queryParams.subscribe((params) => {
            //We're logging in
            if (params.hasOwnProperty("code")) {
                this.router.navigate(["/signin-oidc"], { queryParams: params });
                return;
            }

            if (localStorage.getItem("loginOnReturn")) {
                localStorage.removeItem("loginOnReturn");
                this.authenticationService.login();
            }

            //We were forced to logout or were sent a link and just finished logging in
            if (sessionStorage.getItem("authRedirectUrl")) {
                this.router.navigateByUrl(sessionStorage.getItem("authRedirectUrl")).then(() => {
                    sessionStorage.removeItem("authRedirectUrl");
                });
            }
        });

        this.stormwaterJurisdictions$ = this.stormwaterJurisdictionService.jurisdictionsUserViewableGet().pipe(
            tap((x) => {
                this.stormwaterJurisdictionSubject.next(x[0]);
                this.currentStormwaterJurisdiction = x[0];
            })
        );

        this.lastUpdateDate$ = this.trashGeneratingUnitService.trashGeneratingUnitsLastUpdateDateGet();

        this.areaBasedAcreCalculationsDto$ = this.stormwaterJurisdiction$.pipe(
            tap(() => {
                this.isLoading = true;
            }),
            switchMap((x) => {
                return this.trashResultsByJurisdictionService.trashResultsByJurisdictionJurisdictionIDAreaBasedResultsCalculationsGet(x.StormwaterJurisdictionID);
            }),
            tap(() => {
                this.isLoading = false;
            })
        );

        this.loadResultsDto$ = this.stormwaterJurisdiction$.pipe(
            tap(() => {
                this.isLoading = true;
            }),
            switchMap((x) => {
                return this.trashResultsByJurisdictionService.trashResultsByJurisdictionJurisdictionIDLoadBasedResultsCalculationsGet(x.StormwaterJurisdictionID);
            }),
            tap(() => {
                this.isLoading = false;
            })
        );

        this.ovtaResultsDto$ = this.stormwaterJurisdiction$.pipe(
            tap(() => {
                this.isLoading = true;
            }),
            switchMap((x) => {
                return this.trashResultsByJurisdictionService.trashResultsByJurisdictionJurisdictionIDOvtaBasedResultsCalculationsGet(x.StormwaterJurisdictionID);
            }),
            tap(() => {
                this.isLoading = false;
            })
        );

        this.boundingBox$ = this.stormwaterJurisdiction$.pipe(
            tap((x) => {
                this.addSelectedJurisdictionLayer(x.StormwaterJurisdictionID);
            }),
            switchMap((x) => {
                return this.stormwaterJurisdictionService.jurisdictionsJurisdictionIDBoundingBoxGet(x.StormwaterJurisdictionID);
            }),
            tap((boundingBox) => {
                if (this.mapIsReady) {
                    this.leafletHelperService.fitMapToBoundingBox(this.map, boundingBox);
                }
            })
        );

        this.treatmentBMPs$ = this.stormwaterJurisdiction$.pipe(
            switchMap((x) => {
                return this.treatmentBMPService.treatmentBmpsJurisdictionsJurisdictionIDVerifiedFeatureCollectionGet(x.StormwaterJurisdictionID);
            }),
            tap((treatmentBMPs) => {
                var isCurrentlyOn = this.map.hasLayer(this.treatmentBMPClusterLayer);

                if (this.treatmentBMPClusterLayer) {
                    this.treatmentBMPClusterLayer.clearLayers();
                    this.map.removeLayer(this.treatmentBMPClusterLayer);
                    this.layerControl.removeLayer(this.treatmentBMPClusterLayer);
                }

                const inventoriedTreatmentBMPsLayer = new L.GeoJSON(treatmentBMPs, {
                    pointToLayer: (feature, latlng) => {
                        var iconSrc = "./assets/main/map-icons/marker-icon-orange.png";
                        switch (feature.properties["TrashCaptureStatusTypeID"]) {
                            case TrashCaptureStatusTypeEnum.Full:
                                iconSrc = "./assets/main/map-icons/marker-icon-red.png";
                                break;
                            case TrashCaptureStatusTypeEnum.Partial:
                                iconSrc = "./assets/main/map-icons/marker-icon-blue.png";
                                break;
                            case TrashCaptureStatusTypeEnum.None:
                                iconSrc = "./assets/main/map-icons/marker-icon-black.png";
                                break;
                            case TrashCaptureStatusTypeEnum.NotProvided:
                                iconSrc = "./assets/main/map-icons/marker-icon-gray.png";
                                break;
                            default:
                                iconSrc = "./assets/main/map-icons/marker-icon-orange.png";
                                break;
                        }
                        return L.marker(latlng, { icon: MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath(iconSrc) });
                    },
                    onEachFeature: (feature, layer) => {
                        layer.bindPopup(
                            `<b>Name:</b> <a target="_blank" href="${this.ocstBaseUrl()}/TreatmentBMP/Detail/${feature.properties.TreatmentBMPID}">${
                                feature.properties.TreatmentBMPName
                            }</a><br>` + `<b>Type:</b> ${feature.properties.TreatmentBMPTypeName}`
                        );
                    },
                });
                this.treatmentBMPClusterLayer.addLayer(inventoriedTreatmentBMPsLayer);
                this.treatmentBMPClusterLayer["legendHtml"] = "<img src='/assets/main/map-legend-images/bmpTrashCaptureLegend.png' />";
                this.layerControl.addOverlay(this.treatmentBMPClusterLayer, "BMPs");
                if (isCurrentlyOn) {
                    this.map.addLayer(this.treatmentBMPClusterLayer);
                }
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;

        this.registerClickEvents();
        this.addSelectedJurisdictionLayer(this.currentStormwaterJurisdiction.StormwaterJurisdictionID);
    }

    public onJurisdictionSelected(selectedJurisdiction: StormwaterJurisdictionDto) {
        this.stormwaterJurisdictionSubject.next(selectedJurisdiction);
        this.currentStormwaterJurisdiction = selectedJurisdiction;
        this.cdr.detectChanges();
    }

    public registerClickEvents(): void {
        const wfsService = this.wfsService;
        const self = this;
        this.map.on("click", (event: L.LeafletMouseEvent): void => {
            wfsService.getTrashGeneratingUnitByCoordinate(event.latlng.lng, event.latlng.lat).subscribe((tguFeatureCollection: L.FeatureCollection) => {
                if (tguFeatureCollection.features.length == 0) {
                    this.tguDto$ = null;
                    if (this.tguLayer) {
                        this.map.removeLayer(this.tguLayer);
                    }
                }
                var featuresInRenderedOrder = tguFeatureCollection.features
                    .sort((a, b) => {
                        if (a.properties.IsPriorityLandUse > b.properties.IsPriorityLandUse) {
                            return 1;
                        }
                        if (b.properties.IsPriorityLandUse > a.properties.IsPriorityLandUse) {
                            return -1;
                        }
                        return 0;
                    })
                    .sort((a, b) => {
                        // sort by AssessmentScore descending
                        if (a.properties.AssessmentScore < b.properties.AssessmentScore) {
                            return 1;
                        }
                        if (b.properties.AssessmentScore < a.properties.AssessmentScore) {
                            return -1;
                        }
                        return 0;
                    })
                    .sort((a, b) => {
                        // sort by TrashCaptureStatusSortOrder descending
                        if (a.properties.TrashCaptureStatusSortOrder < b.properties.TrashCaptureStatusSortOrder) {
                            return 1;
                        }
                        if (b.properties.TrashCaptureStatusSortOrder < a.properties.TrashCaptureStatusSortOrder) {
                            return -1;
                        }
                        return 0;
                    });

                featuresInRenderedOrder.forEach((feature: L.Feature) => {
                    this.tguDto$ = this.trashGeneratingUnitService.trashGeneratingUnitsTrashGeneratingUnitIDGet(feature.properties.TrashGeneratingUnitID);
                    const geoJson = L.geoJSON(feature, {
                        style: this.highlightStyle,
                    });
                    if (this.tguLayer) {
                        this.map.removeLayer(this.tguLayer);
                    }
                    this.tguLayer = L.geoJSON(feature, {
                        style: this.highlightStyle,
                    });
                    //this.map.fitBounds(this.tguLayer.getBounds());
                    this.tguLayer.addTo(this.map);
                });
            });
        });
    }

    public addSelectedJurisdictionLayer(stormwaterJurisdictionID: number) {
        this.wfsService
            .getGeoserverWFSLayerWithCQLFilter("OCStormwater:Jurisdictions", `StormwaterJurisdictionID = ${stormwaterJurisdictionID}`, "StormwaterJurisdictionID")
            .subscribe((response) => {
                if (this.mapIsReady) {
                    if (this.selectedStormwaterJurisdictionLayer) {
                        this.map.removeLayer(this.selectedStormwaterJurisdictionLayer);
                    }
                    this.selectedStormwaterJurisdictionLayer = L.geoJSON(response, { style: this.selectedJurisdictionStyle });
                    this.selectedStormwaterJurisdictionLayer.addTo(this.map);
                }
            });
    }

    ngOnDestroy(): void {
        this.watchUserChangeSubscription?.unsubscribe();
    }

    public userIsUnassigned(currentUser: PersonDto) {
        if (!currentUser) {
            return false; // doesn't exist != unassigned
        }

        return currentUser.RoleID === RoleEnum.Unassigned;
    }

    public userIsOCTAGrantReviewer(currentUser: PersonDto) {
        if (!currentUser) {
            return false;
        }

        return currentUser.IsOCTAGrantReviewer;
    }

    public isUserAnAdministrator(currentUser: PersonDto) {
        return this.authenticationService.isUserAnAdministrator(currentUser);
    }

    public login(): void {
        this.authenticationService.login(true);
    }

    public createAccount(): void {
        this.authenticationService.createAccount();
    }

    public forgotPasswordUrl(): string {
        return `${environment.keystoneAuthConfiguration.issuer}/Account/ForgotPassword?${this.authenticationService.getClientIDAndRedirectUrlForKeystone()}`;
    }

    public forgotUsernameUrl(): string {
        return `${environment.keystoneAuthConfiguration.issuer}/Account/ForgotUsername?${this.authenticationService.getClientIDAndRedirectUrlForKeystone()}`;
    }

    public keystoneSupportUrl(): string {
        return `${environment.keystoneAuthConfiguration.issuer}/Account/Support/20?${this.authenticationService.getClientIDAndRedirectUrlForKeystone()}`;
    }

    public requestSupportUrl(): string {
        return `${this.ocstBaseUrl()}/Help/Support`;
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }

    showScoreDefinitions() {
        this.modalService.open(ScoreDescriptionsComponent, null, { CloseOnClickOut: false, TopLayer: false, ModalSize: ModalSizeEnum.Large, ModalTheme: ModalThemeEnum.Light });
    }
}
