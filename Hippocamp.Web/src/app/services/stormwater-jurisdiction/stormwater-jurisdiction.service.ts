import { Injectable } from '@angular/core';
import { StormwaterJurisdictionSimpleDto } from 'src/app/shared/generated/model/stormwater-jurisdiction-simple-dto';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class StormwaterJurisdictionService {

  constructor(
    private apiService: ApiService
  ) { }

  getByPersonID(personID: number): Observable<Array<StormwaterJurisdictionSimpleDto>> {
    let route = `/jurisdictions/${personID}`;
    return this.apiService.getFromApi(route);
  }
}
