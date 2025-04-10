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
import { Ordinates } from '././ordinates';

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class CoordinateSequenceFactory { 
    Ordinates?: Ordinates;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface CoordinateSequenceFactoryForm { 
    Ordinates?: FormControl<Ordinates>;
}

export class CoordinateSequenceFactoryFormControls { 
    public static Ordinates = (value: FormControlState<Ordinates> | Ordinates = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<Ordinates>(
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
