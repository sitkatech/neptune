import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectCreateDto } from 'src/app/shared/generated/model/project-create-dto';
import { ProjectDocumentDto } from 'src/app/shared/generated/model/project-document-dto';
import { ProjectDocumentUpsertDto } from 'src/app/shared/models/project-document-upsert-dto';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private apiService: ApiService, private httpClient: HttpClient) { }

  getByID(projectID: number): Observable<ProjectSimpleDto> {
    let route = `/projects/${projectID}`;
    return this.apiService.getFromApi(route);
  }

  getProjectsByPersonID(personID: number): Observable<Array<ProjectSimpleDto>> {
    let route = `/projects/${personID}/listByPersonID`;
    return this.apiService.getFromApi(route);
  }

  newProject(projectModel: ProjectCreateDto): Observable<ProjectSimpleDto> {
    let route = `/projects/new`;
    return this.apiService.postToApi(route, projectModel);
  }

  updateProject(projectID: number, projectModel: ProjectCreateDto) {
    let route = `/projects/${projectID}/update`;
    return this.apiService.postToApi(route, projectModel);
  }

  deleteProject(projectID: number) {
    let route = `/projects/${projectID}/delete`;
    return this.apiService.deleteToApi(route);
  }

  getAttachmentsByID(projectID: number): Observable<ProjectDocumentDto> {
    let route = `/projects/${projectID}/attachments`;
    return this.apiService.getFromApi(route);
  }

  addAttachmentByProjectID(projectID: number, attachment: ProjectDocumentUpsertDto): Observable<ProjectDocumentDto> {
    // we need to do it this way because the apiService.postToApi does a json.stringify, which won't work for input type="file"
    let formData = new FormData();
    formData.append("ProjectID", attachment.ProjectID.toString());
    formData.append("DisplayName", attachment.DisplayName);
    if(attachment.DocumentDescription !== undefined) {
      formData.append("DocumentDescription", attachment.DocumentDescription);
    }
    formData.append("FileResource", attachment.FileResource);
    
    const mainAppApiUrl = environment.mainAppApiUrl;
    const route = `${mainAppApiUrl}/projects/${projectID}/attachments`;
    var result = this.httpClient.post<any>(route, formData)
    .pipe(
      map((response: any) => {
        return this.apiService.handleResponse(response);
      }),
      catchError((error: any) => {
        return this.apiService.handleError(error);
      })
    );
    return result;
  }

  updateAttachmentByID(attachmentID: number, attachmentUpsert: ProjectDocumentUpsertDto): Observable<ProjectDocumentDto> {
    // we need to do it this way because the apiService.putToApi does a json.stringify, which won't work for input type="file"
    let formData = new FormData();
    formData.append("DisplayName", attachmentUpsert.DisplayName);
    if(attachmentUpsert.DocumentDescription !== undefined) {
      formData.append("DocumentDescription", attachmentUpsert.DocumentDescription);
    }
    formData.append("FileResource", attachmentUpsert.FileResource);
    
    const mainAppApiUrl = environment.mainAppApiUrl;
    const route = `${mainAppApiUrl}/projects/attachments/${attachmentID}/`;
    var result = this.httpClient.put<any>(route, formData)
    .pipe(
      map((response: any) => {
        return this.apiService.handleResponse(response);
      }),
      catchError((error: any) => {
        return this.apiService.handleError(error);
      })
    );
    return result;
  }

  deleteAttachmentByID(attachmentID: number) {
    let route = `/projects/attachments/${attachmentID}`;
    return this.apiService.deleteToApi(route);
  }
}
