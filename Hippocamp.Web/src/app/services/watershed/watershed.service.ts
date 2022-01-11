import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { BoundingBoxDto } from 'src/app/shared/models/bounding-box-dto';
import { WatershedDto } from 'src/app/shared/generated/model/models';

@Injectable({
  providedIn: 'root'
})
export class WatershedService {
  constructor(private apiService: ApiService) { }

  getWatershedByWatershedID(watershedID: number): Observable<WatershedDto> {
    let route = `/watersheds/${watershedID}`;
    return this.apiService.getFromApi(route);
  }

  getBoundingBoxByWatershedIDs(watershedIDs: Array<number>): Observable<BoundingBoxDto> {
    let route = `/watersheds/getBoundingBox`;
    let watershedIDListDto = { watershedIDs: watershedIDs };
    return this.apiService.postToApi(route, watershedIDListDto);
  }

  getWatersheds(): Observable<Array<WatershedDto>> {
    let route = `/watersheds`;
    return this.apiService.getFromApi(route);
  }
}
