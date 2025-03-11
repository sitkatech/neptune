import { Component, OnInit, OnDestroy } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { RoleEnum } from "src/app/shared/generated/enum/role-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { CustomRichTextComponent } from "src/app/shared/components/custom-rich-text/custom-rich-text.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { AsyncPipe, DecimalPipe, NgIf } from "@angular/common";
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
import { FormsModule, ReactiveFormsModule  } from "@angular/forms";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { SelectDropDownModule } from "ngx-select-dropdown";
import { AreaBasedAcreCalculationsDto } from "src/app/shared/generated/model/area-based-acre-calculations-dto";
import { LoadResultsDto } from "src/app/shared/generated/model/load-results-dto";
import { OVTAResultsDto } from "src/app/shared/generated/model/ovta-results-dto";
import { LeafletHelperService } from "src/app/shared/services/leaflet-helper.service";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { StormwaterJurisdictionDto } from "src/app/shared/generated/model/models";
import { InventoriedBMPsTrashCaptureLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-trash-capture-layer/inventoried-bmps-trash-capture-layer.component";
import { WqmpsTrashCaptureLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-trash-capture-layer/wqmps-trash-capture-layer.component";
import { OvtaAreaLayerComponent } from "src/app/shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { TrashGeneratingUnitLoadsLayerComponent } from "src/app/shared/components/leaflet/layers/trash-generating-unit-loads-layer/trash-generating-unit-loads-layer.component";
import { TrashGeneratingUnitByStormwaterJurisdictionService } from "src/app/shared/generated/api/trash-generating-unit-by-stormwater-jurisdiction.service";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";

@Component({
    selector: "trash-home",
    templateUrl: "./trash-home.component.html",
    styleUrls: ["./trash-home.component.scss"],
    standalone: true,
    imports: [NgIf, AlertDisplayComponent, CustomRichTextComponent, AsyncPipe, RouterLink, NeptuneMapComponent,
    RegionalSubbasinsLayerComponent,
    DelineationsLayerComponent,
    JurisdictionsLayerComponent,
    InventoriedBMPsTrashCaptureLayerComponent,
    WqmpsTrashCaptureLayerComponent,
    LandUseBlockLayerComponent,
    TrashGeneratingUnitLayerComponent,
    TrashGeneratingUnitLoadsLayerComponent,
    OvtaAreaLayerComponent,
    NgSelectModule,
    FormsModule, ReactiveFormsModule,
    FieldDefinitionComponent,
    SelectDropDownModule,
    DecimalPipe,
    LoadingDirective],
})
export class TrashHomeComponent implements OnInit, OnDestroy {
    public watchUserChangeSubscription: any;
    public currentUser$: Observable<PersonDto>;

    public richTextTypeID: number = NeptunePageTypeEnum.TrashHomePage;

    public mapHeight = window.innerHeight - window.innerHeight * 0.2 + "px";
    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;

    public stormwaterJurisdictions$: Observable<Array<StormwaterJurisdictionDto>>;
    public currentStormwaterJurisdiction: StormwaterJurisdictionDto;
    private stormwaterJurisdictionSubject = new BehaviorSubject<StormwaterJurisdictionDto | null>(null);
    public stormwaterJurisdiction$ = this.stormwaterJurisdictionSubject.asObservable();

    public currentResultType: string = "Area-Based Results";
    public resultTypes = ["Area-Based Results" , "Load-Based Results (Current)", "Load-Based (Net Change)", "OVTA-Based Results" , "No Metric, Map Overlay"  ];

    public areaBasedAcreCalculationsDto$: Observable<AreaBasedAcreCalculationsDto>
    public loadResultsDto$: Observable<LoadResultsDto>
    public ovtaResultsDto$: Observable<OVTAResultsDto>
    public boundingBox$: Observable<BoundingBoxDto>
    
    public isLoading: boolean

    constructor(
        private authenticationService: AuthenticationService, 
        private router: Router, 
        private route: ActivatedRoute,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private trashResultsByJurisdictionService: TrashGeneratingUnitByStormwaterJurisdictionService,
        private leafletHelperService: LeafletHelperService

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
                this.stormwaterJurisdictionSubject.next(x[0])
                this.currentStormwaterJurisdiction = x[0]
            })
            
        );


        this.areaBasedAcreCalculationsDto$ =  this.stormwaterJurisdiction$.pipe(
            tap(() => {
                this.isLoading = true;
            }),
            switchMap((x) => {
                return this.trashResultsByJurisdictionService.trashResultsByJurisdictionJurisdictionIDAreaBasedResultsCalculationsGet(x.StormwaterJurisdictionID);
            }),
            tap(() => {
                this.isLoading = false;
            }));
        
        this.loadResultsDto$ = this.stormwaterJurisdiction$.pipe(
            tap(() => {
                this.isLoading = true;
            }),
            switchMap((x) => {
                return this.trashResultsByJurisdictionService.trashResultsByJurisdictionJurisdictionIDLoadBasedResultsCalculationsGet(x.StormwaterJurisdictionID);
            }),
            tap(() => {
                this.isLoading = false;
            }));
        
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
            switchMap((x) => {
                return this.stormwaterJurisdictionService.jurisdictionsJurisdictionIDBoundingBoxGet(x.StormwaterJurisdictionID);
            }),
            tap((boundingBox) => {
                if(this.mapIsReady){
                    this.leafletHelperService.fitMapToBoundingBox(this.map, boundingBox);
                }
            }));
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }

    public onJurisdictionSelected(selectedJurisdiction: StormwaterJurisdictionDto) {
        this.stormwaterJurisdictionSubject.next(selectedJurisdiction);
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
        this.authenticationService.login();
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


}
