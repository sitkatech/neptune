import { Injectable } from '@angular/core';
import { ApiService } from '.';
import { Observable } from 'rxjs';
import { CustomRichTextDetailedDto } from '../models/custom-rich-text-detailed-dto';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomRichTextService {

  constructor(private apiService: ApiService, private httpClient: HttpClient) { }

  public getCustomRichText(customRichTextTypeID: number): Observable<CustomRichTextDetailedDto> {
    return this.apiService.getFromApi(`/customRichText/${customRichTextTypeID}`)
  }

  public updateCustomRichText(customRichTextTypeID: number, updateDto: CustomRichTextDetailedDto): Observable<CustomRichTextDetailedDto> {
    return this.apiService.putToApi(`customRichText/${customRichTextTypeID}`, updateDto);
  }

  uploadFile(file: any): Observable<any> {
    const mainAppApiUrl = environment.mainAppApiUrl
    const route = `${mainAppApiUrl}/FileResource/CkEditorUpload`;
    var result = this.httpClient.post<any>(
      route,
      file, // Send the File Blob as the POST body.
      {
        // NOTE: Because we are posting a Blob (File is a specialized Blob
        // object) as the POST body, we have to include the Content-Type
        // header. If we don't, the server will try to parse the body as
        // plain text.
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
