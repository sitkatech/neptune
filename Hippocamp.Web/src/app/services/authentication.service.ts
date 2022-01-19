import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { UserService } from './user/user.service';
import { Subject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { CookieStorageService } from '../shared/services/cookies/cookie-storage.service';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { RoleEnum } from '../shared/models/enums/role.enum';
import { AlertService } from '../shared/services/alert.service';
import { Alert } from '../shared/models/alert';
import { AlertContext } from '../shared/models/enums/alert-context.enum';
import { environment } from 'src/environments/environment';
import { PersonCreateDto } from '../shared/generated/model/person-create-dto';
import { PersonDto } from '../shared/generated/model/person-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUser: PersonDto;

  private getUserObservable: any;

  private _currentUserSetSubject = new Subject<PersonDto>();
  public currentUserSetObservable = this._currentUserSetSubject.asObservable();


  constructor(private router: Router,
    private oauthService: OAuthService,
    private cookieStorageService: CookieStorageService,
    private userService: UserService,
    private alertService: AlertService) {
      this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe((e: NavigationEnd) => {
        if (this.isAuthenticated()) {
          this.getGlobalIDFromClaimsAndAttemptToSetUserObservableAndCreateUserIfNecessary();
        } else {
          this.currentUser = null;
          this._currentUserSetSubject.next(null);
        }
      });

    // check for a currentUser at NavigationStart so that authorization-based guards can work with promises.
    this.router.events
      .pipe(filter(e => e instanceof NavigationStart))
      .subscribe((e: NavigationStart) => {
        this.checkAuthentication();
      })
  }

  public checkAuthentication() {
    if (this.isAuthenticated() && !this.currentUser) {
      console.log("Authenticated but no user found...");
      this.getGlobalIDFromClaimsAndAttemptToSetUserObservableAndCreateUserIfNecessary();
    }
  }

  public getGlobalIDFromClaimsAndAttemptToSetUserObservableAndCreateUserIfNecessary() {
    var claims = this.oauthService.getIdentityClaims();
    var globalID = claims["sub"];

    this.getUserObservable = this.userService.getUserFromGlobalID(globalID).subscribe(result => {
      this.getUserCallback(result);
    }, error => {
      if (error.status !== 404) {
        this.alertService.pushAlert(new Alert("There was an error logging into the application.", AlertContext.Danger));
        this.router.navigate(['/']);
      } else {
        this.alertService.clearAlerts();
        const newUser = new PersonCreateDto({
          FirstName: claims["given_name"],
          LastName: claims["family_name"],
          Email: claims["email"],
          RoleID: RoleEnum.Unassigned,
          LoginName: claims["login_name"],
          UserGuid: claims["sub"],
          OrganizationName: claims["organization_name"]
        });

        this.userService.createNewUser(newUser).subscribe(user => {
          this.getUserCallback(user);
        })

      }
    });
  }

  private getUserCallback(user: PersonDto) {
    this.currentUser = user;
    this._currentUserSetSubject.next(this.currentUser);
  }

  public refreshUserInfo(user: PersonDto) {
    this.getUserCallback(user);
  }

  dispose() {
    this.getUserObservable.unsubscribe();
  }

  public isAuthenticated(): boolean {
    return this.oauthService.hasValidAccessToken();
  }

  public handleUnauthorized(): void {
    this.forcedLogout();
  }

  public login() {
    this.oauthService.initCodeFlow();
  }
  
  public createAccount() {
    localStorage.setItem("loginOnReturn", "true");
    window.location.href = `${environment.keystoneAuthConfiguration.issuer}/Account/Register?${this.getClientIDAndRedirectUrlForKeystone()}`;
  }

  public getClientIDAndRedirectUrlForKeystone() {
    return `ClientID=${environment.keystoneAuthConfiguration.clientId}&RedirectUrl=${encodeURIComponent(environment.createAccountRedirectUrl)}`;
  }

  public forcedLogout() {
    sessionStorage["authRedirectUrl"] = window.location.href;
    this.logout();
  }

  public logout() {
    this.oauthService.logOut();
    setTimeout(() => {
      this.cookieStorageService.removeAll();
    });
  }

  public isUserAnAdministrator(user: PersonDto): boolean {
    const role = user && user.Role
      ? user.Role.RoleID
      : null;
    return role === RoleEnum.Admin || role === RoleEnum.SitkaAdmin;
  }

  public isCurrentUserAnAdministrator(): boolean {
    return this.isUserAnAdministrator(this.currentUser);
  }

  public isUserUnassigned(user: PersonDto): boolean {
    const role = user && user.Role
      ? user.Role.RoleID
      : null;
    return role === RoleEnum.Unassigned;
  }

  public isCurrentUserNullOrUndefined(): boolean {
    return !this.currentUser;
  }

  public doesCurrentUserHaveOneOfTheseRoles(roleIDs: Array<number>): boolean {
    if(roleIDs.length === 0)
    {
      return false;
    }
    const roleID = this.currentUser && this.currentUser.Role
      ? this.currentUser.Role.RoleID
      : null;
    return roleIDs.includes(roleID);
  }
}
