/**
 * Neptune.API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
/* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { GeometryGeoJSONAndAreaDto } from '../model/geometry-geo-json-and-area-dto';
import { TreatmentBMPDisplayDto } from '../model/treatment-bmp-display-dto';
import { TreatmentBMPModelingAttributeDropdownItemDto } from '../model/treatment-bmp-modeling-attribute-dropdown-item-dto';
import { TreatmentBMPTypeWithModelingAttributesDto } from '../model/treatment-bmp-type-with-modeling-attributes-dto';
import { TreatmentBMPUpsertDto } from '../model/treatment-bmp-upsert-dto';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';
import { catchError } from 'rxjs/operators';
import { ApiService } from '../../services';


@Injectable({
  providedIn: 'root'
})
export class TreatmentBMPService {

    protected basePath = 'http://localhost';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration
    , private apiService: ApiService) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPModelingAttributeDropdownItemsGet(observe?: 'body', reportProgress?: boolean): Observable<Array<TreatmentBMPModelingAttributeDropdownItemDto>>;
    public treatmentBMPModelingAttributeDropdownItemsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<TreatmentBMPModelingAttributeDropdownItemDto>>>;
    public treatmentBMPModelingAttributeDropdownItemsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<TreatmentBMPModelingAttributeDropdownItemDto>>>;
    public treatmentBMPModelingAttributeDropdownItemsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json',
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<TreatmentBMPModelingAttributeDropdownItemDto>>(`${this.basePath}/treatmentBMPModelingAttributeDropdownItems`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPTypesGet(observe?: 'body', reportProgress?: boolean): Observable<Array<TreatmentBMPTypeWithModelingAttributesDto>>;
    public treatmentBMPTypesGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<TreatmentBMPTypeWithModelingAttributesDto>>>;
    public treatmentBMPTypesGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<TreatmentBMPTypeWithModelingAttributesDto>>>;
    public treatmentBMPTypesGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json',
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<TreatmentBMPTypeWithModelingAttributesDto>>(`${this.basePath}/treatmentBMPTypes`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPsGet(observe?: 'body', reportProgress?: boolean): Observable<Array<TreatmentBMPDisplayDto>>;
    public treatmentBMPsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<TreatmentBMPDisplayDto>>>;
    public treatmentBMPsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<TreatmentBMPDisplayDto>>>;
    public treatmentBMPsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json',
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<TreatmentBMPDisplayDto>>(`${this.basePath}/treatmentBMPs`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

    /**
     * 
     * 
     * @param projectID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPsProjectIDGetByProjectIDGet(projectID: number, observe?: 'body', reportProgress?: boolean): Observable<Array<TreatmentBMPUpsertDto>>;
    public treatmentBMPsProjectIDGetByProjectIDGet(projectID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<TreatmentBMPUpsertDto>>>;
    public treatmentBMPsProjectIDGetByProjectIDGet(projectID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<TreatmentBMPUpsertDto>>>;
    public treatmentBMPsProjectIDGetByProjectIDGet(projectID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (projectID === null || projectID === undefined) {
            throw new Error('Required parameter projectID was null or undefined when calling treatmentBMPsProjectIDGetByProjectIDGet.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json',
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<TreatmentBMPUpsertDto>>(`${this.basePath}/treatmentBMPs/${encodeURIComponent(String(projectID))}/getByProjectID`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

    /**
     * 
     * 
     * @param projectID 
     * @param treatmentBMPUpsertDto 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPsProjectIDPut(projectID: number, treatmentBMPUpsertDto?: Array<TreatmentBMPUpsertDto>, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public treatmentBMPsProjectIDPut(projectID: number, treatmentBMPUpsertDto?: Array<TreatmentBMPUpsertDto>, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public treatmentBMPsProjectIDPut(projectID: number, treatmentBMPUpsertDto?: Array<TreatmentBMPUpsertDto>, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public treatmentBMPsProjectIDPut(projectID: number, treatmentBMPUpsertDto?: Array<TreatmentBMPUpsertDto>, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (projectID === null || projectID === undefined) {
            throw new Error('Required parameter projectID was null or undefined when calling treatmentBMPsProjectIDPut.');
        }


        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json',
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.put<any>(`${this.basePath}/treatmentBMPs/${encodeURIComponent(String(projectID))}`,
            treatmentBMPUpsertDto,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

    /**
     * 
     * 
     * @param treatmentBMPID 
     * @param treatmentBMPTypeID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut(treatmentBMPID: number, treatmentBMPTypeID: number, observe?: 'body', reportProgress?: boolean): Observable<number>;
    public treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut(treatmentBMPID: number, treatmentBMPTypeID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<number>>;
    public treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut(treatmentBMPID: number, treatmentBMPTypeID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<number>>;
    public treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut(treatmentBMPID: number, treatmentBMPTypeID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (treatmentBMPID === null || treatmentBMPID === undefined) {
            throw new Error('Required parameter treatmentBMPID was null or undefined when calling treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut.');
        }

        if (treatmentBMPTypeID === null || treatmentBMPTypeID === undefined) {
            throw new Error('Required parameter treatmentBMPTypeID was null or undefined when calling treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json',
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.put<number>(`${this.basePath}/treatmentBMPs/${encodeURIComponent(String(treatmentBMPID))}/treatmentBMPType/${encodeURIComponent(String(treatmentBMPTypeID))}`,
            null,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

    /**
     * 
     * 
     * @param treatmentBMPID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPsTreatmentBMPIDUpstreamRSBCatchmentGeoJSONGet(treatmentBMPID: number, observe?: 'body', reportProgress?: boolean): Observable<GeometryGeoJSONAndAreaDto>;
    public treatmentBMPsTreatmentBMPIDUpstreamRSBCatchmentGeoJSONGet(treatmentBMPID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<GeometryGeoJSONAndAreaDto>>;
    public treatmentBMPsTreatmentBMPIDUpstreamRSBCatchmentGeoJSONGet(treatmentBMPID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<GeometryGeoJSONAndAreaDto>>;
    public treatmentBMPsTreatmentBMPIDUpstreamRSBCatchmentGeoJSONGet(treatmentBMPID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (treatmentBMPID === null || treatmentBMPID === undefined) {
            throw new Error('Required parameter treatmentBMPID was null or undefined when calling treatmentBMPsTreatmentBMPIDUpstreamRSBCatchmentGeoJSONGet.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json',
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<GeometryGeoJSONAndAreaDto>(`${this.basePath}/treatmentBMPs/${encodeURIComponent(String(treatmentBMPID))}/upstreamRSBCatchmentGeoJSON`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public treatmentBMPsVerifiedGet(observe?: 'body', reportProgress?: boolean): Observable<Array<TreatmentBMPDisplayDto>>;
    public treatmentBMPsVerifiedGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<TreatmentBMPDisplayDto>>>;
    public treatmentBMPsVerifiedGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<TreatmentBMPDisplayDto>>>;
    public treatmentBMPsVerifiedGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json',
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<TreatmentBMPDisplayDto>>(`${this.basePath}/treatmentBMPs/verified`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

}
