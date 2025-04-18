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
export class OrganizationSimpleDto { 
    OrganizationID?: number;
    OrganizationGuid?: string;
    OrganizationName?: string;
    OrganizationShortName?: string;
    PrimaryContactPersonID?: number;
    IsActive?: boolean;
    OrganizationUrl?: string;
    LogoFileResourceID?: number;
    OrganizationTypeID?: number;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface OrganizationSimpleDtoForm { 
    OrganizationID?: FormControl<number>;
    OrganizationGuid?: FormControl<string>;
    OrganizationName?: FormControl<string>;
    OrganizationShortName?: FormControl<string>;
    PrimaryContactPersonID?: FormControl<number>;
    IsActive?: FormControl<boolean>;
    OrganizationUrl?: FormControl<string>;
    LogoFileResourceID?: FormControl<number>;
    OrganizationTypeID?: FormControl<number>;
}

export class OrganizationSimpleDtoFormControls { 
    public static OrganizationID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OrganizationGuid = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OrganizationName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OrganizationShortName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PrimaryContactPersonID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static IsActive = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OrganizationUrl = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static LogoFileResourceID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OrganizationTypeID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
