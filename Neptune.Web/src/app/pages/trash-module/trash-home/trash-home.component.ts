import { Component, OnInit, OnDestroy } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { RoleEnum } from "src/app/shared/generated/enum/role-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { CustomRichTextComponent } from "src/app/shared/components/custom-rich-text/custom-rich-text.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { Observable } from "rxjs";
import { BtnGroupRadioInputComponent } from "../../../shared/components/inputs/btn-group-radio-input/btn-group-radio-input.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../shared/components/leaflet/neptune-map/neptune-map.component";
import { OvtaAreaLayerComponent } from "../../../shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import * as L from "leaflet";
import "leaflet-draw";
import "leaflet.fullscreen";

@Component({
    selector: "trash-home",
    templateUrl: "./trash-home.component.html",
    styleUrls: ["./trash-home.component.scss"],
    standalone: true,
    imports: [NgIf, AlertDisplayComponent, CustomRichTextComponent, AsyncPipe, RouterLink, BtnGroupRadioInputComponent, NeptuneMapComponent, OvtaAreaLayerComponent],
})
export class TrashHomeComponent implements OnInit, OnDestroy {
    public watchUserChangeSubscription: any;
    public currentUser$: Observable<PersonDto>;

    public richTextTypeID: number = NeptunePageTypeEnum.TrashHomePage;

    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;
    public activeTab: string = "Area-Based Results";
    public tabs = [
        { label: "Area-Based Results", value: "Area-Based Results" },
        { label: "Load-Based Results", value: "Load-Based Results" },
        { label: "OVTA-Based Results", value: "OVTA-Based Results" },
    ];

    constructor(private authenticationService: AuthenticationService, private router: Router, private route: ActivatedRoute) {}

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
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
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

    public setActiveTab(event) {
        this.activeTab = event;
        if (this.activeTab === "Area-Based Results") {
        } else if (this.activeTab === "Load-Based Results") {
        } else if (this.activeTab === "OVTA-Based Results") {
        }
    }
}
