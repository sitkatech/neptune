import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectUpsertDto } from 'src/app/shared/generated/model/project-upsert-dto';
import { ProjectDocumentUpsertDto } from 'src/app/shared/models/project-document-upsert-dto';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { ProjectDocumentSimpleDto } from 'src/app/shared/generated/model/project-document-simple-dto';
import { ProjectDocumentUpdateDto } from 'src/app/shared/models/project-document-update-dto';
import { DelineationSimpleDto, DelineationUpsertDto, ProjectLoadReducingResultDto, ProjectNetworkSolveHistorySimpleDto, TreatmentBMPHRUCharacteristicsSummarySimpleDto} from 'src/app/shared/generated/model/models';


@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private apiService: ApiService, private httpClient: HttpClient) { }

  getByID(projectID: number): Observable<ProjectSimpleDto> {
    let route = `/projects/${projectID}`;
    return this.apiService.getFromApi(route);
  }

  getProjectsByPersonID(): Observable<Array<ProjectSimpleDto>> {
    let route = `/projects`;
    return this.apiService.getFromApi(route);
  }

  newProject(projectModel: ProjectUpsertDto): Observable<ProjectSimpleDto> {
    let route = `/projects/new`;
    return this.apiService.postToApi(route, projectModel);
  }

  newProjectCopy(projectID: number): Observable<number> {
    let route = `/projects/${projectID}/copy`;
    return this.apiService.postToApi(route, {});
  }

  updateProject(projectID: number, projectModel: ProjectUpsertDto) {
    let route = `/projects/${projectID}/update`;
    return this.apiService.postToApi(route, projectModel);
  }

  deleteProject(projectID: number) {
    let route = `/projects/${projectID}/delete`;
    return this.apiService.deleteToApi(route);
  }

  getAttachmentsByProjectID(projectID: number): Observable<Array<ProjectDocumentSimpleDto>> {
    let route = `/projects/${projectID}/attachments`;
    return this.apiService.getFromApi(route);
  }

  getAttachmentByID(attachmentID: number): Observable<ProjectDocumentSimpleDto> {
    let route = `/projects/attachments/${attachmentID}`;
    return this.apiService.getFromApi(route);
  }

  addAttachmentByProjectID(projectID: number, attachment: ProjectDocumentUpsertDto): Observable<ProjectDocumentSimpleDto> {
    // we need to do it this way because the apiService.postToApi does a json.stringify, which won't work for input type="file"
    let formData = new FormData();
    formData.append("ProjectID", attachment.ProjectID.toString());
    if(attachment.DisplayName !== undefined) {
      formData.append("DisplayName", attachment.DisplayName);
    }
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

  updateAttachmentByID(attachmentID: number, attachmentUpdate: ProjectDocumentUpdateDto): Observable<ProjectDocumentSimpleDto> {
    let route = `/projects/attachments/${attachmentID}`;
    return this.apiService.putToApi(route, attachmentUpdate);
  }

  deleteAttachmentByID(attachmentID: number) {
    let route = `/projects/attachments/${attachmentID}`;
    return this.apiService.deleteToApi(route);
  }

  getLoadReducingResultsByProjectID(projectID: number): Observable<Array<ProjectLoadReducingResultDto>> {
    let route = `/projects/${projectID}/load-reducing-results`;
    return this.apiService.getFromApi(route);
  }

  triggerNetworkSolveByProjectID(projectID: number): Observable<any> {
    let route = `/projects/${projectID}/modeled-performance`;
    return this.apiService.postToApi(route, {});
  }

  getNetworkSolveHistoriesByProjectID(projectID: number): Observable<ProjectNetworkSolveHistorySimpleDto[]> {
    let route = `/projects/${projectID}/project-network-solve-histories`;
    return this.apiService.getFromApi(route);
  }

  getTreatmentBMPHRUCharacteristicSummariesByProjectID(projectID: number): Observable<TreatmentBMPHRUCharacteristicsSummarySimpleDto[]> {
    let route = `/projects/${projectID}/treatment-bmp-hru-characteristics`;
    return this.apiService.getFromApi(route);
  }

  getDelineationsByProjectID(projectID: number): Observable<Array<DelineationUpsertDto>> {
    let route = `/projects/${projectID}/delineations`;
    return this.apiService.getFromApi(route);
  }

  mergeDelineationsByProjectID(delineations: Array<DelineationUpsertDto>, projectID: number) {
    let route = `projects/${projectID}/delineations`;
    return this.apiService.putToApi(route, delineations);
  }

  getProjectsSharedWithOCTAM2Tier2GrantProgram(): Observable<any> {
    let route = 'projects/OCTAM2Tier2GrantProgram';
    return this.apiService.getFromApi(route);
  }

  getTreatmentBMPsSharedWithOCTAM2Tier2GrantProgram(): Observable<any> {
    let route = 'projects/OCTAM2Tier2GrantProgram/treatmentBMPs';
    return this.apiService.getFromApi(route);
  }

  downloadProjectModelResults(): Observable<Blob> {
    let route = `${environment.mainAppApiUrl}/projects/download`;
    return this.httpClient.get(route, { responseType: "blob"});
  }

  downloadBMPModelResults(): Observable<Blob> {
    let route = `${environment.mainAppApiUrl}/projects/treatmentBMPs/download`;
    return this.httpClient.get(route, { responseType: "blob"});
  }

  downloadOCTAM2Tier2GrantProgramProjectModelResults(): Observable<Blob> {
    let route = `${environment.mainAppApiUrl}/projects/OCTAM2Tier2GrantProgram/download`;
    return this.httpClient.get(route, { responseType: "blob"});
  }

  downloadOCTAM2Tier2GrantProgramBMPModelResults(): Observable<Blob> {
    let route = `${environment.mainAppApiUrl}/projects/OCTAM2Tier2GrantProgram/treatmentBMPs/download`;
    return this.httpClient.get(route, { responseType: "blob"});
  }

  getAllDelineations() : Observable<Array<DelineationSimpleDto>> {
    let route = `projects/delineations`;
    return this.apiService.getFromApi(route);
  }
}
