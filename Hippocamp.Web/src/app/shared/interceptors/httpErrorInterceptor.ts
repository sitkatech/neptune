import { Injectable } from '@angular/core';
import {
    HttpInterceptor,
    HttpRequest,
    HttpErrorResponse,
    HttpHandler,
    HttpEvent,
    HttpResponse
} from '@angular/common/http';

import { Observable, EMPTY, throwError, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AlertService } from '../services/alert.service';
import { AlertContext } from '../models/enums/alert-context.enum';
import { Alert } from '../models/alert';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router, private alertService: AlertService) {
    }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.error instanceof Error) {
                    // A client-side or network error occurred. Handle it accordingly.
                    console.error('An error occurred:', error.error.message);
                }
                else {
                    // The backend returned an unsuccessful response code.
                    // The response body may contain clues as to what went wrong,
                    console.error(`Backend returned code ${error.status}, body was: ${error.error}`);

                    if (error instanceof HttpErrorResponse) {
                        if (error.status == 401) {
                            this.router.navigateByUrl("/unauthenticated", { replaceUrl: false}).then(x => {
                                if(typeof error.error === "string") {
                                    this.alertService.pushAlert(new Alert(error.error, AlertContext.Danger));
                                }
                            });
                        }
                        if (error.status == 403) {
                            this.router.navigateByUrl("/subscription-insufficient", { replaceUrl: false}).then(x => {
                                if(typeof error.error === "string") {
                                    this.alertService.pushAlert(new Alert(error.error, AlertContext.Danger));
                                }
                            });
                        }
                        if (error.status == 404) {
                            if(error.error.includes("User with GUID "))
                            {
                                // we want the login-callback to create the user to trigger so we just let it pass through and have authentication-service handle it
                                return throwError(error);
                            }
                            else
                            {
                                this.router.navigateByUrl("/not-found", { replaceUrl: false}).then(x => {
                                    if(typeof error.error === "string") {
                                        this.alertService.pushAlert(new Alert(error.error, AlertContext.Danger));
                                    }
                                });
                            }
                        }
                    }
                }

                // If you want to return a new response:
                //return of(new HttpResponse({body: [{name: "Default value..."}]}));

                // If you want to return nothing:
                //return EMPTY;

                // Otherwise pass it on to the upper level and let them take care of it:
                return throwError(error);
            })
        );
    }
}