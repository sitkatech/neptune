import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FeatureCollection} from "geojson";
import {Observable, Subject} from "rxjs";
import {map, takeUntil} from "rxjs/operators";
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class WfsService {

    private getWatershedIDsIntersectingUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private http: HttpClient,
    ) {
    }

    public getWatershedByCoordinate(longitude: number, latitude: number): Observable<FeatureCollection> {
        const url: string = `${environment.geoserverMapServiceUrl}/wms`;
        return this.http.get<FeatureCollection>(url, {
            params: {
                service: 'WFS',
                version: '2.0',
                request: 'GetFeature',
                outputFormat: 'application/json',
                SrsName: 'EPSG:4326',
                typeName: 'Hippocamp:Watersheds',
                cql_filter: `intersects(WatershedGeometry4326, POINT(${latitude} ${longitude}))`
            }
        });
    }
    
    public getWatershedIdsIntersecting(lon1: number, lon2: number, lat1: number, lat2: number): Observable<number[]> {
        this.getWatershedIDsIntersectingUnsubscribe.next();

        const url: string = `${environment.geoserverMapServiceUrl}/wms`;
        return this.http.get(url, {
            responseType: "text",
            params: {
                service: "wfs",
                version: "2.0",
                request: "GetPropertyValue",
                SrsName: "EPSG:4326",
                typeName: "Hippocamp:Watersheds",
                valueReference: "WatershedID",
                cql_filter: `bbox(WatershedGeometry4326,${lat1},${lon1},${lat2},${lon2})`,
            },
        })
            .pipe(
                takeUntil(this.getWatershedIDsIntersectingUnsubscribe),
                map((rawData: string) => {
                    // Parse XML to retrieve nodes
                    const watershedIDNodes: HTMLCollection = new DOMParser()
                        .parseFromString(rawData, "text/xml")
                        .getElementsByTagName("heartwood:watershedId");

                    const watershedIDs: number[] = [];
                    for (let i = 0; i < watershedIDNodes.length; i++) {
                        watershedIDs.push(parseInt(watershedIDNodes[i].innerHTML))
                    }
                    return watershedIDs;
                })
            );

    }
}
