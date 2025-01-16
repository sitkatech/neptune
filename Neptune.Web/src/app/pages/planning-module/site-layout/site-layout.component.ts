import { Component, OnInit, HostListener, ChangeDetectorRef, OnDestroy } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AsyncPipe, NgIf } from "@angular/common";
import { RouterLink, RouterLinkActive, RouterOutlet } from "@angular/router";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { IconComponent } from "src/app/shared/components/icon/icon.component";
import { HeaderNavComponent } from "../../../shared/components/header-nav/header-nav.component";
import { Observable, tap } from "rxjs";

@Component({
    selector: "site-layout",
    templateUrl: "./site-layout.component.html",
    styleUrls: ["./site-layout.component.scss"],
    standalone: true,
    imports: [RouterLink, RouterLinkActive, RouterOutlet, NgIf, AsyncPipe, DropdownToggleDirective, IconComponent, HeaderNavComponent],
})
export class SiteLayoutComponent implements OnInit {
    public currentUser$: Observable<PersonDto>;
    private currentUser: PersonDto;

    public windowWidth: number;

    public showCurrentPageHeader: boolean = true;

    @HostListener("window:resize", ["$event"])
    resize() {
        this.windowWidth = window.innerWidth;
    }

    constructor(private authenticationService: AuthenticationService) {}

    ngOnInit() {
        this.currentUser$ = this.authenticationService.getCurrentUser().pipe(
            tap((currentUser) => {
                this.currentUser = currentUser;
            })
        );
    }

    public isAuthenticated(): boolean {
        return this.authenticationService.isAuthenticated();
    }

    public isAdministrator(): boolean {
        return this.authenticationService.isUserAnAdministrator(this.currentUser);
    }

    public isNotUnassigned(): boolean {
        if (!this.currentUser) {
            return false;
        }
        return !this.authenticationService.isUserUnassigned(this.currentUser);
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
