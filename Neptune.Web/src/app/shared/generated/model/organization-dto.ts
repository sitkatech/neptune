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
import { PersonDto } from '././person-dto';
import { FileResourceDto } from '././file-resource-dto';
import { OrganizationTypeSimpleDto } from '././organization-type-simple-dto';

export class OrganizationDto { 
    OrganizationID?: number;
    OrganizationGuid?: string;
    OrganizationName?: string;
    OrganizationShortName?: string;
    PrimaryContactPerson?: PersonDto;
    IsActive?: boolean;
    OrganizationUrl?: string;
    LogoFileResource?: FileResourceDto;
    OrganizationType?: OrganizationTypeSimpleDto;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}
