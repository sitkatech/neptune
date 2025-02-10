import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AsyncPipe, NgIf } from "@angular/common";
import { RouterLink, RouterLinkActive, RouterOutlet } from "@angular/router";
import { HeaderNavComponent } from "../../../shared/components/header-nav/header-nav.component";
import { Observable } from "rxjs";
import { IconComponent } from "../../../shared/components/icon/icon.component";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { environment } from "src/environments/environment";

@Component({
    selector: "trash-site-layout",
    templateUrl: "./trash-site-layout.component.html",
    styleUrls: ["./trash-site-layout.component.scss"],
    standalone: true,
    imports: [RouterLink, RouterLinkActive, RouterOutlet, NgIf, AsyncPipe, HeaderNavComponent, IconComponent, DropdownToggleDirective],
})
export class TrashSiteLayoutComponent implements OnInit {
    public currentUser$: Observable<PersonDto>;
    public siteUrl = environment.ocStormwaterToolsBaseUrl;

    constructor(private authenticationService: AuthenticationService) {}

    ngOnInit() {
        this.currentUser$ = this.authenticationService.getCurrentUser();
    }

    public isAuthenticated(): boolean {
        return this.authenticationService.isAuthenticated();
    }

    public isNotUnassigned(currentUser: PersonDto): boolean {
        if (!currentUser) {
            return false;
        }
        return !this.authenticationService.isUserUnassigned(currentUser);
    }
}
