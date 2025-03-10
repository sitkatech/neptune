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

import { OnlandVisualTrashAssessmentDetailDto } from '../model/onland-visual-trash-assessment-detail-dto';
import { OnlandVisualTrashAssessmentGridDto } from '../model/onland-visual-trash-assessment-grid-dto';
import { OnlandVisualTrashAssessmentObservationUpsertDto } from '../model/onland-visual-trash-assessment-observation-upsert-dto';
import { OnlandVisualTrashAssessmentSimpleDto } from '../model/onland-visual-trash-assessment-simple-dto';
import { OnlandVisualTrashAssessmentWorkflowDto } from '../model/onland-visual-trash-assessment-workflow-dto';
import { OnlandVisualTrashAssessmentWorkflowProgressDto } from '../model/onland-visual-trash-assessment-workflow-progress-dto';
import { PreliminarySourceIdentificationTypeSimpleDto } from '../model/preliminary-source-identification-type-simple-dto';
import { ProblemDetails } from '../model/problem-details';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';
import { catchError } from 'rxjs/operators';
import { ApiService } from '../../services';


@Injectable({
  providedIn: 'root'
})
export class OnlandVisualTrashAssessmentService {

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
    public onlandVisualTrashAssessmentsGet(observe?: 'body', reportProgress?: boolean): Observable<Array<OnlandVisualTrashAssessmentGridDto>>;
    public onlandVisualTrashAssessmentsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<OnlandVisualTrashAssessmentGridDto>>>;
    public onlandVisualTrashAssessmentsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<OnlandVisualTrashAssessmentGridDto>>>;
    public onlandVisualTrashAssessmentsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.get<Array<OnlandVisualTrashAssessmentGridDto>>(`${this.basePath}/onland-visual-trash-assessments`,
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
     * @param onlandVisualTrashAssessmentAreaID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'body', reportProgress?: boolean): Observable<Array<OnlandVisualTrashAssessmentGridDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<OnlandVisualTrashAssessmentGridDto>>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<OnlandVisualTrashAssessmentGridDto>>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet.');
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

