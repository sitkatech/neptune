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
export class AreaBasedAcreCalculationsDto { 
    FullTrashCaptureAcreage?: number;
    EquivalentAreaAcreage?: number;
    TotalAcresCaptured?: number;
    TotalPLUAcres?: number;
    PercentTreated?: number;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface AreaBasedAcreCalculationsDtoForm { 
    FullTrashCaptureAcreage?: FormControl<number>;
    EquivalentAreaAcreage?: FormControl<number>;
    TotalAcresCaptured?: FormControl<number>;
    TotalPLUAcres?: FormControl<number>;
    PercentTreated?: FormControl<number>;
}

export class AreaBasedAcreCalculationsDtoFormControls { 
    public static FullTrashCaptureAcreage = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static EquivalentAreaAcreage = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static TotalAcresCaptured = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static TotalPLUAcres = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PercentTreated = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
