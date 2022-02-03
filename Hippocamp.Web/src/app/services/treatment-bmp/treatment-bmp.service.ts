import { Injectable } from '@angular/core';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { Observable } from 'rxjs';
import { ApiService } from 'src/app/shared/services';


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
}
