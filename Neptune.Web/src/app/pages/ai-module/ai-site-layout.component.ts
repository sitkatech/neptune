import { Component } from "@angular/core";
import { RouterOutlet, RouterLink } from "@angular/router";
import { HeaderNavComponent } from "../../shared/components/header-nav/header-nav.component";
import { environment } from "src/environments/environment";
import { AsyncPipe } from "@angular/common";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AuthenticationService } from "src/app/services/authentication.service";
import { Observable } from "rxjs";

@Component({
    selector: "ai-site-layout",
    templateUrl: "./ai-site-layout.component.html",
    styleUrls: ["./ai-site-layout.component.scss"],
    imports: [RouterOutlet, RouterLink, HeaderNavComponent, AsyncPipe],
    standalone: true,
})
export class AiSiteLayoutComponent {
    public siteUrl = environment.ocStormwaterToolsBaseUrl;
    public currentUser$: Observable<PersonDto>;
    constructor(private authenticationService: AuthenticationService) {
        this.currentUser$ = this.authenticationService.getCurrentUser();
    }
}
