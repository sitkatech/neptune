import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { FeatureCollection } from "geojson";
import { Observable, map } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: "root",
})
export class WfsService {
    constructor(private http: HttpClient) {}

    public getOCTAPrioritizationMetricsByCoordinate(longitude: number, latitude: number): Observable<FeatureCollection> {
        const url: string = `${environment.geoserverMapServiceUrl}/wms`;
        return this.http.get<FeatureCollection>(url, {
            params: {
                service: "WFS",
                version: "2.0",
                request: "GetFeature",
                outputFormat: "application/json",
                SrsName: "EPSG:4326",
                typeName: "Neptune:OCTAPrioritization",
                cql_filter: `intersects(OCTAPrioritizationGeometry, POINT(${latitude} ${longitude}))`,
            },
        });
    }

    public getRegionalSubbasins(): Observable<FeatureCollection> {
        var owsrootUrl = environment.geoserverMapServiceUrl + "/ows";

        var defaultParameters = {
            service: "WFS",
            version: "2.0",
            request: "GetFeature",
            typeName: "OCStormwater:RegionalSubbasins",
            outputFormat: "application/json",
            // format_options : 'callback:getJson',
            SrsName: "EPSG:4326",
        };
        return this.http.get<FeatureCollection>(owsrootUrl, {
            params: defaultParameters,
        });
    }

    public getGeoserverWFSLayer(layer: string, valueReference: string, bbox: string = ""): Observable<number[]> {
        const url: string = `${environment.geoserverMapServiceUrl}/ows`;
        const wfsParams = new HttpParams()
            .set("responseType", "json")
            .set("service", "wfs")
            .set("version", "2.0")
            .set("request", "GetFeature")
            .set("SrsName", "EPSG:4326")
            .set("typeName", layer)
            .set("outputFormat", "application/json")
            .set("valueReference", valueReference)
            .set("bbox", bbox);

        return this.http.post(url, wfsParams).pipe(
            map((rawData: any) => {
                return rawData.features;
            })
        );
    }

    public getGeoserverWFSLayerWithCQLFilter(layer: string, cqlFilter: string, valueReference: string): Observable<number[]> {
        const url: string = `${environment.geoserverMapServiceUrl}/ows`;
        const wfsParams = new HttpParams()
            .set("responseType", "json")
            .set("service", "wfs")
            .set("version", "2.0")
            .set("request", "GetFeature")
            .set("SrsName", "EPSG:4326")
            .set("typeName", layer)
            .set("outputFormat", "application/json")
            .set("valueReference", valueReference)
            .set("cql_filter", cqlFilter);

        return this.http.post(url, wfsParams).pipe(
            map((rawData: any) => {
                return rawData.features;
            })
        );
    }

    public getTrashGeneratingUnitByCoordinate(longitude: number, latitude: number): Observable<FeatureCollection> {
        const url: string = `${environment.geoserverMapServiceUrl}/wms`;
        return this.http.get<FeatureCollection>(url, {
            params: {
                service: "WFS",
                version: "2.0",
                request: "GetFeature",
                outputFormat: "application/json",
                SrsName: "EPSG:4326",
                typeName: "TrashGeneratingUnits",
                cql_filter: `intersects(TrashGeneratingUnitGeometry, POINT(${latitude} ${longitude}))`,
            },
        });
    }

    public getParcelByCoordinate(longitude: number, latitude: number): Observable<FeatureCollection> {
        const url: string = `${environment.geoserverMapServiceUrl}/wms`;
        return this.http.get<FeatureCollection>(url, {
            params: {
                service: "WFS",
                version: "2.0",
                request: "GetFeature",
                outputFormat: "application/json",
                SrsName: "EPSG:4326",
                typeName: "Parcels",
                cql_filter: `intersects(ParcelGeometry, POINT(${latitude} ${longitude}))`,
            },
        });
    }
}
