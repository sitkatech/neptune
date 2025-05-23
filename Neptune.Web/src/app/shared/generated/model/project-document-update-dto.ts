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
export class ProjectDocumentUpdateDto { 
    ProjectID: number;
    DisplayName: string;
    DocumentDescription?: string;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface ProjectDocumentUpdateDtoForm { 
    ProjectID: FormControl<number>;
    DisplayName: FormControl<string>;
    DocumentDescription?: FormControl<string>;
}

export class ProjectDocumentUpdateDtoFormControls { 
    public static ProjectID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: true,
            validators: 
            [
                Validators.required,
            ],
        }
    );
    public static DisplayName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: true,
            validators: 
            [
                Validators.required,
                Validators.minLength(0),
                Validators.maxLength(200),
            ],
        }
    );
    public static DocumentDescription = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
                Validators.minLength(0),
                Validators.maxLength(500),
            ],
        }
    );
}
