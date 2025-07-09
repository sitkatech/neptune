import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AsyncPipe, NgIf } from "@angular/common";
import { RouterLink, RouterLinkActive, RouterOutlet } from "@angular/router";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { IconComponent } from "src/app/shared/components/icon/icon.component";
import { HeaderNavComponent } from "../../shared/components/header-nav/header-nav.component";
import { Observable } from "rxjs";

@Component({
    selector: "site-layout",
    templateUrl: "./site-layout.component.html",
    styleUrls: ["./site-layout.component.scss"],
    imports: [RouterLink, RouterLinkActive, RouterOutlet, NgIf, AsyncPipe, DropdownToggleDirective, IconComponent, HeaderNavComponent]
})
export class SiteLayoutComponent implements OnInit {
    public currentUser$: Observable<PersonDto>;
    public siteUrl = environment.ocStormwaterToolsBaseUrl;

    constructor(private authenticationService: AuthenticationService) {}

    ngOnInit() {
        this.currentUser$ = this.authenticationService.getCurrentUser();
    }

    public isAuthenticated(): boolean {
        return this.authenticationService.isAuthenticated();
    }

    public isAdministrator(currentUser: PersonDto): boolean {
        return this.authenticationService.isUserAnAdministrator(currentUser);
    }

    public isJurisdicionManager(currentUser: PersonDto): boolean {
        return this.authenticationService.isUserAJurisdictionManager(currentUser);
    }

    public isNotUnassigned(currentUser: PersonDto): boolean {
        if (!currentUser) {
            return false;
        }
        return !this.authenticationService.isUserUnassigned(currentUser);
    }

    public isOCTAGrantReviewer(): boolean {
        return this.authenticationService.isCurrentUserAnOCTAGrantReviewer();
    }

    public usersListUrl(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/User/Index`;
    }

    public organizationsIndexUrl(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Organization/Index`;
    }
}
