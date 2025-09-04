import { Injectable } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";
import { Observable, Subject, race } from "rxjs";
import { filter, first } from "rxjs/operators";
import { CookieStorageService } from "../shared/services/cookies/cookie-storage.service";
import { Router } from "@angular/router";
import { AlertService } from "../shared/services/alert.service";
import { Alert } from "../shared/models/alert";
import { AlertContext } from "../shared/models/enums/alert-context.enum";
import { environment } from "src/environments/environment";
import { PersonCreateDto } from "../shared/generated/model/person-create-dto";
import { PersonDto } from "../shared/generated/model/person-dto";
import { UserService } from "../shared/generated/api/user.service";
import { RoleEnum } from "../shared/generated/enum/role-enum";
import { UserClaimsService } from "../shared/generated/api/user-claims.service";

@Injectable({
    providedIn: "root",
})
export class AuthenticationService {
    private currentUser: PersonDto;

    private _currentUserSetSubject = new Subject<PersonDto>();
    public currentUserSetObservable = this._currentUserSetSubject.asObservable();

    constructor(
        private router: Router,
        private oauthService: OAuthService,
        private cookieStorageService: CookieStorageService,
        private userService: UserService,
        private userClaimsService: UserClaimsService,
        private alertService: AlertService
    ) {
        this.oauthService.events.pipe(filter((e) => ["discovery_document_loaded"].includes(e.type))).subscribe(() => {
            this.checkAuthentication();
        });

        this.oauthService.events.pipe(filter((e) => ["token_received"].includes(e.type))).subscribe(() => {
            this.checkAuthentication();
            this.oauthService.loadUserProfile();
        });

        this.oauthService.events
            .pipe(filter((e) => ["session_terminated", "session_error", "token_error", "token_refresh_error", "silent_refresh_error", "token_validation_error"].includes(e.type)))
            .subscribe(() => this.router.navigateByUrl("/"));

        this.oauthService.setupAutomaticSilentRefresh();
    }

    public initialLoginSequence() {
        this.oauthService
            .loadDiscoveryDocument()
            .then(() => this.oauthService.tryLogin())
            .then(() => Promise.resolve())
            .catch(() => {});
    }

    public checkAuthentication() {
        if (this.isAuthenticated() && !this.currentUser) {
            console.log("Authenticated but no user found...");
            var claims = this.oauthService.getIdentityClaims();
            this.getUser(claims);
        }
    }

    private getUser(claims: any) {
        var globalID = claims["sub"];

        this.userClaimsService.userClaimsGlobalIDGet(globalID).subscribe(
            (result) => {
                this.updateUser(result);
            },
            (error) => {
                this.onGetUserError(error, claims);
            }
        );
    }

    private updateUser(user: PersonDto) {
        this.currentUser = user;
        this._currentUserSetSubject.next(this.currentUser);
    }

    private onGetUserError(error: any, claims: any) {
        if (error.status !== 404) {
            this.alertService.pushAlert(new Alert("There was an error logging into the application.", AlertContext.Danger));
            this.router.navigate(["/"]);
        } else {
            this.alertService.clearAlerts();
            const newUser = new PersonCreateDto({
                FirstName: claims["given_name"],
                LastName: claims["family_name"],
                Email: claims["email"],
                LoginName: claims["login_name"],
                UserGuid: claims["sub"],
            });

            this.userService.usersPost(newUser).subscribe((user) => {
                this.updateUser(user);
            });
        }
    }

    public refreshUserInfo(user: PersonDto) {
        this.updateUser(user);
    }

    public isAuthenticated(): boolean {
        return this.oauthService.hasValidAccessToken();
    }

    public handleUnauthorized(): void {
        this.forcedLogout();
    }

    public forcedLogout() {
        sessionStorage["authRedirectUrl"] = window.location.href;
        this.logout();
    }

    public login(setRedirect: boolean = false) {
        if (setRedirect) {
            const url = new URL(window.location.href);
            sessionStorage["authRedirectUrl"] = url.pathname;
        }
        this.oauthService.initCodeFlow();
    }

