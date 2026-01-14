import { ApplicationConfig, ErrorHandler, importProvidersFrom, inject, provideAppInitializer } from "@angular/core";
import { RouterModule, TitleStrategy, provideRouter, withComponentInputBinding } from "@angular/router";

import { routes } from "./app.routes";
import { DecimalPipe, CurrencyPipe, DatePipe } from "@angular/common";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withInterceptorsFromDi } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { HttpErrorInterceptor } from "./shared/interceptors/httpErrorInterceptor";
import { GlobalErrorHandlerService } from "./shared/services/global-error-handler.service";
import { provideAnimations } from "@angular/platform-browser/animations";
import { ApiModule } from "./shared/generated/api.module";
import { Configuration } from "./shared/generated/configuration";
import { PageTitleStrategy } from "./strategies/page-title-strategy";
import { AppInitService } from "./app.init";
import { PhonePipe } from "./shared/pipes/phone.pipe";
import { GroupByPipe } from "./shared/pipes/group-by.pipe";
import { provideDialogConfig } from "@ngneat/dialog";
import { SumPipe } from "./shared/pipes/sum.pipe";
import { authHttpInterceptorFn, provideAuth0, AuthClientConfig } from "@auth0/auth0-angular";

const authExcludedApiRoutePrefixes = ["public"];

export const appConfig: ApplicationConfig = {
    providers: [
        provideRouter(routes, withComponentInputBinding()),
        AppInitService,
        provideAppInitializer(() => {
            // Wait for AppInitService to fetch runtime config, then populate AuthClientConfig
            const appInitService = inject(AppInitService);
            const authClientConfig = inject(AuthClientConfig);
            return appInitService.init().then(() => {
                const config = (window as any)?.config;
                const auth0 = config?.auth0 ?? {};
                // Set the Auth0 client config so the library can create its client after async load
                authClientConfig.set({
                    domain: auth0.domain,
                    clientId: auth0.clientId,
                    authorizationParams: {
                        redirect_uri: auth0.redirectUri ?? window.location.origin,
                        audience: auth0.audience,
                        scope: "openid profile email offline_access",
                    },
                    useRefreshTokens: true,
                    httpInterceptor: {
                        // Attach tokens to requests that are NOT in the excluded prefixes under the main API base path
                        allowedList: [
                            {
                                uriMatcher: (uri: string) => {
                                    const isExceptionUri = authExcludedApiRoutePrefixes.some((prefix) => uri.startsWith(`${config.mainAppApiUrl}/${prefix}`));
                                    return !isExceptionUri;
                                },
                            },
                        ],
                    },
                } as any);
            });
        }),
        // Provide Auth0 without static config - it will use AuthClientConfig which we set in the initializer above
        provideAuth0(),
        provideHttpClient(withInterceptorsFromDi(), withInterceptors([authHttpInterceptorFn])),
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
        provideAnimations(),
        { provide: TitleStrategy, useClass: PageTitleStrategy },
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
        GroupByPipe,
        SumPipe,
        provideDialogConfig({
            sizes: {
                sm: {
                    width: "100%",
                    maxWidth: "540px",
                    maxHeight: "90vh",
                },
                lg: {
                    width: "100%",
                    maxWidth: "1280px",
                    maxHeight: "90vh",
                },
            },
        }),
    ],
};

export function init_app(appLoadService: AppInitService) {
    return () => appLoadService.init();
}
