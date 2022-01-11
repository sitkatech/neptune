import { Component, OnInit, HostListener, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { CookieStorageService } from '../../services/cookies/cookie-storage.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserDetailedDto } from '../../models';
import { environment } from 'src/environments/environment';
import { CustomPageService } from 'src/app/services/custom-page.service';
import { CustomPageDetailedDto } from '../../models/custom-page-detailed-dto';

@Component({
    selector: 'header-nav',
    templateUrl: './header-nav.component.html',
    styleUrls: ['./header-nav.component.scss']
})

export class HeaderNavComponent implements OnInit, OnDestroy {
    private watchUserChangeSubscription: any;
    private currentUser: UserDetailedDto;

    public windowWidth: number;
    public viewPages: CustomPageDetailedDto[] = [];
    public managePages: CustomPageDetailedDto[] = [];
    public learnMorePages: CustomPageDetailedDto[] = [];

    @HostListener('window:resize', ['$event'])
    resize() {
        this.windowWidth = window.innerWidth;
    }

    constructor(
        private authenticationService: AuthenticationService,
        private cookieStorageService: CookieStorageService,
        private customPageService: CustomPageService,
        private cdr: ChangeDetectorRef) {
    }

    ngOnInit() {
        this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
            this.currentUser = currentUser;

            this.customPageService.getAllCustomPagesWithRoles().subscribe(customPagesWithRoles => {
                customPagesWithRoles = customPagesWithRoles
                    .filter(x => x.ViewableRoles.map(role => role.RoleID).includes(this.currentUser?.Role?.RoleID));
                this.viewPages = customPagesWithRoles.filter(x => x.MenuItem.MenuItemName == "View");
                this.managePages = customPagesWithRoles.filter(x => x.MenuItem.MenuItemName == "Manage");
                this.learnMorePages = customPagesWithRoles.filter(x => x.MenuItem.MenuItemName == "LearnMore");
            });
        });
    }

    ngOnDestroy() {
        this.watchUserChangeSubscription.unsubscribe();
        this.authenticationService.dispose();
        this.cdr.detach();
    }

    public isAuthenticated(): boolean {
        return this.authenticationService.isAuthenticated();
    }

    public isAdministrator(): boolean {
        return this.authenticationService.isUserAnAdministrator(this.currentUser);
    }

    public isUnassigned(): boolean{
        return this.authenticationService.isUserUnassigned(this.currentUser);
    }

    public isUnassignedOrDisabled(): boolean{
        return this.authenticationService.isUserUnassigned(this.currentUser) || this.authenticationService.isUserRoleDisabled(this.currentUser);
    }

    public getUserName() {
        return this.currentUser ? this.currentUser.FullName
            : null;
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


    public platformShortName(): string{
        return environment.platformShortName;
    }
	
	public leadOrganizationShortName(): string{
        return environment.leadOrganizationShortName;
    }

    public leadOrganizationHomeUrl(): string{
        return environment.leadOrganizationHomeUrl;
    }

    public leadOrganizationLogoSrc(): string{
        return `assets/main/logos/${environment.leadOrganizationLogoFilename}`;
    }

    public isCurrentUserBeingImpersonated(): boolean {
        return this.authenticationService.isCurrentUserBeingImpersonated(this.currentUser);
    }
}
