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
export class ProjectUpsertDto { 
    ProjectName: string;
    OrganizationID: number;
    StormwaterJurisdictionID: number;
    PrimaryContactPersonID: number;
    ProjectDescription?: string;
    AdditionalContactInformation?: string;
    DoesNotIncludeTreatmentBMPs?: boolean;
    CalculateOCTAM2Tier2Scores?: boolean;
    ShareOCTAM2Tier2Scores?: boolean;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface ProjectUpsertDtoForm { 
    ProjectName: FormControl<string>;
    OrganizationID: FormControl<number>;
    StormwaterJurisdictionID: FormControl<number>;
    PrimaryContactPersonID: FormControl<number>;
    ProjectDescription?: FormControl<string>;
    AdditionalContactInformation?: FormControl<string>;
    DoesNotIncludeTreatmentBMPs?: FormControl<boolean>;
    CalculateOCTAM2Tier2Scores?: FormControl<boolean>;
    ShareOCTAM2Tier2Scores?: FormControl<boolean>;
}

export class ProjectUpsertDtoFormControls { 
    public static ProjectName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
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
    public static OrganizationID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
    public static StormwaterJurisdictionID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
    public static PrimaryContactPersonID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
    public static ProjectDescription = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
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
    public static AdditionalContactInformation = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
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
    public static DoesNotIncludeTreatmentBMPs = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static CalculateOCTAM2Tier2Scores = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ShareOCTAM2Tier2Scores = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
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
