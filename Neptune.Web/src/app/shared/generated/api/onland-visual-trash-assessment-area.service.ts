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

import { IFeature } from '../model/i-feature';
import { OnlandVisualTrashAssessmentAreaDetailDto } from '../model/onland-visual-trash-assessment-area-detail-dto';
import { OnlandVisualTrashAssessmentAreaGeometryDto } from '../model/onland-visual-trash-assessment-area-geometry-dto';
import { OnlandVisualTrashAssessmentAreaGridDto } from '../model/onland-visual-trash-assessment-area-grid-dto';
import { OnlandVisualTrashAssessmentAreaSimpleDto } from '../model/onland-visual-trash-assessment-area-simple-dto';
import { OnlandVisualTrashAssessmentGridDto } from '../model/onland-visual-trash-assessment-grid-dto';
import { ParcelGeometrySimpleDto } from '../model/parcel-geometry-simple-dto';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';
import { catchError } from 'rxjs/operators';
import { ApiService } from '../../services';


@Injectable({
  providedIn: 'root'
})
export class OnlandVisualTrashAssessmentAreaService {

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
    public onlandVisualTrashAssessmentAreasGet(observe?: 'body', reportProgress?: boolean): Observable<Array<OnlandVisualTrashAssessmentAreaGridDto>>;
    public onlandVisualTrashAssessmentAreasGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<OnlandVisualTrashAssessmentAreaGridDto>>>;
    public onlandVisualTrashAssessmentAreasGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<OnlandVisualTrashAssessmentAreaGridDto>>>;
    public onlandVisualTrashAssessmentAreasGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.get<Array<OnlandVisualTrashAssessmentAreaGridDto>>(`${this.basePath}/onland-visual-trash-assessment-areas`,
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
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDAreaAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'body', reportProgress?: boolean): Observable<Array<IFeature>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDAreaAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<IFeature>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDAreaAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<IFeature>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDAreaAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDAreaAsFeatureCollectionGet.');
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

        return this.httpClient.get<Array<IFeature>>(`${this.basePath}/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}/area-as-feature-collection`,
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
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'body', reportProgress?: boolean): Observable<OnlandVisualTrashAssessmentAreaDetailDto>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<OnlandVisualTrashAssessmentAreaDetailDto>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<OnlandVisualTrashAssessmentAreaDetailDto>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(onlandVisualTrashAssessmentAreaID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet.');
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

        return this.httpClient.get<OnlandVisualTrashAssessmentAreaDetailDto>(`${this.basePath}/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}`,
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
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDOnlandVisualTrashAssessmentsGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'body', reportProgress?: boolean): Observable<Array<OnlandVisualTrashAssessmentGridDto>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDOnlandVisualTrashAssessmentsGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<OnlandVisualTrashAssessmentGridDto>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDOnlandVisualTrashAssessmentsGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<OnlandVisualTrashAssessmentGridDto>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDOnlandVisualTrashAssessmentsGet(onlandVisualTrashAssessmentAreaID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDOnlandVisualTrashAssessmentsGet.');
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

        return this.httpClient.get<Array<OnlandVisualTrashAssessmentGridDto>>(`${this.basePath}/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}/onland-visual-trash-assessments`,
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
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'body', reportProgress?: boolean): Observable<Array<ParcelGeometrySimpleDto>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<ParcelGeometrySimpleDto>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<ParcelGeometrySimpleDto>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesGet(onlandVisualTrashAssessmentAreaID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesGet.');
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

        return this.httpClient.get<Array<ParcelGeometrySimpleDto>>(`${this.basePath}/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}/parcel-geometries`,
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
     * @param onlandVisualTrashAssessmentAreaGeometryDto 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesPost(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaGeometryDto?: OnlandVisualTrashAssessmentAreaGeometryDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesPost(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaGeometryDto?: OnlandVisualTrashAssessmentAreaGeometryDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesPost(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaGeometryDto?: OnlandVisualTrashAssessmentAreaGeometryDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesPost(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaGeometryDto?: OnlandVisualTrashAssessmentAreaGeometryDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesPost.');
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

        return this.httpClient.post<any>(`${this.basePath}/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}/parcel-geometries`,
            onlandVisualTrashAssessmentAreaGeometryDto,
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
     * @param onlandVisualTrashAssessmentAreaSimpleDto 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDPut(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaSimpleDto?: OnlandVisualTrashAssessmentAreaSimpleDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDPut(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaSimpleDto?: OnlandVisualTrashAssessmentAreaSimpleDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDPut(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaSimpleDto?: OnlandVisualTrashAssessmentAreaSimpleDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDPut(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaSimpleDto?: OnlandVisualTrashAssessmentAreaSimpleDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDPut.');
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

        return this.httpClient.put<any>(`${this.basePath}/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}`,
            onlandVisualTrashAssessmentAreaSimpleDto,
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
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDTransectLineAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'body', reportProgress?: boolean): Observable<Array<IFeature>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDTransectLineAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<IFeature>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDTransectLineAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<IFeature>>>;
    public onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDTransectLineAsFeatureCollectionGet(onlandVisualTrashAssessmentAreaID: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (onlandVisualTrashAssessmentAreaID === null || onlandVisualTrashAssessmentAreaID === undefined) {
            throw new Error('Required parameter onlandVisualTrashAssessmentAreaID was null or undefined when calling onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDTransectLineAsFeatureCollectionGet.');
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

        return this.httpClient.get<Array<IFeature>>(`${this.basePath}/onland-visual-trash-assessment-areas/${encodeURIComponent(String(onlandVisualTrashAssessmentAreaID))}/transect-line-as-feature-collection`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(catchError((error: any) => { return this.apiService.handleError(error)}));
    }

}
