import { Injectable } from '@angular/core';
import { StormwaterJurisdictionSimpleDto } from 'src/app/shared/generated/model/stormwater-jurisdiction-simple-dto';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { BoundingBoxDto } from 'src/app/shared/generated/model/bounding-box-dto';


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

  getBoundingBoxByProjectID(projectID: number): Observable<BoundingBoxDto> {
    let route = `/jurisdictions/${projectID}/getBoundingBoxByProjectID`;
    return this.apiService.getFromApi(route);
  }

  getBoundingBoxByLoggedInPerson(): Observable<BoundingBoxDto> {
    let route = `/jurisdictions/boundingBox`;
    return this.apiService.getFromApi(route);
  }
}
