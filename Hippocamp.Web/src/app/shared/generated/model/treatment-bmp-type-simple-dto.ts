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

export class TreatmentBMPTypeSimpleDto { 
    TreatmentBMPTypeID?: number;
    TreatmentBMPTypeName?: string;
    TreatmentBMPTypeDescription?: string;
    IsAnalyzedInModelingModule?: boolean;
    TreatmentBMPModelingTypeID?: number;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}
