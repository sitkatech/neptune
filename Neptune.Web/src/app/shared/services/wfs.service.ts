import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FeatureCollection} from "geojson";
import {Observable} from "rxjs";
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class WfsService {
    constructor(
        private http: HttpClient,
    ) {
    }

    public getOCTAPrioritizationMetricsByCoordinate(longitude: number, latitude: number): Observable<FeatureCollection> {
        const url: string = `${environment.geoserverMapServiceUrl}/wms`;
        return this.http.get<FeatureCollection>(url, {
            params: {
                service: 'WFS',
                version: '2.0',
                request: 'GetFeature',
                outputFormat: 'application/json',
                SrsName: 'EPSG:4326',
                typeName: 'Neptune:OCTAPrioritization',
                cql_filter: `intersects(OCTAPrioritizationGeometry, POINT(${latitude} ${longitude}))`
            }
        });
    }

    public getRegionalSubbasins(): Observable<FeatureCollection> {
        var owsrootUrl = environment.geoserverMapServiceUrl + '/ows';

        var defaultParameters = {
            service : 'WFS',
            version : '2.0',
            request : 'GetFeature',
            typeName : 'OCStormwater:RegionalSubbasins',
            outputFormat : 'application/json',
            // format_options : 'callback:getJson',
            SrsName : 'EPSG:4326'
        };
        return this.http.get<FeatureCollection>(owsrootUrl, {
            params: defaultParameters
        });
    }
}
