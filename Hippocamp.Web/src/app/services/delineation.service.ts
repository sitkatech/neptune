import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DelineationUpsertDto } from '../shared/generated/model/delineation-upsert-dto';
import { ApiService } from '../shared/services';

@Injectable({
  providedIn: 'root'
})
export class DelineationService {

  constructor(
    private apiService: ApiService
  ) { }

  getDelineationsByProjectID(projectID: number): Observable<Array<DelineationUpsertDto>> {
    let route = `/delineations/${projectID}/getByProjectID`;
    return this.apiService.getFromApi(route);
  }
}
