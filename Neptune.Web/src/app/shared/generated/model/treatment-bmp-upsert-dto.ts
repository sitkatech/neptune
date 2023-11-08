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

export class TreatmentBMPUpsertDto { 
    TreatmentBMPID?: number;
    TreatmentBMPName: string;
    TreatmentBMPTypeID: number;
    TreatmentBMPTypeName?: string;
    TreatmentBMPModelingTypeID?: number;
    StormwaterJurisdictionName?: string;
    WatershedName?: string;
    Longitude: number;
    Latitude: number;
    Notes?: string;
    AverageDivertedFlowrate?: number;
    AverageTreatmentFlowrate?: number;
    DesignDryWeatherTreatmentCapacity?: number;
    DesignLowFlowDiversionCapacity?: number;
    DesignMediaFiltrationRate?: number;
    DesignResidenceTimeForPermanentPool?: number;
    DiversionRate?: number;
    DrawdownTimeForWQDetentionVolume?: number;
    EffectiveFootprint?: number;
    EffectiveRetentionDepth?: number;
    InfiltrationDischargeRate?: number;
    InfiltrationSurfaceArea?: number;
    MediaBedFootprint?: number;
    PermanentPoolOrWetlandVolume?: number;
    StorageVolumeBelowLowestOutletElevation?: number;
    SummerHarvestedWaterDemand?: number;
    DrawdownTimeForDetentionVolume?: number;
    TotalEffectiveBMPVolume?: number;
    TotalEffectiveDrywellBMPVolume?: number;
    TreatmentRate?: number;
    UnderlyingInfiltrationRate?: number;
    WaterQualityDetentionVolume?: number;
    WettedFootprint?: number;
    WinterHarvestedWaterDemand?: number;
    RoutingConfigurationID?: number;
    TimeOfConcentrationID?: number;
    UnderlyingHydrologicSoilGroupID?: number;
    MonthsOfOperationID?: number;
    DryWeatherFlowOverrideID?: number;
    AreAllModelingAttributesComplete?: boolean;
    IsFullyParameterized?: boolean;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}
