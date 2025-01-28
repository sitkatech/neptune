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
import { OnlandVisualTrashAssessmentGridDto } from '././onland-visual-trash-assessment-grid-dto';
import { BoundingBoxDto } from '././bounding-box-dto';

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class OnlandVisualTrashAssessmentAreaDetailDto { 
    OnlandVisualTrashAssessmentAreaID?: number;
    OnlandVisualTrashAssessmentAreaName?: string;
    StormwaterJurisdictionID?: number;
    StormwaterJurisdictionName?: string;
    OnlandVisualTrashAssessmentBaselineScoreName?: string;
    AssessmentAreaDescription?: string;
    OnlandVisualTrashAssessmentProgressScoreName?: string;
    NumberOfAssessmentsCompleted?: number;
    LastAssessmentDate?: string;
    OnlandVisualTrashAssessments?: Array<OnlandVisualTrashAssessmentGridDto>;
    BoundingBox?: BoundingBoxDto;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface OnlandVisualTrashAssessmentAreaDetailDtoForm { 
    OnlandVisualTrashAssessmentAreaID?: FormControl<number>;
    OnlandVisualTrashAssessmentAreaName?: FormControl<string>;
    StormwaterJurisdictionID?: FormControl<number>;
    StormwaterJurisdictionName?: FormControl<string>;
    OnlandVisualTrashAssessmentBaselineScoreName?: FormControl<string>;
    AssessmentAreaDescription?: FormControl<string>;
    OnlandVisualTrashAssessmentProgressScoreName?: FormControl<string>;
    NumberOfAssessmentsCompleted?: FormControl<number>;
    LastAssessmentDate?: FormControl<string>;
    OnlandVisualTrashAssessments?: FormControl<Array<OnlandVisualTrashAssessmentGridDto>>;
    BoundingBox?: FormControl<BoundingBoxDto>;
}

export class OnlandVisualTrashAssessmentAreaDetailDtoFormControls { 
    public static OnlandVisualTrashAssessmentAreaID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OnlandVisualTrashAssessmentAreaName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
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
    public static StormwaterJurisdictionName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OnlandVisualTrashAssessmentBaselineScoreName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static AssessmentAreaDescription = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OnlandVisualTrashAssessmentProgressScoreName = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static NumberOfAssessmentsCompleted = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static LastAssessmentDate = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static OnlandVisualTrashAssessments = (value: FormControlState<Array<OnlandVisualTrashAssessmentGridDto>> | Array<OnlandVisualTrashAssessmentGridDto> = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<Array<OnlandVisualTrashAssessmentGridDto>>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static BoundingBox = (value: FormControlState<BoundingBoxDto> | BoundingBoxDto = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<BoundingBoxDto>(
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
