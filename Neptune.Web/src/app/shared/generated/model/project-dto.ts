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
import { OrganizationSimpleDto } from '././organization-simple-dto';
import { ProjectStatusSimpleDto } from '././project-status-simple-dto';
import { PersonSimpleDto } from '././person-simple-dto';
import { StormwaterJurisdictionDto } from '././stormwater-jurisdiction-dto';

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class ProjectDto { 
    ProjectID?: number;
    ProjectName?: string;
    OrganizationID?: number;
    StormwaterJurisdictionID?: number;
    ProjectStatusID?: number;
    PrimaryContactPersonID?: number;
    CreatePersonID?: number;
    DateCreated?: string;
    ProjectDescription?: string;
    AdditionalContactInformation?: string;
    DoesNotIncludeTreatmentBMPs?: boolean;
    CalculateOCTAM2Tier2Scores?: boolean;
    ShareOCTAM2Tier2Scores?: boolean;
    OCTAM2Tier2ScoresLastSharedDate?: string;
    OCTAWatersheds?: string;
    PollutantVolume?: number;
    PollutantMetals?: number;
    PollutantBacteria?: number;
    PollutantNutrients?: number;
    PollutantTSS?: number;
    TPI?: number;
    SEA?: number;
    DryWeatherWQLRI?: number;
    WetWeatherWQLRI?: number;
    AreaTreatedAcres?: number;
    ImperviousAreaTreatedAcres?: number;
    UpdatePersonID?: number;
    DateUpdated?: string;
    Organization?: OrganizationSimpleDto;
    StormwaterJurisdiction?: StormwaterJurisdictionDto;
    ProjectStatus?: ProjectStatusSimpleDto;
    PrimaryContactPerson?: PersonSimpleDto;
    CreatePerson?: PersonSimpleDto;
    HasModeledResults?: boolean;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface ProjectDtoForm { 
    ProjectID?: FormControl<number>;
    ProjectName?: FormControl<string>;
    OrganizationID?: FormControl<number>;
    StormwaterJurisdictionID?: FormControl<number>;
    ProjectStatusID?: FormControl<number>;
    PrimaryContactPersonID?: FormControl<number>;
    CreatePersonID?: FormControl<number>;
    DateCreated?: FormControl<string>;
    ProjectDescription?: FormControl<string>;
    AdditionalContactInformation?: FormControl<string>;
    DoesNotIncludeTreatmentBMPs?: FormControl<boolean>;
    CalculateOCTAM2Tier2Scores?: FormControl<boolean>;
    ShareOCTAM2Tier2Scores?: FormControl<boolean>;
    OCTAM2Tier2ScoresLastSharedDate?: FormControl<string>;
    OCTAWatersheds?: FormControl<string>;
    PollutantVolume?: FormControl<number>;
    PollutantMetals?: FormControl<number>;
    PollutantBacteria?: FormControl<number>;
    PollutantNutrients?: FormControl<number>;
    PollutantTSS?: FormControl<number>;
    TPI?: FormControl<number>;
    SEA?: FormControl<number>;
    DryWeatherWQLRI?: FormControl<number>;
    WetWeatherWQLRI?: FormControl<number>;
    AreaTreatedAcres?: FormControl<number>;
    ImperviousAreaTreatedAcres?: FormControl<number>;
    UpdatePersonID?: FormControl<number>;
    DateUpdated?: FormControl<string>;
    Organization?: FormControl<OrganizationSimpleDto>;
    StormwaterJurisdiction?: FormControl<StormwaterJurisdictionDto>;
    ProjectStatus?: FormControl<ProjectStatusSimpleDto>;
    PrimaryContactPerson?: FormControl<PersonSimpleDto>;
    CreatePerson?: FormControl<PersonSimpleDto>;
    HasModeledResults?: FormControl<boolean>;
}

export class ProjectDtoFormControls { 
    public static ProjectID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ProjectName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
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
    public static StormwaterJurisdictionID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ProjectStatusID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
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
    public static CreatePersonID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DateCreated = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
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
    public static OCTAM2Tier2ScoresLastSharedDate = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OCTAWatersheds = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PollutantVolume = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PollutantMetals = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PollutantBacteria = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PollutantNutrients = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PollutantTSS = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static TPI = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static SEA = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DryWeatherWQLRI = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static WetWeatherWQLRI = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static AreaTreatedAcres = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ImperviousAreaTreatedAcres = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static UpdatePersonID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DateUpdated = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Organization = (value: FormControlState<OrganizationSimpleDto> | OrganizationSimpleDto = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<OrganizationSimpleDto>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static StormwaterJurisdiction = (value: FormControlState<StormwaterJurisdictionDto> | StormwaterJurisdictionDto = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<StormwaterJurisdictionDto>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ProjectStatus = (value: FormControlState<ProjectStatusSimpleDto> | ProjectStatusSimpleDto = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<ProjectStatusSimpleDto>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static PrimaryContactPerson = (value: FormControlState<PersonSimpleDto> | PersonSimpleDto = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<PersonSimpleDto>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static CreatePerson = (value: FormControlState<PersonSimpleDto> | PersonSimpleDto = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<PersonSimpleDto>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static HasModeledResults = (value: FormControlState<boolean> | boolean = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<boolean>(
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
