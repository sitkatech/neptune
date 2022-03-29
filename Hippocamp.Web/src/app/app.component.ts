import { Component, Inject } from '@angular/core';
import { OAuthService, OAuthSuccessEvent } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { Subject, Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { CookieStorageService } from './shared/services/cookies/cookie-storage.service';
import { Router, RouteConfigLoadStart, RouteConfigLoadEnd, NavigationEnd } from '@angular/router';
import { BusyService } from './shared/services';
import { AuthenticationService } from './services/authentication.service';
import { Title } from '@angular/platform-browser';
import { DOCUMENT } from '@angular/common';

declare var require: any

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {

    userClaimsUpsertStarted = false;
    ignoreSessionTerminated = false;
    public currentYear: number = new Date().getFullYear();
    
    constructor(
        private router: Router,
        private oauthService: OAuthService,
        private busyService: BusyService,
        private authenticationService: AuthenticationService,
        private titleService: Title,
        @Inject(DOCUMENT) private _document: HTMLDocument
    ) {
        this.configureOAuthService();
        this.authenticationService.initialLoginSequence();
    }

    ngOnInit() {
        this.router.events.subscribe((event: any) => {
            if (event instanceof RouteConfigLoadStart) { // lazy loaded route started
                this.busyService.setBusy(true);
            } else if (event instanceof RouteConfigLoadEnd) { // lazy loaded route ended
                this.busyService.setBusy(false);
            } else if (event instanceof NavigationEnd) {
                window.scrollTo(0, 0);
            }
        });

        this.titleService.setTitle(`${environment.platformShortName}`)
        this.setAppFavicon();
    }

    private configureOAuthService() {
        this.oauthService.configure(environment.keystoneAuthConfiguration);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    }

    setAppFavicon(){
        this._document.getElementById('appFavicon').setAttribute('href', "assets/main/favicons/" + environment.faviconFilename);
     }

    public legalUrl(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Legal`;
    }
}
