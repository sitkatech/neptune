import { Component, OnInit, HostListener, ChangeDetectorRef, OnDestroy, Input } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { NgIf } from "@angular/common";
import { RouterLink, RouterLinkActive, RouterOutlet } from "@angular/router";
import { DropdownToggleDirective } from "src/app/shared/directives/dropdown-toggle.directive";
import { IconComponent } from "src/app/shared/components/icon/icon.component";

@Component({
    selector: "header-nav",
    templateUrl: "./header-nav.component.html",
    styleUrls: ["./header-nav.component.scss"],
    standalone: true,
    imports: [RouterLink, RouterLinkActive, NgIf, DropdownToggleDirective, IconComponent],
})
export class HeaderNavComponent implements OnInit, OnDestroy {
    @Input() moduleTitle: string;
    private watchUserChangeSubscription: any;
    private currentUser: PersonDto;

    public windowWidth: number;

    public showCurrentPageHeader: boolean = true;

    @HostListener("window:resize", ["$event"])
    resize() {
        this.windowWidth = window.innerWidth;
    }

    constructor(private authenticationService: AuthenticationService, private cdr: ChangeDetectorRef) {}

    ngOnInit() {
        this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe((currentUser) => {
            this.currentUser = currentUser;
        });
    }

    ngOnDestroy() {
        this.watchUserChangeSubscription.unsubscribe();

        this.cdr.detach();
    }

    public isAuthenticated(): boolean {
        return this.authenticationService.isAuthenticated();
    }

    public isAdministrator(): boolean {
        return this.authenticationService.isUserAnAdministrator(this.currentUser);
    }

    public getUserName() {
        return this.currentUser ? this.currentUser.FirstName + " " + this.currentUser.LastName : null;
    }

    public login(): void {
        this.authenticationService.login();
    }

    public logout(): void {
        this.authenticationService.logout();

        setTimeout(() => {
            this.cdr.detectChanges();
        });
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
