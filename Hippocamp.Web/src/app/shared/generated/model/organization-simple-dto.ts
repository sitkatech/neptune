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
