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

export class PersonCreateDto { 
    LoginName?: string;
    UserGuid?: string;
    FirstName?: string;
    LastName?: string;
    OrganizationName?: string;
    Email?: string;
    PhoneNumber?: string;
    RoleID: number;
    ReceiveSupportEmails: boolean;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}
