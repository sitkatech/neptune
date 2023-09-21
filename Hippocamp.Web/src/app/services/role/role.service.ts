import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { RoleDto } from 'src/app/shared/generated/model/models';

@Injectable({
    providedIn: 'root'
})
export class RoleService {

    constructor(private apiService: ApiService) { }
        
    getRoles(): Observable<Array<RoleDto>> {
        let route = `/roles`;
        return this.apiService.getFromApi(route);
    }
}