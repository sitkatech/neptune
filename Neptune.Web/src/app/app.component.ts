import { Component, Inject, Renderer2, ViewContainerRef, DOCUMENT } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";
import { JwksValidationHandler } from "angular-oauth2-oidc-jwks";
import { environment } from "../environments/environment";
import { Router, RouteConfigLoadStart, RouteConfigLoadEnd, NavigationEnd, RouterOutlet } from "@angular/router";
import { BusyService } from "./shared/services";
import { AuthenticationService } from "./services/authentication.service";
import { Title } from "@angular/platform-browser";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.scss"],

    imports: [RouterOutlet],
})
export class AppComponent {
    public isIframe = false;
    userClaimsUpsertStarted = false;
    ignoreSessionTerminated = false;

    public currentYear: number = new Date().getFullYear();

    constructor(
        @Inject(DOCUMENT) private _document: Document,
        private router: Router,
        private oauthService: OAuthService,
        private busyService: BusyService,
        private authenticationService: AuthenticationService,
        private titleService: Title,
        private renderer: Renderer2,
        public viewRef: ViewContainerRef
    ) {
        this.configureOAuthService();
        this.authenticationService.initialLoginSequence();
    }

    ngOnInit() {
        this.isIframe = window !== window.parent && !window.opener;
        const environmentClassName = environment.production ? "env-prod" : environment.staging ? "env-qa" : "env-dev";
        this.renderer.addClass(this._document.body, environmentClassName);

        this.router.events.subscribe((event: any) => {
            if (event instanceof RouteConfigLoadStart) {
                // lazy loaded route started
                this.busyService.setBusy(true);
            } else if (event instanceof RouteConfigLoadEnd) {
                // lazy loaded route ended
                this.busyService.setBusy(false);
            } else if (event instanceof NavigationEnd) {
                window.scrollTo(0, 0);
            }
        });

        this.titleService.setTitle(`Stormwater Tools | Orange County`);
        this.setAppFavicon();
    }

    private configureOAuthService() {
        this.oauthService.configure(environment.keystoneAuthConfiguration);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    }

    setAppFavicon() {
        this._document.getElementById("appFavicon").setAttribute("href", "assets/main/favicons/favicon.ico");
    }

    public legalUrl(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Legal`;
    }
}
