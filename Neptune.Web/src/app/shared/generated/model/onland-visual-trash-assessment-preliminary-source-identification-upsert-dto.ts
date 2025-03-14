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
export class OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDto { 
    Selected?: boolean;
    PreliminarySourceIdentificationTypeID?: number;
    ExplanationIfTypeIsOther?: string;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDtoForm { 
    Selected?: FormControl<boolean>;
    PreliminarySourceIdentificationTypeID?: FormControl<number>;
    ExplanationIfTypeIsOther?: FormControl<string>;
}

export class OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDtoFormControls { 
    public static Selected = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PreliminarySourceIdentificationTypeID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ExplanationIfTypeIsOther = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
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