    public createAccount() {
        localStorage.setItem("loginOnReturn", "true");
        window.location.href = `${environment.keystoneAuthConfiguration.issuer}/Account/Register?${this.getClientIDAndRedirectUrlForKeystone()}`;
    }

    public getClientIDAndRedirectUrlForKeystone() {
        return `ClientID=${environment.keystoneAuthConfiguration.clientId}&RedirectUrl=${encodeURIComponent(environment.createAccountRedirectUrl)}`;
    }

    public logout() {
        this.oauthService.logOut();
        setTimeout(() => {
            this.cookieStorageService.removeAll();
        });
    }

    public getAuthRedirectUrl() {
        return sessionStorage["authRedirectUrl"];
    }

    public setAuthRedirectUrl(url: string) {
        sessionStorage["authRedirectUrl"] = url;
    }

    public clearAuthRedirectUrl() {
        this.setAuthRedirectUrl("");
    }

    public getCurrentUser(): Observable<PersonDto> {
        return race(
            new Observable((subscriber) => {
                if (this.currentUser) {
                    subscriber.next(this.currentUser);
                    subscriber.complete();
                }
            }),
            this.currentUserSetObservable.pipe(first())
        );
    }

    public getAccessToken(): string {
        return this.oauthService.getAccessToken();
    }

    public isUserAnAdministrator(user: PersonDto): boolean {
        const role = user ? user.RoleID : null;
        return role === RoleEnum.Admin || role === RoleEnum.SitkaAdmin;
    }

    public isCurrentUserAnAdministrator(): boolean {
        return this.isUserAnAdministrator(this.currentUser);
    }

    public isCurrentUserAnOCTAGrantReviewer(): boolean {
        if (!this.currentUser) {
            return false;
        }
        return this.currentUser.IsOCTAGrantReviewer;
    }

    public isUserAJurisdictionManager(user: PersonDto): boolean {
        const role = user ? user.RoleID : null;
        return role === RoleEnum.JurisdictionManager;
    }

    public isCurrentUserAJurisdictionManagerWithAssignedJurisdiction(): boolean {
        return this.isUserAJurisdictionManager(this.currentUser) && this.doesCurrentUserHaveAssignedStormwaterJurisdiction();
    }

    public isCurrentUserAJurisdictionEditorWithAssignedJurisdiction(): boolean {
        return this.isUserAJurisdictionEditor(this.currentUser) && this.doesCurrentUserHaveAssignedStormwaterJurisdiction();
    }

    public isUserAJurisdictionEditor(user: PersonDto): boolean {
        const role = user ? user.RoleID : null;
        return role === RoleEnum.JurisdictionEditor;
    }

    public doesCurrentUserHaveAssignedStormwaterJurisdiction(): boolean {
        if (!this.currentUser) {
            return false;
        }
        return this.currentUser.HasAssignedStormwaterJurisdiction;
    }

    public doesCurrentUserHaveJurisdictionManagePermission(): boolean {
        return this.isCurrentUserAnAdministrator() || this.isCurrentUserAJurisdictionManagerWithAssignedJurisdiction();
    }

    public doesCurrentUserHaveJurisdictionEditPermission(): boolean {
        return (
            this.isCurrentUserAnAdministrator() ||
            this.isCurrentUserAJurisdictionManagerWithAssignedJurisdiction() ||
            this.isCurrentUserAJurisdictionEditorWithAssignedJurisdiction()
        );
    }

    public isUserUnassigned(user: PersonDto): boolean {
        const role = user ? user.RoleID : null;
        return role === RoleEnum.Unassigned;
    }

    public isCurrentUserNullOrUndefined(): boolean {
        return !this.currentUser;
    }

    public doesCurrentUserHaveOneOfTheseRoles(roleIDs: Array<number>): boolean {
        if (roleIDs.length === 0) {
            return false;
        }
        const roleID = this.currentUser ? this.currentUser.RoleID : null;
        return roleIDs.includes(roleID);
    }
}
