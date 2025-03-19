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
import { BoundingBoxDto } from '././bounding-box-dto';

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class OnlandVisualTrashAssessmentRefineAreaDto { 
    OnlandVisualTrashAssessmentID?: number;
    OnlandVisualTrashAssessmentAreaID?: number;
    BoundingBox?: BoundingBoxDto;
    GeometryAsGeoJson?: string;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface OnlandVisualTrashAssessmentRefineAreaDtoForm { 
    OnlandVisualTrashAssessmentID?: FormControl<number>;
    OnlandVisualTrashAssessmentAreaID?: FormControl<number>;
    BoundingBox?: FormControl<BoundingBoxDto>;
    GeometryAsGeoJson?: FormControl<string>;
}

export class OnlandVisualTrashAssessmentRefineAreaDtoFormControls { 
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
    public static GeometryAsGeoJson = (value: FormControlState<string> | string = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<string>(
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
