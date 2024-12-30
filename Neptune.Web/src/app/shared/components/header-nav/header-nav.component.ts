import { Component, OnInit, HostListener, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { environment } from 'src/environments/environment';
import { PersonDto } from '../../generated/model/person-dto';
import { NgIf } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
    selector: 'header-nav',
    templateUrl: './header-nav.component.html',
    styleUrls: ['./header-nav.component.scss'],
    standalone: true,
    imports: [RouterLink, RouterLinkActive, NgIf]
})

export class HeaderNavComponent implements OnInit, OnDestroy {
    private watchUserChangeSubscription: any;
    private currentUser: PersonDto;

    public windowWidth: number;

    public showCurrentPageHeader: boolean = true;

    @HostListener('window:resize', ['$event'])
    resize() {
        this.windowWidth = window.innerWidth;
    }

    constructor(
        private authenticationService: AuthenticationService,
        private cdr: ChangeDetectorRef) {
    }

    ngOnInit() {
        this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
            this.currentUser = currentUser;
        });
    }

    ngOnDestroy() {
        this.watchUserChangeSubscription.unsubscribe();
        
        this.cdr.detach();
    }

    public toggleCurrentPageHeader(){
        this.showCurrentPageHeader = !this.showCurrentPageHeader;
    }

    public isAuthenticated(): boolean {
        return this.authenticationService.isAuthenticated();
    }

    public isAdministrator(): boolean {
        return this.authenticationService.isUserAnAdministrator(this.currentUser);
    }

    public isNotUnassigned(): boolean{
        if (!this.currentUser) {
            return false;
        }
        return !this.authenticationService.isUserUnassigned(this.currentUser);
    }

    public isOCTAGrantReviewer(): boolean {
        return this.authenticationService.isCurrentUserAnOCTAGrantReviewer();
    }

    public getUserName() {
        return this.currentUser ? this.currentUser.FirstName + " " + this.currentUser.LastName
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

    public usersListUrl(): string{
        return `${environment.ocStormwaterToolsBaseUrl}/User/Index`;
    }

    public organizationsIndexUrl(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Organization/Index`;
    }

    public requestSupportUrl(): string {
        return `${environment.ocStormwaterToolsBaseUrl}/Help/Support`;
    }

    public ocStormwaterToolsMainUrl(): string{
        return environment.ocStormwaterToolsBaseUrl;
    }

}
