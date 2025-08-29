import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from "@angular/router";
import { Observable } from "rxjs";
import { CookieStorageService } from "../../services/cookies/cookie-storage.service";
import { AuthenticationService } from "src/app/services/authentication.service";

@Injectable({
    providedIn: "root",
})
export class UnauthenticatedAccessGuard {
    constructor(private cookieStorageService: CookieStorageService, private authenticationService: AuthenticationService) {}

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        const token = this.cookieStorageService.getItem("access_token");
        if (token && this.authenticationService.isAuthenticated()) {
            return true;
        } else {
            this.authenticationService.login(true);
            return false;
        }
    }
}
