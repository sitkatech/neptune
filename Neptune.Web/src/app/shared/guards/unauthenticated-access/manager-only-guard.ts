import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Injectable } from '@angular/core';
import { AlertService } from '../../services/alert.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { RoleEnum } from '../../generated/enum/role-enum';
@Injectable({
  providedIn: 'root'
})
export class ManagerOnlyGuard  {
  constructor(
    private router: Router,
    private alertService: AlertService, 
    private authenticationService: AuthenticationService) { }
    
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (!this.authenticationService.isCurrentUserNullOrUndefined()) {
      if (this.authenticationService.isCurrentUserAnAdministrator()) {
        return true;
      } else {
        return this.returnUnauthorized();
      }
    }

    return this.authenticationService.currentUserSetObservable
      .pipe(
        map(x => {
          if (x.Role.RoleID == RoleEnum.Admin || x.Role.RoleID == RoleEnum.SitkaAdmin) {
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
