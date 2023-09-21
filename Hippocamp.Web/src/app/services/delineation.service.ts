import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DelineationSimpleDto } from '../shared/generated/model/delineation-simple-dto';
import { ApiService } from '../shared/services';

@Injectable({
  providedIn: 'root'
})
export class DelineationService {

  constructor(
    private apiService: ApiService
  ) { }

  getDelineations() : Observable<Array<DelineationSimpleDto>> {
    let route = `delineations`;
    return this.apiService.getFromApi(route);
  }
}
