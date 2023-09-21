import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrganizationSimpleDto } from 'src/app/shared/generated/model/organization-simple-dto';
import { ApiService } from 'src/app/shared/services';


@Injectable({
  providedIn: 'root'
})
export class OrganizationService {

  constructor(
    private apiService: ApiService
  ) { }

  getAllOrganizations(): Observable<Array<OrganizationSimpleDto>> {
    let route = `/organizations`;
    return this.apiService.getFromApi(route);
  }
}
