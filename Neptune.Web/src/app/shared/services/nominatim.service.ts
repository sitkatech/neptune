import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class NominatimService {
    private baseURL = "https://nominatim.openstreetmap.org/search";

    constructor(private http: HttpClient) {}

    public makeNominatimRequest(q: string): Observable<any> {
        const url: string = `${this.baseURL}?q=${q}&format=geojson&polygon_kml=1&viewbox=-124.8360916,45.5437314,-116.9159938,49.0024392&bounded=1`;
        return this.http.get<any>(url);
    }
}
