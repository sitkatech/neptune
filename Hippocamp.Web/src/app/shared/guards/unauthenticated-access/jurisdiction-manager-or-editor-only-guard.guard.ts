import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { AuthenticationService } from 'src/app/services/authentication.service';
import { RoleEnum } from '../../models/enums/role.enum';
import { AlertService } from '../../services/alert.service';

@Injectable({
  providedIn: 'root'
})
export class JurisdictionManagerOrEditorOnlyGuard implements CanActivate {
  constructor(
    private router: Router,
    private alertService: AlertService, 
    private authenticationService: AuthenticationService
  ) { }
    
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (!this.authenticationService.isCurrentUserNullOrUndefined()) {
      if (this.authenticationService.doesCurrentUserHaveOneOfTheseRoles([RoleEnum.Admin, RoleEnum.SitkaAdmin, 
        RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor])) {
        return true;
      } else {
        return this.returnUnauthorized();
      }
    }

    return this.authenticationService.currentUserSetObservable
      .pipe(
        map(x => {
          if (x.Role.RoleID == RoleEnum.Admin || x.Role.RoleID == RoleEnum.SitkaAdmin || 
            RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor) {
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
