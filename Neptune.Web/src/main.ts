import { enableProdMode, APP_INITIALIZER, ErrorHandler, importProvidersFrom } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { init_app } from './app/app.module';
import { environment } from './environments/environment';
import { AppComponent } from './app/app.component';
import { environment as environment_1 } from 'src/environments/environment';
import { Configuration } from './app/shared/generated/configuration';
import { ApiModule } from './app/shared/generated/api.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule } from '@angular/forms';
import { SharedModule } from './app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { provideAnimations } from '@angular/platform-browser/animations';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { AppRoutingModule } from './app/app-routing.module';
import { CookieStorageService } from './app/shared/services/cookies/cookie-storage.service';
import { OAuthStorage, OAuthModule } from 'angular-oauth2-oidc';
import { DecimalPipe, CurrencyPipe, DatePipe } from '@angular/common';
import { GlobalErrorHandlerService } from './app/shared/services/global-error-handler.service';
import { HttpErrorInterceptor } from './app/shared/interceptors/httpErrorInterceptor';
import { AuthInterceptor } from './app/shared/interceptors/auth-interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppInitService } from './app/app.init';
import { CookieService } from 'ngx-cookie-service';

if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
    providers: [
        importProvidersFrom(AppRoutingModule, BrowserModule, NgbModule, RouterModule, OAuthModule.forRoot(), SharedModule.forRoot(), FormsModule, AgGridModule, DragDropModule, NgSelectModule, ApiModule.forRoot(() => {
            return new Configuration({
                basePath: `${environment.mainAppApiUrl}`,
            });
        })),
        CookieService,
        AppInitService,
        {
            provide: APP_INITIALIZER,
            useFactory: init_app,
            deps: [AppInitService],
            multi: true,
        },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpErrorInterceptor,
            multi: true,
        },
        {
            provide: ErrorHandler,
            useClass: GlobalErrorHandlerService,
        },
        DecimalPipe,
        CurrencyPipe,
        DatePipe,
        {
            provide: OAuthStorage,
            useClass: CookieStorageService,
        },
        provideAnimations(),
        provideAnimations(),
    ]
})
  .catch(err => console.error(err));
