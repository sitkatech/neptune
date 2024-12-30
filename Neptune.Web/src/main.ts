import { AppComponent } from "./app/app.component";
import { createApplication } from "@angular/platform-browser";
import { appConfig } from "./app/app.config";

(async () => {
    const app = createApplication(appConfig);
    (await app).bootstrap(AppComponent);
    //  (await app).bootstrap(MsalRedirectComponent);

    // todo: example of creating a custom popup in leaflet
    // const wriaPopupComponent = createCustomElement(WaterResourceInventoryAreaPopupComponent, {
    //     injector: (await app).injector,
    // });
    // customElements.define("water-resource-inventory-area-popup-custom-element", wriaPopupComponent);
})();
