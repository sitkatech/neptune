import { APP_INITIALIZER, ApplicationConfig, ErrorHandler, importProvidersFrom } from "@angular/core";
import { RouterModule, TitleStrategy, provideRouter } from "@angular/router";

import { routes } from "./app.routes";
import { DecimalPipe, CurrencyPipe, DatePipe, PercentPipe } from "@angular/common";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { HttpErrorInterceptor } from "./shared/interceptors/httpErrorInterceptor";
import { GlobalErrorHandlerService } from "./shared/services/global-error-handler.service";
import { provideAnimations } from "@angular/platform-browser/animations";
import { ApiModule } from "./shared/generated/api.module";
import { Configuration } from "./shared/generated/configuration";
import { PageTitleStrategy } from "./strategies/page-title-strategy";
import { AppInitService } from "./app.init";
import { AuthInterceptor } from "./shared/interceptors/auth-interceptor";
import { CookieService } from "ngx-cookie-service";
import { CookieStorageService } from "./shared/services/cookies/cookie-storage.service";
import { OAuthStorage, OAuthModule } from "angular-oauth2-oidc";
import { PhonePipe } from "./shared/pipes/phone.pipe";

export const appConfig: ApplicationConfig = {
    providers: [
        provideRouter(routes),
        importProvidersFrom(
            ApiModule.forRoot(() => {
                return new Configuration({
                    basePath: `${environment.mainAppApiUrl}`,
                });
            })
        ),
        importProvidersFrom(
            RouterModule.forRoot(routes, {
                paramsInheritanceStrategy: "always",
                scrollPositionRestoration: "enabled",
                anchorScrolling: "enabled",
            })
        ),
        importProvidersFrom(OAuthModule.forRoot()),
        provideHttpClient(withInterceptorsFromDi()),
        provideAnimations(),
        { provide: TitleStrategy, useClass: PageTitleStrategy },
        {
            provide: ErrorHandler,
            useClass: GlobalErrorHandlerService,
        },
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
        PhonePipe,
        {
            provide: OAuthStorage,
            useClass: CookieStorageService,
        },
    ],
};

export function init_app(appLoadService: AppInitService) {
    return () => appLoadService.init();
}
