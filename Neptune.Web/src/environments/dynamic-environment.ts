declare var window: any;

export class DynamicEnvironment {
    private _production: boolean;

    constructor(_production: boolean) {
        this._production = _production;
    }

    public get production() {
        return (window?.config?.production ?? this._production) as boolean;
    }

    public get staging() {
        return window?.config?.staging ?? false;
    }

    public get dev() {
        return window?.config?.dev ?? false;
    }

    public get mainAppApiUrl() {
        return window?.config?.mainAppApiUrl ?? "";
    }

    public get geoserverMapServiceUrl() {
        return window?.config?.geoserverMapServiceUrl ?? "";
    }

    public get auth0() {
        const cfg = window?.config ?? null;
        return cfg?.auth0 ?? null;
    }

    public get ocStormwaterToolsBaseUrl() {
        return window?.config?.ocStormwaterToolsBaseUrl ?? "";
    }
}
