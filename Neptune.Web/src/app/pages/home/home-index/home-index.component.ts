import { Component, OnInit, OnDestroy } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { environment } from "src/environments/environment";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { RoleEnum } from "src/app/shared/generated/enum/role-enum";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { CustomRichTextComponent } from "../../../shared/components/custom-rich-text/custom-rich-text.component";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { Observable } from "rxjs";

@Component({
    selector: "app-home-index",
    templateUrl: "./home-index.component.html",
    styleUrls: ["./home-index.component.scss"],
    standalone: true,
    imports: [NgIf, AlertDisplayComponent, RouterLink, CustomRichTextComponent, AsyncPipe],
})
export class HomeIndexComponent implements OnInit, OnDestroy {
    public watchUserChangeSubscription: any;
    public currentUser$: Observable<PersonDto>;

    public richTextTypeID: number = NeptunePageTypeEnum.HippocampHomePage;

    constructor(private authenticationService: AuthenticationService, private router: Router, private route: ActivatedRoute) {}

    public ngOnInit(): void {
        this.currentUser$ = this.authenticationService.getCurrentUser();

        this.route.queryParams.subscribe((params) => {
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
                this.router.navigateByUrl(sessionStorage.getItem("authRedirectUrl")).then(() => {
                    sessionStorage.removeItem("authRedirectUrl");
                });
            }
        });
    }

    ngOnDestroy(): void {
        this.watchUserChangeSubscription?.unsubscribe();
    }

    public userIsUnassigned(currentUser: PersonDto) {
        if (!currentUser) {
            return false; // doesn't exist != unassigned
        }

        return currentUser.Role.RoleID === RoleEnum.Unassigned;
    }

    public userIsOCTAGrantReviewer(currentUser: PersonDto) {
        if (!currentUser) {
            return false;
        }

        return currentUser.IsOCTAGrantReviewer;
    }

    public isUserAnAdministrator(currentUser: PersonDto) {
        return this.authenticationService.isUserAnAdministrator(currentUser);
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

    public requestSupportUrl(): string {
        return `${this.ocstBaseUrl()}/Help/Support`;
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }
}
