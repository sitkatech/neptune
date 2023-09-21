import { Injectable } from '@angular/core';
import { ApiService } from '.';
import { Observable } from 'rxjs';
import { FieldDefinitionDto } from '../generated/model/models';

@Injectable({
  providedIn: 'root'
})
export class FieldDefinitionService {

  constructor(private apiService: ApiService) { }

  public listAllFieldDefinitions(): Observable<Array<FieldDefinitionDto>> {
    return this.apiService.getFromApi(`/fieldDefinitions`);
  }

  public getFieldDefinition(fieldDefinitionTypeID: number): Observable<FieldDefinitionDto> {
    return this.apiService.getFromApi(`/fieldDefinitions/${fieldDefinitionTypeID}`);
  }

  public updateFieldDefinition(fieldDefinition: FieldDefinitionDto): Observable<FieldDefinitionDto> {
    return this.apiService.putToApi(`fieldDefinitions/${fieldDefinition.FieldDefinitionType.FieldDefinitionTypeID}`, fieldDefinition);
  }
}