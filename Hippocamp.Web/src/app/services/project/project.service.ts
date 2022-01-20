import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';


@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private apiService: ApiService) { }

  getProjects(): Observable<Array<ProjectSimpleDto>> {
    let route = `/projects`;
    return this.apiService.getFromApi(route);
}
}
