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
        const url: string = `${this.baseURL}?q=${q}&format=geojson&polygon_kml=1&viewbox=-118.09341430664051,33.46459577300336,-117.5193786621095,33.844679670212059&bounded=1`;
        return this.http.get<any>(url);
    }
}
