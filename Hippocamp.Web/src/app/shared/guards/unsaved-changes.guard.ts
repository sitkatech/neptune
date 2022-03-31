import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ConfirmService } from '../services/confirm.service';

export interface IDeactivateComponent {
  canExit: () => Observable<boolean> | Promise<boolean> | boolean;
}

@Injectable({
  providedIn: 'root'
})
export class UnsavedChangesGuard implements CanDeactivate<IDeactivateComponent> {

  constructor(private confirmService: ConfirmService) {}

  canDeactivate(
    component: IDeactivateComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (component.canExit && !component.canExit()) {
        return this.confirmService.confirm({ buttonClassYes: 'btn-danger', modalSize: 'md', title:'Warning: There are unsaved changes', message: 'You have unsaved changes on this page.  Are you sure you want to leave this page?', buttonTextYes: "Proceed", buttonTextNo: "Stay on page" });
      }
  
      return Promise.resolve(true);
  }
  
}
