import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Injectable } from '@angular/core';
import { AlertService } from '../services/alert.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CustomPageService } from 'src/app/services/custom-page.service';
@Injectable({
  providedIn: 'root'
})

export class CustomPageAccessGuard implements CanActivate {
  constructor(
    private router: Router,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private customPageService: CustomPageService) { }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> | Promise<boolean> | boolean 
  {
    const vanityUrl = route.paramMap.get("vanity-url");
    if (vanityUrl) {
      return this.customPageService.getCustomPageRolesByVanityUrl(vanityUrl)
        .pipe(
          map(roles => {
          if (this.authenticationService.doesCurrentUserHaveOneOfTheseRoles(roles.map(y => y.RoleID))) {
            return true;
          } else {
            return this.returnUnauthorized();
          }
        })
        )
    }
    else {
      return this.returnUnauthorized();
    }
  }

  private returnUnauthorized() {
    this.router.navigate(["/"]).then(() => {
      this.alertService.pushNotFoundUnauthorizedAlert();
    });
    return false;
  }
}
