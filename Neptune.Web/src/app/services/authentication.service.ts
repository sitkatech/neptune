import { Injectable } from "@angular/core";
import { Observable, ReplaySubject, Subject, of, race } from "rxjs";
import { first, switchMap, takeUntil } from "rxjs/operators";
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
import { AuthService as Auth0Service } from "@auth0/auth0-angular";

@Injectable({
    providedIn: "root",
})
export class AuthenticationService {
    private currentUser: PersonDto;
    private claimsUser: any;
    private readonly _destroying$ = new Subject<void>();

    private _currentUserSetSubject = new ReplaySubject<PersonDto>(1);
    public currentUserSetObservable = this._currentUserSetSubject.asObservable();

    constructor(
        private router: Router,
        private auth0: Auth0Service,
        private userClaimsService: UserClaimsService,
        private alertService: AlertService
    ) {
        // Subscribe to Auth0 user stream to update claims and current user
        this.auth0.user$.pipe(takeUntil(this._destroying$)).subscribe((user) => {
            if (user) {
                this.claimsUser = user as any;
                this.postUser();
            } else {
                this.claimsUser = null;
                this.currentUser = null;
                this._currentUserSetSubject.next(this.currentUser);
            }
        });
    }

    private postUser() {
        this.userClaimsService.postUserClaimsUserClaims().subscribe(
            (result) => {
                this.updateUser(result);
            },
            () => {
                this.onGetUserError();
            }
        );
    }

    private updateUser(user: PersonDto) {
        this.currentUser = user;
        this._currentUserSetSubject.next(this.currentUser);
    }

    private onGetUserError() {
        this.router.navigate(["/"]).then((x) => {
            this.alertService.pushAlert(
                new Alert(
                    "There was an error authorizing with the application. The application will force log you out in 3 seconds, please try to login again.",
                    AlertContext.Danger
                )
            );
            setTimeout(() => {
                this.auth0.logout({ logoutParams: { returnTo: window.location.origin } } as any);
            }, 3000);
        });
    }

    public refreshUserInfo(user: PersonDto) {
        this.updateUser(user);
    }

    public isAuthenticated(): boolean {
        return this.claimsUser != null;
    }

    public handleUnauthorized(): void {
        this.forcedLogout();
    }

    public forcedLogout() {
        this.logout();
    }

    public guardInitObservable(): Observable<any> {
        // For Auth0, return an observable that completes when loading finishes and user info is available
        return this.auth0.isLoading$.pipe(
            first((loading) => loading === false),
            switchMap(() => of(null as any))
        );
    }

    public login() {
        this.auth0.loginWithRedirect();
    }

    public logout() {
        this.auth0.logout({ logoutParams: { returnTo: window.location.origin } } as any);
    }

    resetPassword() {
        // Password reset flow should be handled via Auth0 hosted page or Management API. Triggering a redirect to a password reset can be done via Universal Login with proper configurations.
        this.auth0.loginWithRedirect({ authorizationParams: { screen_hint: "reset-password" } } as any);
    }

    editProfile() {
        this.auth0.loginWithRedirect({ appState: { target: "/profile" } } as any);
    }

    updateEmail() {
        // Use the edit profile flow or a dedicated Auth0 action to change login/email
        this.auth0.loginWithRedirect({ appState: { target: "/profile" } } as any);
    }

    signUp() {
        const baseRedirect = environment.auth0?.redirectUri ?? window.location.origin;
        const target = baseRedirect.replace(/\/$/, "") + "/create-user-callback";
        this.auth0.loginWithRedirect({ authorizationParams: { screen_hint: "signup", redirect_uri: target } } as any);
    }

    public isCurrentUserNullOrUndefined(): boolean {
        return !this.currentUser;
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

    public getAccessToken(): Observable<string> {
        return this.auth0.getAccessTokenSilently();
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

    public doesCurrentUserHaveOneOfTheseRoles(roleIDs: Array<number>): boolean {
        if (roleIDs.length === 0) {
            return false;
        }
        const roleID = this.currentUser ? this.currentUser.RoleID : null;
        return roleIDs.includes(roleID);
    }
}
