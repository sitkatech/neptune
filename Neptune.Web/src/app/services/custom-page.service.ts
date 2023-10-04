import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CustomPageDetailedDto } from '../shared/models/custom-page-detailed-dto';
import { CustomPageWithRolesDto } from '../shared/models/custom-page-with-roles-dto';
import { CustomPageUpsertDto } from '../shared/models/custom-page-upsert-dto';
import { CustomPageRoleSimpleDto } from '../shared/generated/model/models';

@Injectable({
  providedIn: 'root'
})
export class CustomPageService {
  constructor(private apiService: ApiService, private httpClient: HttpClient) { }
  
  getCustomPageByID(customPageID: number): Observable<CustomPageDetailedDto> {
    let route = `/customPages/getByID/${customPageID}`;
    return this.apiService.getFromApi(route);
  }
  
  getCustomPageByVanityUrl(vanityUrl: string): Observable<CustomPageDetailedDto> {
    let route = `/customPages/getByURL/${vanityUrl}`;
    return this.apiService.getFromApi(route);
  }
  
  getAllCustomPageRoles(): Observable<Array<CustomPageRoleSimpleDto>> {
    let route = `/customPages/roles`;
    return this.apiService.getFromApi(route);
  }

  getCustomPageRolesByID(customPageID: number): Observable<Array<CustomPageRoleSimpleDto>> {
    let route = `/customPages/getByID/${customPageID}/roles`;
    return this.apiService.getFromApi(route);
  }
  
  getCustomPageRolesByVanityUrl(vanityUrl: string): Observable<Array<CustomPageRoleSimpleDto>> {
    let route = `/customPages/getByURL/${vanityUrl}/roles`;
    return this.apiService.getFromApi(route);
  }
  
  getAllCustomPages(): Observable<Array<CustomPageDetailedDto>> {
    let route = `/customPages`;
    return this.apiService.getFromApi(route);
  }

  getAllCustomPagesWithRoles(): Observable<Array<CustomPageWithRolesDto>>  {
    let route = `/customPages/withRoles`;
    return this.apiService.getFromApi(route);
  }
  
  createNewCustomPage(customPageToCreate: CustomPageUpsertDto): Observable<CustomPageDetailedDto> {
    let route = `/customPages`;
    return this.apiService.postToApi(route, customPageToCreate);
  }
  
  updateCustomPageByID(customPageID: number, updateDto: CustomPageUpsertDto): Observable<CustomPageDetailedDto> {
    let route = `/customPages/${customPageID}`;
    return this.apiService.putToApi(route, updateDto);
  }
  
  deleteCustomPageByID(customPageID: number): Observable<any> {
    let route = `/customPages/${customPageID}`;
    return this.apiService.deleteToApi(route);
  }

  uploadFile(file: any): Observable<any> {
    const apiUrl = environment.mainAppApiUrl
    const route = `${apiUrl}/FileResource/CkEditorUpload`;
    var result = this.httpClient.post<any>(
      route,
      file,
      {
        // NOTE: Because we are posting a Blob as the POST body, 
        // we have to include the Content-Type header. If we don't, 
        // the server will try to parse the body as plain text.
        headers: {
          "Content-Type": file.type
        },
        params: {
          clientFilename: file.name,
          mimeType: file.type
        }
      }
    );

    return result;
  }
}
