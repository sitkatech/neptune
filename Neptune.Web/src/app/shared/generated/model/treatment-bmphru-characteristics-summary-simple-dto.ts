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
export class TreatmentBMPHRUCharacteristicsSummarySimpleDto { 
    ProjectHRUCharacteristicID?: number;
    TreatmentBMPID?: number;
    LandUse?: string;
    Area?: number;
    ImperviousCover?: number;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface TreatmentBMPHRUCharacteristicsSummarySimpleDtoForm { 
    ProjectHRUCharacteristicID?: FormControl<number>;
    TreatmentBMPID?: FormControl<number>;
    LandUse?: FormControl<string>;
    Area?: FormControl<number>;
    ImperviousCover?: FormControl<number>;
}

export class TreatmentBMPHRUCharacteristicsSummarySimpleDtoFormControls { 
    public static ProjectHRUCharacteristicID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
    public static LandUse = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Area = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ImperviousCover = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
