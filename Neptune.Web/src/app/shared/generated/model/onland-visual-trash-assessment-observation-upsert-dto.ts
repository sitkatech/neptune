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

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class OnlandVisualTrashAssessmentObservationUpsertDto { 
    OnlandVisualTrashAssessmentObservationID?: number;
    OnlandVisualTrashAssessmentID?: number;
    Note?: string;
    ObservationDatetime?: string;
    FileResourceID?: number;
    FileResourceGUID?: string;
    PhotoStagingID?: number;
    Longitude?: number;
    Latitude?: number;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface OnlandVisualTrashAssessmentObservationUpsertDtoForm { 
    OnlandVisualTrashAssessmentObservationID?: FormControl<number>;
    OnlandVisualTrashAssessmentID?: FormControl<number>;
    Note?: FormControl<string>;
    ObservationDatetime?: FormControl<string>;
    FileResourceID?: FormControl<number>;
    FileResourceGUID?: FormControl<string>;
    PhotoStagingID?: FormControl<number>;
    Longitude?: FormControl<number>;
    Latitude?: FormControl<number>;
}

export class OnlandVisualTrashAssessmentObservationUpsertDtoFormControls { 
    public static OnlandVisualTrashAssessmentObservationID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OnlandVisualTrashAssessmentID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Note = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ObservationDatetime = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static FileResourceID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static FileResourceGUID = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PhotoStagingID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Longitude = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Latitude = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
}
