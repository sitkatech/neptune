import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectCreateDto } from 'src/app/shared/generated/model/project-create-dto';


@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private apiService: ApiService) { }

  getByID(projectID: number): Observable<ProjectSimpleDto> {
    let route = `/projects/${projectID}`;
    return this.apiService.getFromApi(route);
  }

  getProjectsByPersonID(personID: number): Observable<Array<ProjectSimpleDto>> {
    let route = `/projects/${personID}/listByPersonID`;
    return this.apiService.getFromApi(route);
  }

  newProject(projectModel: ProjectCreateDto) {
    let route = `/projects/new`;
    return this.apiService.postToApi(route, projectModel);
  }

  updateProject(projectID: number, projectModel: ProjectCreateDto) {
    let route = `/projects/${projectID}/update`;
    return this.apiService.postToApi(route, projectModel);
  }
}
