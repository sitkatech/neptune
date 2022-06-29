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
import { OrganizationSimpleDto } from '././organization-simple-dto';
import { ProjectStatusSimpleDto } from '././project-status-simple-dto';
import { PersonSimpleDto } from '././person-simple-dto';
import { StormwaterJurisdictionSimpleDto } from '././stormwater-jurisdiction-simple-dto';

export class ProjectHRUCharacteristicsSummaryDto { 
    Area?: number;
    ImperviousAcres?: number;
    TPI?: number;
    SEA?: number;
    DryWeatherWQLRI?: number;
    WetWeatherWQLRI?: number;
    ProjectID?: number;
    ProjectName?: string;
    OrganizationID?: number;
    StormwaterJurisdictionID?: number;
    ProjectStatusID?: number;
    PrimaryContactPersonID?: number;
    CreatePersonID?: number;
    DateCreated?: string;
    ProjectDescription?: string;
    AdditionalContactInformation?: string;
    DoesNotIncludeTreatmentBMPs?: boolean;
    CalculateOCTAM2Tier2Scores?: boolean;
    ShareOCTAM2Tier2Scores?: boolean;
    OCTAM2Tier2ScoresLastSharedDate?: string;
    Organization?: OrganizationSimpleDto;
    StormwaterJurisdiction?: StormwaterJurisdictionSimpleDto;
    ProjectStatus?: ProjectStatusSimpleDto;
    PrimaryContactPerson?: PersonSimpleDto;
    CreatePerson?: PersonSimpleDto;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}
