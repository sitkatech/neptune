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
import { CoordinateSequenceFactory } from '././coordinate-sequence-factory';
import { PrecisionModel } from '././precision-model';

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class NtsGeometryServices { 
    GeometryOverlay?: object;
    CoordinateEqualityComparer?: object;
    readonly DefaultSRID?: number;
    DefaultCoordinateSequenceFactory?: CoordinateSequenceFactory;
    DefaultPrecisionModel?: PrecisionModel;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface NtsGeometryServicesForm { 
    GeometryOverlay?: FormControl<object>;
    CoordinateEqualityComparer?: FormControl<object>;
    DefaultSRID?: FormControl<number>;
    DefaultCoordinateSequenceFactory?: FormControl<CoordinateSequenceFactory>;
    DefaultPrecisionModel?: FormControl<PrecisionModel>;
}

export class NtsGeometryServicesFormControls { 
    public static GeometryOverlay = (value: FormControlState<object> | object = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<object>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static CoordinateEqualityComparer = (value: FormControlState<object> | object = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<object>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DefaultSRID = (value: FormControlState<number> | number = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<number>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DefaultCoordinateSequenceFactory = (value: FormControlState<CoordinateSequenceFactory> | CoordinateSequenceFactory = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<CoordinateSequenceFactory>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static DefaultPrecisionModel = (value: FormControlState<PrecisionModel> | PrecisionModel = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<PrecisionModel>(
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
