import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserDetailedDto } from 'src/app/shared/models';
import { RoleEnum } from 'src/app/shared/models/enums/role.enum';
import { environment } from 'src/environments/environment';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginCallbackComponent } from '../../login-callback/login-callback.component';

@Component({
    selector: 'app-home-index',
    templateUrl: './home-index.component.html',
    styleUrls: ['./home-index.component.scss']
})
export class HomeIndexComponent implements OnInit, OnDestroy {
    public watchUserChangeSubscription: any;
    public currentUser: UserDetailedDto;

    public richTextTypeID: number = CustomRichTextType.Homepage;

    constructor(private authenticationService: AuthenticationService,
        private router: Router,
        private route: ActivatedRoute) {
    }

    public ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            //We're logging in
            if (params.hasOwnProperty("code")) {
                this.router.navigate(["/signin-oidc"], { queryParams: params });
                return;
            }

            if (localStorage.getItem("loginOnReturn")) {
                localStorage.removeItem("loginOnReturn");
                this.authenticationService.login();
            }

            //We were forced to logout or were sent a link and just finished logging in
            if (sessionStorage.getItem("authRedirectUrl")) {
                this.router.navigateByUrl(sessionStorage.getItem("authRedirectUrl"))
                    .then(() => {
                        sessionStorage.removeItem("authRedirectUrl");
                    });
            }

            this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
                this.currentUser = currentUser;
            });

        });
    }

    ngOnDestroy(): void {
        this.watchUserChangeSubscription?.unsubscribe();
    }

    public userIsUnassigned() {
        if (!this.currentUser) {
            return false; // doesn't exist != unassigned
        }

        return this.currentUser.Role.RoleID === RoleEnum.Unassigned;
    }

    public userRoleIsDisabled() {
        if (!this.currentUser) {
            return false; // doesn't exist != unassigned
        }

        return this.currentUser.Role.RoleID === RoleEnum.Disabled;
    }

    public isUserAnAdministrator() {
        return this.authenticationService.isUserAnAdministrator(this.currentUser);
    }

    public login(): void {
        this.authenticationService.login();
    }

    public createAccount(): void {
        this.authenticationService.createAccount();
    }

    public forgotPasswordUrl(): string {
        return `${environment.keystoneAuthConfiguration.issuer}/Account/ForgotPassword?${this.authenticationService.getClientIDAndRedirectUrlForKeystone()}`;
    }

    public forgotUsernameUrl(): string {
        return `${environment.keystoneAuthConfiguration.issuer}/Account/ForgotUsername?${this.authenticationService.getClientIDAndRedirectUrlForKeystone()}`;
    }

    public keystoneSupportUrl(): string {
        return `${environment.keystoneAuthConfiguration.issuer}/Account/Support/20?${this.authenticationService.getClientIDAndRedirectUrlForKeystone()}`;
    }

    public platformLongName(): string {
        return environment.platformLongName;
    }

    public platformShortName(): string {
        return environment.platformShortName;
    }

    public leadOrganizationShortName(): string {
        return environment.leadOrganizationShortName;
    }

    public leadOrganizationLongName(): string {
        return environment.leadOrganizationLongName;
    }

    public leadOrganizationHomeUrl(): string {
        return environment.leadOrganizationHomeUrl;
    }
}
