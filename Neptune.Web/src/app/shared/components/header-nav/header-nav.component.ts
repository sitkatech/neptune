import { Component, OnInit, Input } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AsyncPipe, NgIf } from "@angular/common";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { IconComponent } from "src/app/shared/components/icon/icon.component";
import { Observable } from "rxjs";

@Component({
    selector: "header-nav",
    templateUrl: "./header-nav.component.html",
    styleUrls: ["./header-nav.component.scss"],
    standalone: true,
    imports: [RouterLink, RouterLinkActive, NgIf, AsyncPipe, DropdownToggleDirective, IconComponent],
})
export class HeaderNavComponent implements OnInit {
    @Input() moduleTitle: string;
    public currentUser$: Observable<PersonDto>;

    constructor(private authenticationService: AuthenticationService) {}

    ngOnInit() {
        this.currentUser$ = this.authenticationService.getCurrentUser();
    }

    public isAuthenticated(): boolean {
        return this.authenticationService.isAuthenticated();
    }

    public login(): void {
        this.authenticationService.login();
    }

    public logout(): void {
        this.authenticationService.logout();
    }

    public ocStormwaterToolsMainUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }

    public showTestingWarning(): boolean {
        return environment.staging || environment.dev;
    }

    public testingWarningText(): string {
        return environment.staging ? "QA Environment" : "Development Environment";
    }
}
