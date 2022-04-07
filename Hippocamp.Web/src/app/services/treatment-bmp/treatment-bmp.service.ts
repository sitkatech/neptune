import { Injectable } from '@angular/core';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { Observable } from 'rxjs';
import { ApiService } from 'src/app/shared/services';
import { TreatmentBMPTypeSimpleDto } from 'src/app/shared/generated/model/treatment-bmp-type-simple-dto';
import { TreatmentBMPModelingAttributeDropdownItemDto } from 'src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto';
import { GeometryGeoJSONAndAreaDto } from 'src/app/shared/generated/model/geometry-geo-json-and-area-dto';
import { TreatmentBMPDisplayDto } from 'src/app/shared/generated/model/models';


@Injectable({
  providedIn: 'root'
})
export class TreatmentBMPService {

  constructor(
    private apiService: ApiService
  ) { }

  getTreatmentBMPsByProjectID(projectID: number): Observable<Array<TreatmentBMPUpsertDto>> {
    let route = `/treatmentBMPs/${projectID}/getByProjectID`;
    return this.apiService.getFromApi(route);
  }

  getTypes(): Observable<Array<TreatmentBMPTypeSimpleDto>> {
    let route = `treatmentBMPs/types`;
    return this.apiService.getFromApi(route);
  }

  getModelingAttributesDropdownitems(): Observable<Array<TreatmentBMPModelingAttributeDropdownItemDto>> {
    let route = `treatmentBMPs/modelingAttributeDropdownItems`;
    return this.apiService.getFromApi(route);
  }

  getUpstreamRSBCatchmentGeoJSON(treatmentBMPID: Number): Observable<GeometryGeoJSONAndAreaDto> {
    let route = `treatmentBMPs/${treatmentBMPID}/upstreamRSBCatchmentGeoJSON`;
    return this.apiService.getFromApi(route);
  }

  mergeTreatmentBMPs(treamentBMPs: Array<TreatmentBMPUpsertDto>, projectID: number) {
    let route = `treatmentBMPs/${projectID}`;
    return this.apiService.putToApi(route, treamentBMPs);
  }

  getTreatmentBMPs() : Observable<Array<TreatmentBMPDisplayDto>> {
    let route = `treatmentBMPs`;
    return this.apiService.getFromApi(route);
  }
}