        return this.httpClient.get<Array<OnlandVisualTrashAssessmentGridDto>>(`${this.basePath}/onland-visual-trash-assessments/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}`,
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
     * @param onlandVisualTrashAssessmentID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(onlandVisualTrashAssessmentID: number, observe?: 'body', reportProgress?: boolean): Observable<OnlandVisualTrashAssessmentDetailDto>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(onlandVisualTrashAssessmentID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<OnlandVisualTrashAssessmentDetailDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(onlandVisualTrashAssessmentID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<OnlandVisualTrashAssessmentDetailDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(onlandVisualTrashAssessmentID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentID === null || onlandVisualTrashAssessmentID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentID was null or undefined when calling onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet.');
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

        return this.httpClient.get<OnlandVisualTrashAssessmentDetailDto>(`${this.basePath}/onland-visual-trash-assessments/${encodeURIComponent(String(onlandVisualTrashAssessmentID))}`,
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
     * @param onlandVisualTrashAssessmentID 
     * @param file 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationPhotoStagingPost(onlandVisualTrashAssessmentID: number, file?: Blob, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationPhotoStagingPost(onlandVisualTrashAssessmentID: number, file?: Blob, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationPhotoStagingPost(onlandVisualTrashAssessmentID: number, file?: Blob, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationPhotoStagingPost(onlandVisualTrashAssessmentID: number, file?: Blob, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentID === null || onlandVisualTrashAssessmentID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentID was null or undefined when calling onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationPhotoStagingPost.');
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
            'multipart/form-data',
        ];

        const canConsumeForm = this.canConsumeForm(consumes);

        let formParams: { append(param: string, value: any): void | HttpParams; };
        let useForm = false;
        let convertFormParamsToString = false;
        // use FormData to transmit files using content-type "multipart/form-data"
        // see https://stackoverflow.com/questions/4007969/application-x-www-form-urlencoded-or-multipart-form-data
        useForm = canConsumeForm;
        if (useForm) {
            formParams = new FormData();
        } else {
            formParams = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        }

        if (file !== undefined) {
            formParams = formParams.append('file', <any>file) || formParams;
        }

        return this.httpClient.post<any>(`${this.basePath}/onland-visual-trash-assessments/${encodeURIComponent(String(onlandVisualTrashAssessmentID))}/observation-photo-staging`,
            convertFormParamsToString ? formParams.toString() : formParams,
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
     * @param onlandVisualTrashAssessmentID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(onlandVisualTrashAssessmentID: number, observe?: 'body', reportProgress?: boolean): Observable<Array<OnlandVisualTrashAssessmentObservationUpsertDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(onlandVisualTrashAssessmentID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<OnlandVisualTrashAssessmentObservationUpsertDto>>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(onlandVisualTrashAssessmentID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<OnlandVisualTrashAssessmentObservationUpsertDto>>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(onlandVisualTrashAssessmentID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentID === null || onlandVisualTrashAssessmentID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentID was null or undefined when calling onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet.');
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

        return this.httpClient.get<Array<OnlandVisualTrashAssessmentObservationUpsertDto>>(`${this.basePath}/onland-visual-trash-assessments/${encodeURIComponent(String(onlandVisualTrashAssessmentID))}/observations`,
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
     * @param onlandVisualTrashAssessmentID 
     * @param onlandVisualTrashAssessmentObservationUpsertDto 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(onlandVisualTrashAssessmentID: number, onlandVisualTrashAssessmentObservationUpsertDto?: Array<OnlandVisualTrashAssessmentObservationUpsertDto>, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(onlandVisualTrashAssessmentID: number, onlandVisualTrashAssessmentObservationUpsertDto?: Array<OnlandVisualTrashAssessmentObservationUpsertDto>, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(onlandVisualTrashAssessmentID: number, onlandVisualTrashAssessmentObservationUpsertDto?: Array<OnlandVisualTrashAssessmentObservationUpsertDto>, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(onlandVisualTrashAssessmentID: number, onlandVisualTrashAssessmentObservationUpsertDto?: Array<OnlandVisualTrashAssessmentObservationUpsertDto>, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentID === null || onlandVisualTrashAssessmentID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentID was null or undefined when calling onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost.');
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

        return this.httpClient.post<any>(`${this.basePath}/onland-visual-trash-assessments/${encodeURIComponent(String(onlandVisualTrashAssessmentID))}/observations`,
            onlandVisualTrashAssessmentObservationUpsertDto,
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
     * @param onlandVisualTrashAssessmentID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet(onlandVisualTrashAssessmentID: number, observe?: 'body', reportProgress?: boolean): Observable<OnlandVisualTrashAssessmentWorkflowProgressDto>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet(onlandVisualTrashAssessmentID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<OnlandVisualTrashAssessmentWorkflowProgressDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet(onlandVisualTrashAssessmentID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<OnlandVisualTrashAssessmentWorkflowProgressDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet(onlandVisualTrashAssessmentID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentID === null || onlandVisualTrashAssessmentID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentID was null or undefined when calling onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet.');
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

        return this.httpClient.get<OnlandVisualTrashAssessmentWorkflowProgressDto>(`${this.basePath}/onland-visual-trash-assessments/${encodeURIComponent(String(onlandVisualTrashAssessmentID))}/progress`,
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
     * @param onlandVisualTrashAssessmentID 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDWorkflowGet(onlandVisualTrashAssessmentID: number, observe?: 'body', reportProgress?: boolean): Observable<OnlandVisualTrashAssessmentWorkflowDto>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDWorkflowGet(onlandVisualTrashAssessmentID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<OnlandVisualTrashAssessmentWorkflowDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDWorkflowGet(onlandVisualTrashAssessmentID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<OnlandVisualTrashAssessmentWorkflowDto>>;
    public onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDWorkflowGet(onlandVisualTrashAssessmentID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentID === null || onlandVisualTrashAssessmentID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentID was null or undefined when calling onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDWorkflowGet.');
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

        return this.httpClient.get<OnlandVisualTrashAssessmentWorkflowDto>(`${this.basePath}/onland-visual-trash-assessments/${encodeURIComponent(String(onlandVisualTrashAssessmentID))}/workflow`,
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
     * @param onlandVisualTrashAssessmentSimpleDto 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsPost(onlandVisualTrashAssessmentSimpleDto?: OnlandVisualTrashAssessmentSimpleDto, observe?: 'body', reportProgress?: boolean): Observable<OnlandVisualTrashAssessmentSimpleDto>;
    public onlandVisualTrashAssessmentsPost(onlandVisualTrashAssessmentSimpleDto?: OnlandVisualTrashAssessmentSimpleDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<OnlandVisualTrashAssessmentSimpleDto>>;
    public onlandVisualTrashAssessmentsPost(onlandVisualTrashAssessmentSimpleDto?: OnlandVisualTrashAssessmentSimpleDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<OnlandVisualTrashAssessmentSimpleDto>>;
    public onlandVisualTrashAssessmentsPost(onlandVisualTrashAssessmentSimpleDto?: OnlandVisualTrashAssessmentSimpleDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


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
            'application/json',
            'text/json',
            'application/_*+json',
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.post<OnlandVisualTrashAssessmentSimpleDto>(`${this.basePath}/onland-visual-trash-assessments`,
            onlandVisualTrashAssessmentSimpleDto,
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
    public onlandVisualTrashAssessmentsPreliminarySourceIdentificationTypesGet(observe?: 'body', reportProgress?: boolean): Observable<Array<PreliminarySourceIdentificationTypeSimpleDto>>;
    public onlandVisualTrashAssessmentsPreliminarySourceIdentificationTypesGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<PreliminarySourceIdentificationTypeSimpleDto>>>;
    public onlandVisualTrashAssessmentsPreliminarySourceIdentificationTypesGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<PreliminarySourceIdentificationTypeSimpleDto>>>;
    public onlandVisualTrashAssessmentsPreliminarySourceIdentificationTypesGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.get<Array<PreliminarySourceIdentificationTypeSimpleDto>>(`${this.basePath}/onland-visual-trash-assessments/preliminary-source-identification-types`,
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
     * @param onlandVisualTrashAssessmentWorkflowDto 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentsPut(onlandVisualTrashAssessmentWorkflowDto?: OnlandVisualTrashAssessmentWorkflowDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public onlandVisualTrashAssessmentsPut(onlandVisualTrashAssessmentWorkflowDto?: OnlandVisualTrashAssessmentWorkflowDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public onlandVisualTrashAssessmentsPut(onlandVisualTrashAssessmentWorkflowDto?: OnlandVisualTrashAssessmentWorkflowDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public onlandVisualTrashAssessmentsPut(onlandVisualTrashAssessmentWorkflowDto?: OnlandVisualTrashAssessmentWorkflowDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


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

        return this.httpClient.put<any>(`${this.basePath}/onland-visual-trash-assessments`,
            onlandVisualTrashAssessmentWorkflowDto,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

}
