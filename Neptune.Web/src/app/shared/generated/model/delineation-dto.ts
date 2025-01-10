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
export class DelineationDto { 
    DelineationID?: number;
    DelineationTypeID?: number;
    IsVerified?: boolean;
    DateLastVerified?: string;
    VerifiedByPersonID?: number;
    TreatmentBMPID?: number;
    DateLastModified?: string;
    HasDiscrepancies?: boolean;
    Geometry?: string;
    DelineationArea?: number;
    DelineationTypeName?: string;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface DelineationDtoForm { 
    DelineationID?: FormControl<number>;
    DelineationTypeID?: FormControl<number>;
    IsVerified?: FormControl<boolean>;
    DateLastVerified?: FormControl<string>;
    VerifiedByPersonID?: FormControl<number>;
    TreatmentBMPID?: FormControl<number>;
    DateLastModified?: FormControl<string>;
    HasDiscrepancies?: FormControl<boolean>;
    Geometry?: FormControl<string>;
    DelineationArea?: FormControl<number>;
    DelineationTypeName?: FormControl<string>;
}

export class DelineationDtoFormControls { 
    public static DelineationID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DelineationTypeID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static IsVerified = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DateLastVerified = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static VerifiedByPersonID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static TreatmentBMPID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DateLastModified = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static HasDiscrepancies = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Geometry = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DelineationArea = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DelineationTypeName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
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
