import { Component, Inject, Renderer2, ViewContainerRef, DOCUMENT } from "@angular/core";
import { environment } from "../environments/environment";
import { RouterOutlet } from "@angular/router";
import { AuthenticationService } from "./services/authentication.service";
import { Title } from "@angular/platform-browser";
import { PersonDto } from "./shared/generated/model/person-dto";

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

    private currentUser: PersonDto;

    public currentYear: number = new Date().getFullYear();

    constructor(
        @Inject(DOCUMENT) private _document: Document,
        private authenticationService: AuthenticationService,
        private titleService: Title,
        private renderer: Renderer2,
        public viewRef: ViewContainerRef
    ) {}

    ngOnInit() {
        console.log("Environment:", environment);
        this.isIframe = window !== window.parent && !window.opener;
        const environmentClassName = environment.production ? "env-prod" : environment.staging ? "env-qa" : "env-dev";
        this.renderer.addClass(this._document.body, environmentClassName);

        this.authenticationService.currentUserSetObservable.subscribe((currentUser) => {
            this.currentUser = currentUser;
        });

        this.titleService.setTitle(`Stormwater Tools | Orange County`);
        this.setAppFavicon();
    }

    setAppFavicon() {
        this._document.getElementById("appFavicon").setAttribute("href", "assets/main/favicons/favicon.ico");
    }

    public legalUrl(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Legal`;
    }
}
