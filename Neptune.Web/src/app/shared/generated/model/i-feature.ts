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
import { Geometry } from '././geometry';
import { Envelope } from '././envelope';
import { IAttributesTable } from '././i-attributes-table';

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class IFeature { 
    Geometry?: Geometry;
    BoundingBox?: Envelope;
    Attributes?: IAttributesTable;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface IFeatureForm { 
    Geometry?: FormControl<Geometry>;
    BoundingBox?: FormControl<Envelope>;
    Attributes?: FormControl<IAttributesTable>;
}

export class IFeatureFormControls { 
    public static Geometry = (value: FormControlState<Geometry> | Geometry = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<Geometry>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static BoundingBox = (value: FormControlState<Envelope> | Envelope = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<Envelope>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Attributes = (value: FormControlState<IAttributesTable> | IAttributesTable = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<IAttributesTable>(
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
