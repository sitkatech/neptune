import { Injectable } from '@angular/core';
import { ApplicationInsights, ITelemetryItem, IDependencyTelemetry } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppInsightsService {

  constructor() { }

  initAppInsights() {
    const appInsights = new ApplicationInsights({
      config: {
        appId: "Neptune",
        enableAutoRouteTracking: true, 
        disableFetchTracking: false,
        enableCorsCorrelation: true,
        enableRequestHeaderTracking: true,
        enableResponseHeaderTracking: true,
        correlationHeaderExcludedDomains: [new URL(environment.keystoneAuthConfiguration.issuer).hostname, new URL(environment.geoserverMapServiceUrl).hostname],
        instrumentationKey: environment.appInsightsInstrumentationKey,
        maxAjaxCallsPerView: -1,
      }
    });

    appInsights.loadAppInsights();

    appInsights.addTelemetryInitializer((item: ITelemetryItem) => {
      if (
        item &&
        item.baseData &&
        [0, 401].indexOf((item.baseData as IDependencyTelemetry).responseCode) >= 0
      ) {
        return false;
      }
    }); 
    appInsights.addTelemetryInitializer((envelope) => {
      envelope.tags["ai.cloud.role"] = environment.keystoneAuthConfiguration.clientId + ".Web";
    });

    (window as any).appInsights = appInsights;
  }
}
