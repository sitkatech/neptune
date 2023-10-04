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

export class PersonSimpleDto { 
    PersonID?: number;
    PersonGuid?: string;
    FirstName?: string;
    LastName?: string;
    Email?: string;
    Phone?: string;
    RoleID?: number;
    CreateDate?: string;
    UpdateDate?: string;
    LastActivityDate?: string;
    IsActive?: boolean;
    OrganizationID?: number;
    ReceiveSupportEmails?: boolean;
    LoginName?: string;
    ReceiveRSBRevisionRequestEmails?: boolean;
    WebServiceAccessToken?: string;
    IsOCTAGrantReviewer?: boolean;
    readonly FullName?: string;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}