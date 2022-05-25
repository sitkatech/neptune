/**
 * Hippocamp.API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

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
