import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { AuthenticationService } from "src/app/services/authentication.service";
import { AlertService } from "../../services/alert.service";
import { RoleEnum } from "src/app/shared/generated/enum/role-enum";

@Injectable({
    providedIn: "root",
})
export class JurisdictionManagerOrEditorOnlyGuard {
    constructor(private router: Router, private alertService: AlertService, private authenticationService: AuthenticationService) {}

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        if (!this.authenticationService.isCurrentUserNullOrUndefined()) {
            if (this.authenticationService.doesCurrentUserHaveOneOfTheseRoles([RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor])) {
                return true;
            } else {
                return this.returnUnauthorized();
            }
        }

        return this.authenticationService.currentUserSetObservable.pipe(
            map((x) => {
                if (x.RoleID == RoleEnum.Admin || x.RoleID == RoleEnum.SitkaAdmin || x.RoleID == RoleEnum.JurisdictionManager || x.RoleID == RoleEnum.JurisdictionEditor) {
                    return true;
                } else {
                    return this.returnUnauthorized();
                }
            })
        );
    }

    private returnUnauthorized() {
        this.router.navigate(["/"]).then(() => {
            this.alertService.pushNotFoundUnauthorizedAlert();
        });
        return false;
    }
}
