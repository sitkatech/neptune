//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinition]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class FieldDefinition : IHavePrimaryKey
    {
        public static readonly FieldDefinitionIsPrimaryContactOrganization IsPrimaryContactOrganization = FieldDefinitionIsPrimaryContactOrganization.Instance;
        public static readonly FieldDefinitionOrganization Organization = FieldDefinitionOrganization.Instance;
        public static readonly FieldDefinitionPassword Password = FieldDefinitionPassword.Instance;
        public static readonly FieldDefinitionMeasurementUnit MeasurementUnit = FieldDefinitionMeasurementUnit.Instance;
        public static readonly FieldDefinitionPhotoCaption PhotoCaption = FieldDefinitionPhotoCaption.Instance;
        public static readonly FieldDefinitionPhotoCredit PhotoCredit = FieldDefinitionPhotoCredit.Instance;
        public static readonly FieldDefinitionPhotoTiming PhotoTiming = FieldDefinitionPhotoTiming.Instance;
        public static readonly FieldDefinitionPrimaryContact PrimaryContact = FieldDefinitionPrimaryContact.Instance;
        public static readonly FieldDefinitionOrganizationType OrganizationType = FieldDefinitionOrganizationType.Instance;
        public static readonly FieldDefinitionUsername Username = FieldDefinitionUsername.Instance;
        public static readonly FieldDefinitionExternalLinks ExternalLinks = FieldDefinitionExternalLinks.Instance;
        public static readonly FieldDefinitionRoleName RoleName = FieldDefinitionRoleName.Instance;
        public static readonly FieldDefinitionChartLastUpdatedDate ChartLastUpdatedDate = FieldDefinitionChartLastUpdatedDate.Instance;
        public static readonly FieldDefinitionTreatmentBMPType TreatmentBMPType = FieldDefinitionTreatmentBMPType.Instance;
        public static readonly FieldDefinitionConveyanceFunctionsAsIntended ConveyanceFunctionsAsIntended = FieldDefinitionConveyanceFunctionsAsIntended.Instance;
        public static readonly FieldDefinitionAssessmentScoreWeight AssessmentScoreWeight = FieldDefinitionAssessmentScoreWeight.Instance;
        public static readonly FieldDefinitionObservationScore ObservationScore = FieldDefinitionObservationScore.Instance;
        public static readonly FieldDefinitionAlternativeScore AlternativeScore = FieldDefinitionAlternativeScore.Instance;
        public static readonly FieldDefinitionAssessmentForInternalUseOnly AssessmentForInternalUseOnly = FieldDefinitionAssessmentForInternalUseOnly.Instance;
        public static readonly FieldDefinitionTreatmentBMPDesignDepth TreatmentBMPDesignDepth = FieldDefinitionTreatmentBMPDesignDepth.Instance;
        public static readonly FieldDefinitionReceivesSystemCommunications ReceivesSystemCommunications = FieldDefinitionReceivesSystemCommunications.Instance;
        public static readonly FieldDefinitionJurisdiction Jurisdiction = FieldDefinitionJurisdiction.Instance;
        public static readonly FieldDefinitionDelineation Delineation = FieldDefinitionDelineation.Instance;
        public static readonly FieldDefinitionTreatmentBMP TreatmentBMP = FieldDefinitionTreatmentBMP.Instance;
        public static readonly FieldDefinitionTreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType = FieldDefinitionTreatmentBMPAssessmentObservationType.Instance;
        public static readonly FieldDefinitionObservationCollectionMethod ObservationCollectionMethod = FieldDefinitionObservationCollectionMethod.Instance;
        public static readonly FieldDefinitionObservationThresholdType ObservationThresholdType = FieldDefinitionObservationThresholdType.Instance;
        public static readonly FieldDefinitionObservationTargetType ObservationTargetType = FieldDefinitionObservationTargetType.Instance;
        public static readonly FieldDefinitionMeasurementUnitLabel MeasurementUnitLabel = FieldDefinitionMeasurementUnitLabel.Instance;
        public static readonly FieldDefinitionPropertiesToObserve PropertiesToObserve = FieldDefinitionPropertiesToObserve.Instance;
        public static readonly FieldDefinitionMinimumNumberOfObservations MinimumNumberOfObservations = FieldDefinitionMinimumNumberOfObservations.Instance;
        public static readonly FieldDefinitionMaximumNumberOfObservations MaximumNumberOfObservations = FieldDefinitionMaximumNumberOfObservations.Instance;
        public static readonly FieldDefinitionMinimumValueOfEachObservation MinimumValueOfEachObservation = FieldDefinitionMinimumValueOfEachObservation.Instance;
        public static readonly FieldDefinitionMaximumValueOfEachObservation MaximumValueOfEachObservation = FieldDefinitionMaximumValueOfEachObservation.Instance;
        public static readonly FieldDefinitionDefaultThresholdValue DefaultThresholdValue = FieldDefinitionDefaultThresholdValue.Instance;
        public static readonly FieldDefinitionDefaultBenchmarkValue DefaultBenchmarkValue = FieldDefinitionDefaultBenchmarkValue.Instance;
        public static readonly FieldDefinitionAssessmentFailsIfObservationFails AssessmentFailsIfObservationFails = FieldDefinitionAssessmentFailsIfObservationFails.Instance;
        public static readonly FieldDefinitionCustomAttributeType CustomAttributeType = FieldDefinitionCustomAttributeType.Instance;
        public static readonly FieldDefinitionCustomAttributeDataType CustomAttributeDataType = FieldDefinitionCustomAttributeDataType.Instance;
        public static readonly FieldDefinitionMaintenanceRecordType MaintenanceRecordType = FieldDefinitionMaintenanceRecordType.Instance;
        public static readonly FieldDefinitionMaintenanceRecord MaintenanceRecord = FieldDefinitionMaintenanceRecord.Instance;
        public static readonly FieldDefinitionAttributeTypePurpose AttributeTypePurpose = FieldDefinitionAttributeTypePurpose.Instance;
        public static readonly FieldDefinitionFundingSource FundingSource = FieldDefinitionFundingSource.Instance;
        public static readonly FieldDefinitionIsPostMaintenanceAssessment IsPostMaintenanceAssessment = FieldDefinitionIsPostMaintenanceAssessment.Instance;
        public static readonly FieldDefinitionFundingEvent FundingEvent = FieldDefinitionFundingEvent.Instance;
        public static readonly FieldDefinitionFieldVisit FieldVisit = FieldDefinitionFieldVisit.Instance;
        public static readonly FieldDefinitionFieldVisitStatus FieldVisitStatus = FieldDefinitionFieldVisitStatus.Instance;
        public static readonly FieldDefinitionWaterQualityManagementPlan WaterQualityManagementPlan = FieldDefinitionWaterQualityManagementPlan.Instance;
        public static readonly FieldDefinitionParcel Parcel = FieldDefinitionParcel.Instance;
        public static readonly FieldDefinitionRequiredLifespanOfInstallation RequiredLifespanOfInstallation = FieldDefinitionRequiredLifespanOfInstallation.Instance;
        public static readonly FieldDefinitionRequiredFieldVisitsPerYear RequiredFieldVisitsPerYear = FieldDefinitionRequiredFieldVisitsPerYear.Instance;
        public static readonly FieldDefinitionRequiredPostStormFieldVisitsPerYear RequiredPostStormFieldVisitsPerYear = FieldDefinitionRequiredPostStormFieldVisitsPerYear.Instance;
        public static readonly FieldDefinitionWaterQualityManagementPlanDocumentType WaterQualityManagementPlanDocumentType = FieldDefinitionWaterQualityManagementPlanDocumentType.Instance;
        public static readonly FieldDefinitionHasAllRequiredDocuments HasAllRequiredDocuments = FieldDefinitionHasAllRequiredDocuments.Instance;
        public static readonly FieldDefinitionDateOfLastInventoryChange DateOfLastInventoryChange = FieldDefinitionDateOfLastInventoryChange.Instance;
        public static readonly FieldDefinitionTrashCaptureStatus TrashCaptureStatus = FieldDefinitionTrashCaptureStatus.Instance;
        public static readonly FieldDefinitionOnlandVisualTrashAssessment OnlandVisualTrashAssessment = FieldDefinitionOnlandVisualTrashAssessment.Instance;
        public static readonly FieldDefinitionOnlandVisualTrashAssessmentNotes OnlandVisualTrashAssessmentNotes = FieldDefinitionOnlandVisualTrashAssessmentNotes.Instance;
        public static readonly FieldDefinitionDelineationType DelineationType = FieldDefinitionDelineationType.Instance;
        public static readonly FieldDefinitionBaselineScore BaselineScore = FieldDefinitionBaselineScore.Instance;
        public static readonly FieldDefinitionSizingBasis SizingBasis = FieldDefinitionSizingBasis.Instance;
        public static readonly FieldDefinitionProgressScore ProgressScore = FieldDefinitionProgressScore.Instance;
        public static readonly FieldDefinitionAssessmentScore AssessmentScore = FieldDefinitionAssessmentScore.Instance;
        public static readonly FieldDefinitionViaFullCapture ViaFullCapture = FieldDefinitionViaFullCapture.Instance;
        public static readonly FieldDefinitionViaPartialCapture ViaPartialCapture = FieldDefinitionViaPartialCapture.Instance;
        public static readonly FieldDefinitionViaOVTAScore ViaOVTAScore = FieldDefinitionViaOVTAScore.Instance;
        public static readonly FieldDefinitionTotalAchieved TotalAchieved = FieldDefinitionTotalAchieved.Instance;
        public static readonly FieldDefinitionTargetLoadReduction TargetLoadReduction = FieldDefinitionTargetLoadReduction.Instance;
        public static readonly FieldDefinitionLoadingRate LoadingRate = FieldDefinitionLoadingRate.Instance;
        public static readonly FieldDefinitionLandUse LandUse = FieldDefinitionLandUse.Instance;
        public static readonly FieldDefinitionArea Area = FieldDefinitionArea.Instance;
        public static readonly FieldDefinitionImperviousArea ImperviousArea = FieldDefinitionImperviousArea.Instance;
        public static readonly FieldDefinitionGrossArea GrossArea = FieldDefinitionGrossArea.Instance;
        public static readonly FieldDefinitionLandUseStatistics LandUseStatistics = FieldDefinitionLandUseStatistics.Instance;
        public static readonly FieldDefinitionNetworkCatchment NetworkCatchment = FieldDefinitionNetworkCatchment.Instance;
        public static readonly FieldDefinitionAverageDivertedFlowrate AverageDivertedFlowrate = FieldDefinitionAverageDivertedFlowrate.Instance;
        public static readonly FieldDefinitionAverageTreatmentFlowrate AverageTreatmentFlowrate = FieldDefinitionAverageTreatmentFlowrate.Instance;
        public static readonly FieldDefinitionDesignDryWeatherTreatmentCapacity DesignDryWeatherTreatmentCapacity = FieldDefinitionDesignDryWeatherTreatmentCapacity.Instance;
        public static readonly FieldDefinitionDesignLowFlowDiversionCapacity DesignLowFlowDiversionCapacity = FieldDefinitionDesignLowFlowDiversionCapacity.Instance;
        public static readonly FieldDefinitionDesignMediaFiltrationRate DesignMediaFiltrationRate = FieldDefinitionDesignMediaFiltrationRate.Instance;
        public static readonly FieldDefinitionDesignResidenceTimeforPermanentPool DesignResidenceTimeforPermanentPool = FieldDefinitionDesignResidenceTimeforPermanentPool.Instance;
        public static readonly FieldDefinitionDiversionRate DiversionRate = FieldDefinitionDiversionRate.Instance;
        public static readonly FieldDefinitionDrawdownTimeforWQDetentionVolume DrawdownTimeforWQDetentionVolume = FieldDefinitionDrawdownTimeforWQDetentionVolume.Instance;
        public static readonly FieldDefinitionEffectiveFootprint EffectiveFootprint = FieldDefinitionEffectiveFootprint.Instance;
        public static readonly FieldDefinitionEffectiveRetentionDepth EffectiveRetentionDepth = FieldDefinitionEffectiveRetentionDepth.Instance;
        public static readonly FieldDefinitionInfiltrationDischargeRate InfiltrationDischargeRate = FieldDefinitionInfiltrationDischargeRate.Instance;
        public static readonly FieldDefinitionInfiltrationSurfaceArea InfiltrationSurfaceArea = FieldDefinitionInfiltrationSurfaceArea.Instance;
        public static readonly FieldDefinitionMediaBedFootprint MediaBedFootprint = FieldDefinitionMediaBedFootprint.Instance;
        public static readonly FieldDefinitionMonthsofOperation MonthsofOperation = FieldDefinitionMonthsofOperation.Instance;
        public static readonly FieldDefinitionPermanentPoolorWetlandVolume PermanentPoolorWetlandVolume = FieldDefinitionPermanentPoolorWetlandVolume.Instance;
        public static readonly FieldDefinitionRoutingConfiguration RoutingConfiguration = FieldDefinitionRoutingConfiguration.Instance;
        public static readonly FieldDefinitionStorageVolumeBelowLowestOutletElevation StorageVolumeBelowLowestOutletElevation = FieldDefinitionStorageVolumeBelowLowestOutletElevation.Instance;
        public static readonly FieldDefinitionSummerHarvestedWaterDemand SummerHarvestedWaterDemand = FieldDefinitionSummerHarvestedWaterDemand.Instance;
        public static readonly FieldDefinitionTimeofConcentration TimeofConcentration = FieldDefinitionTimeofConcentration.Instance;
        public static readonly FieldDefinitionTotalDrawdownTime TotalDrawdownTime = FieldDefinitionTotalDrawdownTime.Instance;
        public static readonly FieldDefinitionTotalEffectiveBMPVolume TotalEffectiveBMPVolume = FieldDefinitionTotalEffectiveBMPVolume.Instance;
        public static readonly FieldDefinitionTotalEffectiveDrywellBMPVolume TotalEffectiveDrywellBMPVolume = FieldDefinitionTotalEffectiveDrywellBMPVolume.Instance;
        public static readonly FieldDefinitionTreatmentRate TreatmentRate = FieldDefinitionTreatmentRate.Instance;
        public static readonly FieldDefinitionUnderlyingHydrologicSoilGroupHSG UnderlyingHydrologicSoilGroupHSG = FieldDefinitionUnderlyingHydrologicSoilGroupHSG.Instance;
        public static readonly FieldDefinitionUnderlyingInfiltrationRate UnderlyingInfiltrationRate = FieldDefinitionUnderlyingInfiltrationRate.Instance;
        public static readonly FieldDefinitionUpstreamBMP UpstreamBMP = FieldDefinitionUpstreamBMP.Instance;
        public static readonly FieldDefinitionWaterQualityDetentionVolume WaterQualityDetentionVolume = FieldDefinitionWaterQualityDetentionVolume.Instance;
        public static readonly FieldDefinitionWettedFootprint WettedFootprint = FieldDefinitionWettedFootprint.Instance;
        public static readonly FieldDefinitionWinterHarvestedWaterDemand WinterHarvestedWaterDemand = FieldDefinitionWinterHarvestedWaterDemand.Instance;

        public static readonly List<FieldDefinition> All;
        public static readonly ReadOnlyDictionary<int, FieldDefinition> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static FieldDefinition()
        {
            All = new List<FieldDefinition> { IsPrimaryContactOrganization, Organization, Password, MeasurementUnit, PhotoCaption, PhotoCredit, PhotoTiming, PrimaryContact, OrganizationType, Username, ExternalLinks, RoleName, ChartLastUpdatedDate, TreatmentBMPType, ConveyanceFunctionsAsIntended, AssessmentScoreWeight, ObservationScore, AlternativeScore, AssessmentForInternalUseOnly, TreatmentBMPDesignDepth, ReceivesSystemCommunications, Jurisdiction, Delineation, TreatmentBMP, TreatmentBMPAssessmentObservationType, ObservationCollectionMethod, ObservationThresholdType, ObservationTargetType, MeasurementUnitLabel, PropertiesToObserve, MinimumNumberOfObservations, MaximumNumberOfObservations, MinimumValueOfEachObservation, MaximumValueOfEachObservation, DefaultThresholdValue, DefaultBenchmarkValue, AssessmentFailsIfObservationFails, CustomAttributeType, CustomAttributeDataType, MaintenanceRecordType, MaintenanceRecord, AttributeTypePurpose, FundingSource, IsPostMaintenanceAssessment, FundingEvent, FieldVisit, FieldVisitStatus, WaterQualityManagementPlan, Parcel, RequiredLifespanOfInstallation, RequiredFieldVisitsPerYear, RequiredPostStormFieldVisitsPerYear, WaterQualityManagementPlanDocumentType, HasAllRequiredDocuments, DateOfLastInventoryChange, TrashCaptureStatus, OnlandVisualTrashAssessment, OnlandVisualTrashAssessmentNotes, DelineationType, BaselineScore, SizingBasis, ProgressScore, AssessmentScore, ViaFullCapture, ViaPartialCapture, ViaOVTAScore, TotalAchieved, TargetLoadReduction, LoadingRate, LandUse, Area, ImperviousArea, GrossArea, LandUseStatistics, NetworkCatchment, AverageDivertedFlowrate, AverageTreatmentFlowrate, DesignDryWeatherTreatmentCapacity, DesignLowFlowDiversionCapacity, DesignMediaFiltrationRate, DesignResidenceTimeforPermanentPool, DiversionRate, DrawdownTimeforWQDetentionVolume, EffectiveFootprint, EffectiveRetentionDepth, InfiltrationDischargeRate, InfiltrationSurfaceArea, MediaBedFootprint, MonthsofOperation, PermanentPoolorWetlandVolume, RoutingConfiguration, StorageVolumeBelowLowestOutletElevation, SummerHarvestedWaterDemand, TimeofConcentration, TotalDrawdownTime, TotalEffectiveBMPVolume, TotalEffectiveDrywellBMPVolume, TreatmentRate, UnderlyingHydrologicSoilGroupHSG, UnderlyingInfiltrationRate, UpstreamBMP, WaterQualityDetentionVolume, WettedFootprint, WinterHarvestedWaterDemand };
            AllLookupDictionary = new ReadOnlyDictionary<int, FieldDefinition>(All.ToDictionary(x => x.FieldDefinitionID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected FieldDefinition(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel)
        {
            FieldDefinitionID = fieldDefinitionID;
            FieldDefinitionName = fieldDefinitionName;
            FieldDefinitionDisplayName = fieldDefinitionDisplayName;
            DefaultDefinition = defaultDefinition;
            CanCustomizeLabel = canCustomizeLabel;
        }

        [Key]
        public int FieldDefinitionID { get; private set; }
        public string FieldDefinitionName { get; private set; }
        public string FieldDefinitionDisplayName { get; private set; }
        public string DefaultDefinition { get; set; }
        [NotMapped]
        public HtmlString DefaultDefinitionHtmlString
        { 
            get { return DefaultDefinition == null ? null : new HtmlString(DefaultDefinition); }
            set { DefaultDefinition = value?.ToString(); }
        }
        public bool CanCustomizeLabel { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return FieldDefinitionID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(FieldDefinition other)
        {
            if (other == null)
            {
                return false;
            }
            return other.FieldDefinitionID == FieldDefinitionID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as FieldDefinition);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return FieldDefinitionID;
        }

        public static bool operator ==(FieldDefinition left, FieldDefinition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FieldDefinition left, FieldDefinition right)
        {
            return !Equals(left, right);
        }

        public FieldDefinitionEnum ToEnum { get { return (FieldDefinitionEnum)GetHashCode(); } }

        public static FieldDefinition ToType(int enumValue)
        {
            return ToType((FieldDefinitionEnum)enumValue);
        }

        public static FieldDefinition ToType(FieldDefinitionEnum enumValue)
        {
            switch (enumValue)
            {
                case FieldDefinitionEnum.AlternativeScore:
                    return AlternativeScore;
                case FieldDefinitionEnum.Area:
                    return Area;
                case FieldDefinitionEnum.AssessmentFailsIfObservationFails:
                    return AssessmentFailsIfObservationFails;
                case FieldDefinitionEnum.AssessmentForInternalUseOnly:
                    return AssessmentForInternalUseOnly;
                case FieldDefinitionEnum.AssessmentScore:
                    return AssessmentScore;
                case FieldDefinitionEnum.AssessmentScoreWeight:
                    return AssessmentScoreWeight;
                case FieldDefinitionEnum.AttributeTypePurpose:
                    return AttributeTypePurpose;
                case FieldDefinitionEnum.AverageDivertedFlowrate:
                    return AverageDivertedFlowrate;
                case FieldDefinitionEnum.AverageTreatmentFlowrate:
                    return AverageTreatmentFlowrate;
                case FieldDefinitionEnum.BaselineScore:
                    return BaselineScore;
                case FieldDefinitionEnum.ChartLastUpdatedDate:
                    return ChartLastUpdatedDate;
                case FieldDefinitionEnum.ConveyanceFunctionsAsIntended:
                    return ConveyanceFunctionsAsIntended;
                case FieldDefinitionEnum.CustomAttributeDataType:
                    return CustomAttributeDataType;
                case FieldDefinitionEnum.CustomAttributeType:
                    return CustomAttributeType;
                case FieldDefinitionEnum.DateOfLastInventoryChange:
                    return DateOfLastInventoryChange;
                case FieldDefinitionEnum.DefaultBenchmarkValue:
                    return DefaultBenchmarkValue;
                case FieldDefinitionEnum.DefaultThresholdValue:
                    return DefaultThresholdValue;
                case FieldDefinitionEnum.Delineation:
                    return Delineation;
                case FieldDefinitionEnum.DelineationType:
                    return DelineationType;
                case FieldDefinitionEnum.DesignDryWeatherTreatmentCapacity:
                    return DesignDryWeatherTreatmentCapacity;
                case FieldDefinitionEnum.DesignLowFlowDiversionCapacity:
                    return DesignLowFlowDiversionCapacity;
                case FieldDefinitionEnum.DesignMediaFiltrationRate:
                    return DesignMediaFiltrationRate;
                case FieldDefinitionEnum.DesignResidenceTimeforPermanentPool:
                    return DesignResidenceTimeforPermanentPool;
                case FieldDefinitionEnum.DiversionRate:
                    return DiversionRate;
                case FieldDefinitionEnum.DrawdownTimeforWQDetentionVolume:
                    return DrawdownTimeforWQDetentionVolume;
                case FieldDefinitionEnum.EffectiveFootprint:
                    return EffectiveFootprint;
                case FieldDefinitionEnum.EffectiveRetentionDepth:
                    return EffectiveRetentionDepth;
                case FieldDefinitionEnum.ExternalLinks:
                    return ExternalLinks;
                case FieldDefinitionEnum.FieldVisit:
                    return FieldVisit;
                case FieldDefinitionEnum.FieldVisitStatus:
                    return FieldVisitStatus;
                case FieldDefinitionEnum.FundingEvent:
                    return FundingEvent;
                case FieldDefinitionEnum.FundingSource:
                    return FundingSource;
                case FieldDefinitionEnum.GrossArea:
                    return GrossArea;
                case FieldDefinitionEnum.HasAllRequiredDocuments:
                    return HasAllRequiredDocuments;
                case FieldDefinitionEnum.ImperviousArea:
                    return ImperviousArea;
                case FieldDefinitionEnum.InfiltrationDischargeRate:
                    return InfiltrationDischargeRate;
                case FieldDefinitionEnum.InfiltrationSurfaceArea:
                    return InfiltrationSurfaceArea;
                case FieldDefinitionEnum.IsPostMaintenanceAssessment:
                    return IsPostMaintenanceAssessment;
                case FieldDefinitionEnum.IsPrimaryContactOrganization:
                    return IsPrimaryContactOrganization;
                case FieldDefinitionEnum.Jurisdiction:
                    return Jurisdiction;
                case FieldDefinitionEnum.LandUse:
                    return LandUse;
                case FieldDefinitionEnum.LandUseStatistics:
                    return LandUseStatistics;
                case FieldDefinitionEnum.LoadingRate:
                    return LoadingRate;
                case FieldDefinitionEnum.MaintenanceRecord:
                    return MaintenanceRecord;
                case FieldDefinitionEnum.MaintenanceRecordType:
                    return MaintenanceRecordType;
                case FieldDefinitionEnum.MaximumNumberOfObservations:
                    return MaximumNumberOfObservations;
                case FieldDefinitionEnum.MaximumValueOfEachObservation:
                    return MaximumValueOfEachObservation;
                case FieldDefinitionEnum.MeasurementUnit:
                    return MeasurementUnit;
                case FieldDefinitionEnum.MeasurementUnitLabel:
                    return MeasurementUnitLabel;
                case FieldDefinitionEnum.MediaBedFootprint:
                    return MediaBedFootprint;
                case FieldDefinitionEnum.MinimumNumberOfObservations:
                    return MinimumNumberOfObservations;
                case FieldDefinitionEnum.MinimumValueOfEachObservation:
                    return MinimumValueOfEachObservation;
                case FieldDefinitionEnum.MonthsofOperation:
                    return MonthsofOperation;
                case FieldDefinitionEnum.NetworkCatchment:
                    return NetworkCatchment;
                case FieldDefinitionEnum.ObservationCollectionMethod:
                    return ObservationCollectionMethod;
                case FieldDefinitionEnum.ObservationScore:
                    return ObservationScore;
                case FieldDefinitionEnum.ObservationTargetType:
                    return ObservationTargetType;
                case FieldDefinitionEnum.ObservationThresholdType:
                    return ObservationThresholdType;
                case FieldDefinitionEnum.OnlandVisualTrashAssessment:
                    return OnlandVisualTrashAssessment;
                case FieldDefinitionEnum.OnlandVisualTrashAssessmentNotes:
                    return OnlandVisualTrashAssessmentNotes;
                case FieldDefinitionEnum.Organization:
                    return Organization;
                case FieldDefinitionEnum.OrganizationType:
                    return OrganizationType;
                case FieldDefinitionEnum.Parcel:
                    return Parcel;
                case FieldDefinitionEnum.Password:
                    return Password;
                case FieldDefinitionEnum.PermanentPoolorWetlandVolume:
                    return PermanentPoolorWetlandVolume;
                case FieldDefinitionEnum.PhotoCaption:
                    return PhotoCaption;
                case FieldDefinitionEnum.PhotoCredit:
                    return PhotoCredit;
                case FieldDefinitionEnum.PhotoTiming:
                    return PhotoTiming;
                case FieldDefinitionEnum.PrimaryContact:
                    return PrimaryContact;
                case FieldDefinitionEnum.ProgressScore:
                    return ProgressScore;
                case FieldDefinitionEnum.PropertiesToObserve:
                    return PropertiesToObserve;
                case FieldDefinitionEnum.ReceivesSystemCommunications:
                    return ReceivesSystemCommunications;
                case FieldDefinitionEnum.RequiredFieldVisitsPerYear:
                    return RequiredFieldVisitsPerYear;
                case FieldDefinitionEnum.RequiredLifespanOfInstallation:
                    return RequiredLifespanOfInstallation;
                case FieldDefinitionEnum.RequiredPostStormFieldVisitsPerYear:
                    return RequiredPostStormFieldVisitsPerYear;
                case FieldDefinitionEnum.RoleName:
                    return RoleName;
                case FieldDefinitionEnum.RoutingConfiguration:
                    return RoutingConfiguration;
                case FieldDefinitionEnum.SizingBasis:
                    return SizingBasis;
                case FieldDefinitionEnum.StorageVolumeBelowLowestOutletElevation:
                    return StorageVolumeBelowLowestOutletElevation;
                case FieldDefinitionEnum.SummerHarvestedWaterDemand:
                    return SummerHarvestedWaterDemand;
                case FieldDefinitionEnum.TargetLoadReduction:
                    return TargetLoadReduction;
                case FieldDefinitionEnum.TimeofConcentration:
                    return TimeofConcentration;
                case FieldDefinitionEnum.TotalDrawdownTime:
                    return TotalDrawdownTime;
                case FieldDefinitionEnum.TotalEffectiveBMPVolume:
                    return TotalEffectiveBMPVolume;
                case FieldDefinitionEnum.TotalEffectiveDrywellBMPVolume:
                    return TotalEffectiveDrywellBMPVolume;
                case FieldDefinitionEnum.TotalAchieved:
                    return TotalAchieved;
                case FieldDefinitionEnum.TrashCaptureStatus:
                    return TrashCaptureStatus;
                case FieldDefinitionEnum.TreatmentRate:
                    return TreatmentRate;
                case FieldDefinitionEnum.TreatmentBMP:
                    return TreatmentBMP;
                case FieldDefinitionEnum.TreatmentBMPAssessmentObservationType:
                    return TreatmentBMPAssessmentObservationType;
                case FieldDefinitionEnum.TreatmentBMPDesignDepth:
                    return TreatmentBMPDesignDepth;
                case FieldDefinitionEnum.TreatmentBMPType:
                    return TreatmentBMPType;
                case FieldDefinitionEnum.UnderlyingHydrologicSoilGroupHSG:
                    return UnderlyingHydrologicSoilGroupHSG;
                case FieldDefinitionEnum.UnderlyingInfiltrationRate:
                    return UnderlyingInfiltrationRate;
                case FieldDefinitionEnum.UpstreamBMP:
                    return UpstreamBMP;
                case FieldDefinitionEnum.Username:
                    return Username;
                case FieldDefinitionEnum.ViaFullCapture:
                    return ViaFullCapture;
                case FieldDefinitionEnum.ViaOVTAScore:
                    return ViaOVTAScore;
                case FieldDefinitionEnum.ViaPartialCapture:
                    return ViaPartialCapture;
                case FieldDefinitionEnum.WaterQualityDetentionVolume:
                    return WaterQualityDetentionVolume;
                case FieldDefinitionEnum.WaterQualityManagementPlan:
                    return WaterQualityManagementPlan;
                case FieldDefinitionEnum.WaterQualityManagementPlanDocumentType:
                    return WaterQualityManagementPlanDocumentType;
                case FieldDefinitionEnum.WettedFootprint:
                    return WettedFootprint;
                case FieldDefinitionEnum.WinterHarvestedWaterDemand:
                    return WinterHarvestedWaterDemand;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum FieldDefinitionEnum
    {
        IsPrimaryContactOrganization = 1,
        Organization = 2,
        Password = 3,
        MeasurementUnit = 4,
        PhotoCaption = 5,
        PhotoCredit = 6,
        PhotoTiming = 7,
        PrimaryContact = 8,
        OrganizationType = 9,
        Username = 10,
        ExternalLinks = 11,
        RoleName = 12,
        ChartLastUpdatedDate = 13,
        TreatmentBMPType = 14,
        ConveyanceFunctionsAsIntended = 16,
        AssessmentScoreWeight = 17,
        ObservationScore = 18,
        AlternativeScore = 19,
        AssessmentForInternalUseOnly = 20,
        TreatmentBMPDesignDepth = 21,
        ReceivesSystemCommunications = 22,
        Jurisdiction = 23,
        Delineation = 24,
        TreatmentBMP = 25,
        TreatmentBMPAssessmentObservationType = 26,
        ObservationCollectionMethod = 27,
        ObservationThresholdType = 28,
        ObservationTargetType = 29,
        MeasurementUnitLabel = 30,
        PropertiesToObserve = 31,
        MinimumNumberOfObservations = 32,
        MaximumNumberOfObservations = 33,
        MinimumValueOfEachObservation = 34,
        MaximumValueOfEachObservation = 35,
        DefaultThresholdValue = 36,
        DefaultBenchmarkValue = 37,
        AssessmentFailsIfObservationFails = 38,
        CustomAttributeType = 39,
        CustomAttributeDataType = 40,
        MaintenanceRecordType = 41,
        MaintenanceRecord = 42,
        AttributeTypePurpose = 43,
        FundingSource = 44,
        IsPostMaintenanceAssessment = 45,
        FundingEvent = 46,
        FieldVisit = 47,
        FieldVisitStatus = 48,
        WaterQualityManagementPlan = 49,
        Parcel = 50,
        RequiredLifespanOfInstallation = 51,
        RequiredFieldVisitsPerYear = 52,
        RequiredPostStormFieldVisitsPerYear = 53,
        WaterQualityManagementPlanDocumentType = 54,
        HasAllRequiredDocuments = 55,
        DateOfLastInventoryChange = 56,
        TrashCaptureStatus = 57,
        OnlandVisualTrashAssessment = 58,
        OnlandVisualTrashAssessmentNotes = 59,
        DelineationType = 60,
        BaselineScore = 61,
        SizingBasis = 62,
        ProgressScore = 63,
        AssessmentScore = 64,
        ViaFullCapture = 65,
        ViaPartialCapture = 66,
        ViaOVTAScore = 67,
        TotalAchieved = 68,
        TargetLoadReduction = 69,
        LoadingRate = 70,
        LandUse = 71,
        Area = 72,
        ImperviousArea = 73,
        GrossArea = 74,
        LandUseStatistics = 75,
        NetworkCatchment = 76,
        AverageDivertedFlowrate = 77,
        AverageTreatmentFlowrate = 78,
        DesignDryWeatherTreatmentCapacity = 79,
        DesignLowFlowDiversionCapacity = 80,
        DesignMediaFiltrationRate = 81,
        DesignResidenceTimeforPermanentPool = 82,
        DiversionRate = 83,
        DrawdownTimeforWQDetentionVolume = 84,
        EffectiveFootprint = 85,
        EffectiveRetentionDepth = 86,
        InfiltrationDischargeRate = 87,
        InfiltrationSurfaceArea = 88,
        MediaBedFootprint = 89,
        MonthsofOperation = 90,
        PermanentPoolorWetlandVolume = 91,
        RoutingConfiguration = 92,
        StorageVolumeBelowLowestOutletElevation = 93,
        SummerHarvestedWaterDemand = 94,
        TimeofConcentration = 95,
        TotalDrawdownTime = 96,
        TotalEffectiveBMPVolume = 97,
        TotalEffectiveDrywellBMPVolume = 98,
        TreatmentRate = 99,
        UnderlyingHydrologicSoilGroupHSG = 100,
        UnderlyingInfiltrationRate = 101,
        UpstreamBMP = 102,
        WaterQualityDetentionVolume = 103,
        WettedFootprint = 104,
        WinterHarvestedWaterDemand = 105
    }

    public partial class FieldDefinitionIsPrimaryContactOrganization : FieldDefinition
    {
        private FieldDefinitionIsPrimaryContactOrganization(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionIsPrimaryContactOrganization Instance = new FieldDefinitionIsPrimaryContactOrganization(1, @"IsPrimaryContactOrganization", @"Is Primary Contact Organization", @"<p>The entity with primary responsibility for organizing, planning, and executing implementation activities for a project or program. This is usually the lead implementer.</p>", true);
    }

    public partial class FieldDefinitionOrganization : FieldDefinition
    {
        private FieldDefinitionOrganization(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionOrganization Instance = new FieldDefinitionOrganization(2, @"Organization", @"Organization", @"<p>A partner entity that is directly involved with implementation or funding a project.&nbsp;</p>", true);
    }

    public partial class FieldDefinitionPassword : FieldDefinition
    {
        private FieldDefinitionPassword(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionPassword Instance = new FieldDefinitionPassword(3, @"Password", @"Password", @"<p>Password required to log into the Orange County Stormwater Tools in order to access and edit project and program information.</p>", false);
    }

    public partial class FieldDefinitionMeasurementUnit : FieldDefinition
    {
        private FieldDefinitionMeasurementUnit(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMeasurementUnit Instance = new FieldDefinitionMeasurementUnit(4, @"MeasurementUnit", @"Measurement Unit", @"<p>The unit of measure used by an Indicator (aka&nbsp;Performance Measure) to track the extent of implementation.</p>", true);
    }

    public partial class FieldDefinitionPhotoCaption : FieldDefinition
    {
        private FieldDefinitionPhotoCaption(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionPhotoCaption Instance = new FieldDefinitionPhotoCaption(5, @"PhotoCaption", @"Photo Caption", @"<p>A concise yet descriptive explanation of an uploaded photo. Photo captions are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>", true);
    }

    public partial class FieldDefinitionPhotoCredit : FieldDefinition
    {
        private FieldDefinitionPhotoCredit(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionPhotoCredit Instance = new FieldDefinitionPhotoCredit(6, @"PhotoCredit", @"Photo Credit", @"<p>If needed, credit is given to the photographer or owner of an image on the website. Photo credits are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>", true);
    }

    public partial class FieldDefinitionPhotoTiming : FieldDefinition
    {
        private FieldDefinitionPhotoTiming(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionPhotoTiming Instance = new FieldDefinitionPhotoTiming(7, @"PhotoTiming", @"Photo Timing", @"<p>The phase in a project timeline during which the photograph was taken. Photo timing can be before, during or after project implementation.&nbsp;</p>", true);
    }

    public partial class FieldDefinitionPrimaryContact : FieldDefinition
    {
        private FieldDefinitionPrimaryContact(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionPrimaryContact Instance = new FieldDefinitionPrimaryContact(8, @"PrimaryContact", @"Primary Contact", @"<p>An individual at the listed organization responsible for reporting accomplishments and expenditures achieved by the project or program, and who should be contacted when there are questions related to any project associated to the organization.</p>", true);
    }

    public partial class FieldDefinitionOrganizationType : FieldDefinition
    {
        private FieldDefinitionOrganizationType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionOrganizationType Instance = new FieldDefinitionOrganizationType(9, @"OrganizationType", @"Organization Type", @"<p>A categorization of an organization, e.g. Local, State, Federal or Private.</p>", true);
    }

    public partial class FieldDefinitionUsername : FieldDefinition
    {
        private FieldDefinitionUsername(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionUsername Instance = new FieldDefinitionUsername(10, @"Username", @"User name", @"<p>Password required to log into the system&nbsp;order to access and edit project and program information that is not allowed by public users.</p>", true);
    }

    public partial class FieldDefinitionExternalLinks : FieldDefinition
    {
        private FieldDefinitionExternalLinks(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionExternalLinks Instance = new FieldDefinitionExternalLinks(11, @"ExternalLinks", @"External Links", @"<p>Links to external web pages where you might find additional information.</p>", true);
    }

    public partial class FieldDefinitionRoleName : FieldDefinition
    {
        private FieldDefinitionRoleName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionRoleName Instance = new FieldDefinitionRoleName(12, @"RoleName", @"Role Name", @"<p>The name or title describing&nbsp;function or set of permissions that can be assigned to a user.</p>", true);
    }

    public partial class FieldDefinitionChartLastUpdatedDate : FieldDefinition
    {
        private FieldDefinitionChartLastUpdatedDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionChartLastUpdatedDate Instance = new FieldDefinitionChartLastUpdatedDate(13, @"Chart Last Updated Date", @"ChartLastUpdatedDate", @"<p>The date this chart was last updated with current information.</p>", true);
    }

    public partial class FieldDefinitionTreatmentBMPType : FieldDefinition
    {
        private FieldDefinitionTreatmentBMPType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTreatmentBMPType Instance = new FieldDefinitionTreatmentBMPType(14, @"TreatmentBMPType", @"Treatment BMP Type", @"", true);
    }

    public partial class FieldDefinitionConveyanceFunctionsAsIntended : FieldDefinition
    {
        private FieldDefinitionConveyanceFunctionsAsIntended(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionConveyanceFunctionsAsIntended Instance = new FieldDefinitionConveyanceFunctionsAsIntended(16, @"ConveyanceFunctionsAsIntended", @"Conveyance Functions as Intended", @"", true);
    }

    public partial class FieldDefinitionAssessmentScoreWeight : FieldDefinition
    {
        private FieldDefinitionAssessmentScoreWeight(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAssessmentScoreWeight Instance = new FieldDefinitionAssessmentScoreWeight(17, @"AssessmentScoreWeight", @"Assessment Score Weight", @"", true);
    }

    public partial class FieldDefinitionObservationScore : FieldDefinition
    {
        private FieldDefinitionObservationScore(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationScore Instance = new FieldDefinitionObservationScore(18, @"ObservationScore", @"Observation Score", @"", true);
    }

    public partial class FieldDefinitionAlternativeScore : FieldDefinition
    {
        private FieldDefinitionAlternativeScore(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAlternativeScore Instance = new FieldDefinitionAlternativeScore(19, @"AlternativeScore", @"Alternative Score", @"", true);
    }

    public partial class FieldDefinitionAssessmentForInternalUseOnly : FieldDefinition
    {
        private FieldDefinitionAssessmentForInternalUseOnly(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAssessmentForInternalUseOnly Instance = new FieldDefinitionAssessmentForInternalUseOnly(20, @"AssessmentForInternalUseOnly", @"Assessment for Internal Use Only", @"", true);
    }

    public partial class FieldDefinitionTreatmentBMPDesignDepth : FieldDefinition
    {
        private FieldDefinitionTreatmentBMPDesignDepth(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTreatmentBMPDesignDepth Instance = new FieldDefinitionTreatmentBMPDesignDepth(21, @"TreatmentBMPDesignDepth", @"Treatment BMP Design Depth", @"", true);
    }

    public partial class FieldDefinitionReceivesSystemCommunications : FieldDefinition
    {
        private FieldDefinitionReceivesSystemCommunications(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionReceivesSystemCommunications Instance = new FieldDefinitionReceivesSystemCommunications(22, @"ReceivesSystemCommunications", @"Receives System Communications", @"", true);
    }

    public partial class FieldDefinitionJurisdiction : FieldDefinition
    {
        private FieldDefinitionJurisdiction(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionJurisdiction Instance = new FieldDefinitionJurisdiction(23, @"Jurisdiction", @"Jurisdiction", @"", true);
    }

    public partial class FieldDefinitionDelineation : FieldDefinition
    {
        private FieldDefinitionDelineation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDelineation Instance = new FieldDefinitionDelineation(24, @"Delineation", @"Delineation", @"", true);
    }

    public partial class FieldDefinitionTreatmentBMP : FieldDefinition
    {
        private FieldDefinitionTreatmentBMP(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTreatmentBMP Instance = new FieldDefinitionTreatmentBMP(25, @"TreatmentBMP", @"Treatment BMP", @"", true);
    }

    public partial class FieldDefinitionTreatmentBMPAssessmentObservationType : FieldDefinition
    {
        private FieldDefinitionTreatmentBMPAssessmentObservationType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTreatmentBMPAssessmentObservationType Instance = new FieldDefinitionTreatmentBMPAssessmentObservationType(26, @"TreatmentBMPAssessmentObservationType", @"Observation Name", @"", true);
    }

    public partial class FieldDefinitionObservationCollectionMethod : FieldDefinition
    {
        private FieldDefinitionObservationCollectionMethod(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationCollectionMethod Instance = new FieldDefinitionObservationCollectionMethod(27, @"ObservationCollectionMethod", @"Collection Method", @"", true);
    }

    public partial class FieldDefinitionObservationThresholdType : FieldDefinition
    {
        private FieldDefinitionObservationThresholdType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationThresholdType Instance = new FieldDefinitionObservationThresholdType(28, @"ObservationThresholdType", @"Threshold Type", @"", true);
    }

    public partial class FieldDefinitionObservationTargetType : FieldDefinition
    {
        private FieldDefinitionObservationTargetType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationTargetType Instance = new FieldDefinitionObservationTargetType(29, @"ObservationTargetType", @"Target Type", @"", true);
    }

    public partial class FieldDefinitionMeasurementUnitLabel : FieldDefinition
    {
        private FieldDefinitionMeasurementUnitLabel(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMeasurementUnitLabel Instance = new FieldDefinitionMeasurementUnitLabel(30, @"MeasurementUnitLabel", @"Measurement Unit Label", @"", true);
    }

    public partial class FieldDefinitionPropertiesToObserve : FieldDefinition
    {
        private FieldDefinitionPropertiesToObserve(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionPropertiesToObserve Instance = new FieldDefinitionPropertiesToObserve(31, @"PropertiesToObserve", @"Properties To Observe", @"", true);
    }

    public partial class FieldDefinitionMinimumNumberOfObservations : FieldDefinition
    {
        private FieldDefinitionMinimumNumberOfObservations(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMinimumNumberOfObservations Instance = new FieldDefinitionMinimumNumberOfObservations(32, @"MinimumNumberOfObservations", @"Minimum Number of Observations", @"", true);
    }

    public partial class FieldDefinitionMaximumNumberOfObservations : FieldDefinition
    {
        private FieldDefinitionMaximumNumberOfObservations(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMaximumNumberOfObservations Instance = new FieldDefinitionMaximumNumberOfObservations(33, @"MaximumNumberOfObservations", @"Maximum Number of Observations", @"", true);
    }

    public partial class FieldDefinitionMinimumValueOfEachObservation : FieldDefinition
    {
        private FieldDefinitionMinimumValueOfEachObservation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMinimumValueOfEachObservation Instance = new FieldDefinitionMinimumValueOfEachObservation(34, @"MinimumValueOfEachObservation", @"Minimum Value of Each Observation", @"", true);
    }

    public partial class FieldDefinitionMaximumValueOfEachObservation : FieldDefinition
    {
        private FieldDefinitionMaximumValueOfEachObservation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMaximumValueOfEachObservation Instance = new FieldDefinitionMaximumValueOfEachObservation(35, @"MaximumValueOfEachObservation", @"Maximum Value of Each Observation", @"", true);
    }

    public partial class FieldDefinitionDefaultThresholdValue : FieldDefinition
    {
        private FieldDefinitionDefaultThresholdValue(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDefaultThresholdValue Instance = new FieldDefinitionDefaultThresholdValue(36, @"DefaultThresholdValue", @"Default Threshold Value", @"", true);
    }

    public partial class FieldDefinitionDefaultBenchmarkValue : FieldDefinition
    {
        private FieldDefinitionDefaultBenchmarkValue(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDefaultBenchmarkValue Instance = new FieldDefinitionDefaultBenchmarkValue(37, @"DefaultBenchmarkValue", @"Default Benchmark Value", @"", true);
    }

    public partial class FieldDefinitionAssessmentFailsIfObservationFails : FieldDefinition
    {
        private FieldDefinitionAssessmentFailsIfObservationFails(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAssessmentFailsIfObservationFails Instance = new FieldDefinitionAssessmentFailsIfObservationFails(38, @"AssessmentFailsIfObservationFails", @"Assessment Fails if Observation Fails", @"", true);
    }

    public partial class FieldDefinitionCustomAttributeType : FieldDefinition
    {
        private FieldDefinitionCustomAttributeType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionCustomAttributeType Instance = new FieldDefinitionCustomAttributeType(39, @"CustomAttributeType", @"Attribute Name", @"", true);
    }

    public partial class FieldDefinitionCustomAttributeDataType : FieldDefinition
    {
        private FieldDefinitionCustomAttributeDataType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionCustomAttributeDataType Instance = new FieldDefinitionCustomAttributeDataType(40, @"CustomAttributeDataType", @"Data Type", @"", true);
    }

    public partial class FieldDefinitionMaintenanceRecordType : FieldDefinition
    {
        private FieldDefinitionMaintenanceRecordType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMaintenanceRecordType Instance = new FieldDefinitionMaintenanceRecordType(41, @"MaintenanceRecordType", @"Maintenance Type", @"Whether the maintenance performed was Preventative or Corrective maintenance", true);
    }

    public partial class FieldDefinitionMaintenanceRecord : FieldDefinition
    {
        private FieldDefinitionMaintenanceRecord(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMaintenanceRecord Instance = new FieldDefinitionMaintenanceRecord(42, @"MaintenanceRecord", @"Maintenance Record", @"A record of a maintenance activity performed on a Treatment BMP", true);
    }

    public partial class FieldDefinitionAttributeTypePurpose : FieldDefinition
    {
        private FieldDefinitionAttributeTypePurpose(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAttributeTypePurpose Instance = new FieldDefinitionAttributeTypePurpose(43, @"AttributeTypePurpose", @"Purpose", @"How the attribute type will be used for analysis and reporting", true);
    }

    public partial class FieldDefinitionFundingSource : FieldDefinition
    {
        private FieldDefinitionFundingSource(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionFundingSource Instance = new FieldDefinitionFundingSource(44, @"FundingSource", @"Funding Source", @"", true);
    }

    public partial class FieldDefinitionIsPostMaintenanceAssessment : FieldDefinition
    {
        private FieldDefinitionIsPostMaintenanceAssessment(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionIsPostMaintenanceAssessment Instance = new FieldDefinitionIsPostMaintenanceAssessment(45, @"IsPostMaintenanceAssessment", @"Post Maintenance Assessment?", @"Whether the assessment was conducted as a follow-up to a maintenance activity", true);
    }

    public partial class FieldDefinitionFundingEvent : FieldDefinition
    {
        private FieldDefinitionFundingEvent(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionFundingEvent Instance = new FieldDefinitionFundingEvent(46, @"FundingEvent", @"Funding Event", @"A discrete activity (e.g. Project planning, major maintenance, capital construction) that invests in a BMP. A funding event consists of one or funding sources and associated expenditures", true);
    }

    public partial class FieldDefinitionFieldVisit : FieldDefinition
    {
        private FieldDefinitionFieldVisit(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionFieldVisit Instance = new FieldDefinitionFieldVisit(47, @"FieldVisit", @"Field Visit", @"A visit to a Treatment BMP which can consist of an Assessment, a Maintenance Record, and a Post-Maintenance Assessment", true);
    }

    public partial class FieldDefinitionFieldVisitStatus : FieldDefinition
    {
        private FieldDefinitionFieldVisitStatus(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionFieldVisitStatus Instance = new FieldDefinitionFieldVisitStatus(48, @"FieldVisitStatus", @"Field Visit Status", @"Completion status of the Field Visit. A Field Visit can be In Progress, Complete, or left Unresolved. A Treatment BMP may only have one In Progress Field Visit at any given time.", true);
    }

    public partial class FieldDefinitionWaterQualityManagementPlan : FieldDefinition
    {
        private FieldDefinitionWaterQualityManagementPlan(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionWaterQualityManagementPlan Instance = new FieldDefinitionWaterQualityManagementPlan(49, @"WaterQualityManagementPlan", @"Water Quality Management Plan", @"", true);
    }

    public partial class FieldDefinitionParcel : FieldDefinition
    {
        private FieldDefinitionParcel(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionParcel Instance = new FieldDefinitionParcel(50, @"Parcel", @"Parcel", @"", true);
    }

    public partial class FieldDefinitionRequiredLifespanOfInstallation : FieldDefinition
    {
        private FieldDefinitionRequiredLifespanOfInstallation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionRequiredLifespanOfInstallation Instance = new FieldDefinitionRequiredLifespanOfInstallation(51, @"RequiredLifespanOfInstallation", @"Required Lifespan of Installation", @"Specifies when or whether a BMP can be removed", true);
    }

    public partial class FieldDefinitionRequiredFieldVisitsPerYear : FieldDefinition
    {
        private FieldDefinitionRequiredFieldVisitsPerYear(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionRequiredFieldVisitsPerYear Instance = new FieldDefinitionRequiredFieldVisitsPerYear(52, @"RequiredFieldVisitsPerYear", @"Required Field Visits Per Year", @"Number of Field Visists that must be conducted for a given BMP each year", true);
    }

    public partial class FieldDefinitionRequiredPostStormFieldVisitsPerYear : FieldDefinition
    {
        private FieldDefinitionRequiredPostStormFieldVisitsPerYear(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionRequiredPostStormFieldVisitsPerYear Instance = new FieldDefinitionRequiredPostStormFieldVisitsPerYear(53, @"RequiredPostStormFieldVisitsPerYear", @"Required Post-Storm Field Visits Per Year", @"Number of Post-Storm Field Visists that must be conducted for a given BMP each year", true);
    }

    public partial class FieldDefinitionWaterQualityManagementPlanDocumentType : FieldDefinition
    {
        private FieldDefinitionWaterQualityManagementPlanDocumentType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionWaterQualityManagementPlanDocumentType Instance = new FieldDefinitionWaterQualityManagementPlanDocumentType(54, @"WaterQualityManagementPlanDocumentType", @"WQMP Document Type", @"Specifies what type of supporting document this is. Some document types are required for a WQMP to be considered complete", true);
    }

    public partial class FieldDefinitionHasAllRequiredDocuments : FieldDefinition
    {
        private FieldDefinitionHasAllRequiredDocuments(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionHasAllRequiredDocuments Instance = new FieldDefinitionHasAllRequiredDocuments(55, @"HasAllRequiredDocuments", @"Has All Required Documents?", @"Indicates whether all required supporting documents are present for a WQMP", true);
    }

    public partial class FieldDefinitionDateOfLastInventoryChange : FieldDefinition
    {
        private FieldDefinitionDateOfLastInventoryChange(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDateOfLastInventoryChange Instance = new FieldDefinitionDateOfLastInventoryChange(56, @"DateOfLastInventoryChange", @"Date of Last Inventory Change", @"", true);
    }

    public partial class FieldDefinitionTrashCaptureStatus : FieldDefinition
    {
        private FieldDefinitionTrashCaptureStatus(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTrashCaptureStatus Instance = new FieldDefinitionTrashCaptureStatus(57, @"TrashCaptureStatus", @"Trash Capture Status", @"Indicates the ability of this BMP to capture trash.", true);
    }

    public partial class FieldDefinitionOnlandVisualTrashAssessment : FieldDefinition
    {
        private FieldDefinitionOnlandVisualTrashAssessment(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionOnlandVisualTrashAssessment Instance = new FieldDefinitionOnlandVisualTrashAssessment(58, @"OnlandVisualTrashAssessment", @"On-land Visual Trash Assessment", @"The assessing, visually, of trash on land.", true);
    }

    public partial class FieldDefinitionOnlandVisualTrashAssessmentNotes : FieldDefinition
    {
        private FieldDefinitionOnlandVisualTrashAssessmentNotes(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionOnlandVisualTrashAssessmentNotes Instance = new FieldDefinitionOnlandVisualTrashAssessmentNotes(59, @"OnlandVisualTrashAssessmentNotes", @"Comments and Additional Information", @"Enter the name of all assessors and any other notes about the assessment.", true);
    }

    public partial class FieldDefinitionDelineationType : FieldDefinition
    {
        private FieldDefinitionDelineationType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDelineationType Instance = new FieldDefinitionDelineationType(60, @"DelineationType", @"Delineation Type", @"Indicates whether the delineation is distributed or centralized.", true);
    }

    public partial class FieldDefinitionBaselineScore : FieldDefinition
    {
        private FieldDefinitionBaselineScore(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionBaselineScore Instance = new FieldDefinitionBaselineScore(61, @"BaselineScore", @"Baseline Score", @"For an OVTA, scores range from A to D and indicate the condition of the assessed area at the time of the assessment. For an OVTA Area, the score is an aggregate of all of its Assessments' scores.", true);
    }

    public partial class FieldDefinitionSizingBasis : FieldDefinition
    {
        private FieldDefinitionSizingBasis(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionSizingBasis Instance = new FieldDefinitionSizingBasis(62, @"SizingBasis", @"Sizing Basis", @"Indicates whether this BMP is sized for full trash capture, water quality improvement, or otherwise.", true);
    }

    public partial class FieldDefinitionProgressScore : FieldDefinition
    {
        private FieldDefinitionProgressScore(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionProgressScore Instance = new FieldDefinitionProgressScore(63, @"ProgressScore", @"Progress Score", @"", true);
    }

    public partial class FieldDefinitionAssessmentScore : FieldDefinition
    {
        private FieldDefinitionAssessmentScore(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAssessmentScore Instance = new FieldDefinitionAssessmentScore(64, @"AssessmentScore", @"Assessment Score", @"", true);
    }

    public partial class FieldDefinitionViaFullCapture : FieldDefinition
    {
        private FieldDefinitionViaFullCapture(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionViaFullCapture Instance = new FieldDefinitionViaFullCapture(65, @"ViaFullCapture", @"Via Full Capture", @"", true);
    }

    public partial class FieldDefinitionViaPartialCapture : FieldDefinition
    {
        private FieldDefinitionViaPartialCapture(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionViaPartialCapture Instance = new FieldDefinitionViaPartialCapture(66, @"ViaPartialCapture", @"Via Partial Capture", @"", true);
    }

    public partial class FieldDefinitionViaOVTAScore : FieldDefinition
    {
        private FieldDefinitionViaOVTAScore(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionViaOVTAScore Instance = new FieldDefinitionViaOVTAScore(67, @"ViaOVTAScore", @"Via OVTA Score", @"", true);
    }

    public partial class FieldDefinitionTotalAchieved : FieldDefinition
    {
        private FieldDefinitionTotalAchieved(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTotalAchieved Instance = new FieldDefinitionTotalAchieved(68, @"TotalAchieved", @"Total Achieved", @"", true);
    }

    public partial class FieldDefinitionTargetLoadReduction : FieldDefinition
    {
        private FieldDefinitionTargetLoadReduction(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTargetLoadReduction Instance = new FieldDefinitionTargetLoadReduction(69, @"TargetLoadReduction", @"Target Load Reduction", @"", true);
    }

    public partial class FieldDefinitionLoadingRate : FieldDefinition
    {
        private FieldDefinitionLoadingRate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionLoadingRate Instance = new FieldDefinitionLoadingRate(70, @"LoadingRate", @"Loading Rate", @"", true);
    }

    public partial class FieldDefinitionLandUse : FieldDefinition
    {
        private FieldDefinitionLandUse(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionLandUse Instance = new FieldDefinitionLandUse(71, @"LandUse", @"Land Use", @"", true);
    }

    public partial class FieldDefinitionArea : FieldDefinition
    {
        private FieldDefinitionArea(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionArea Instance = new FieldDefinitionArea(72, @"Area", @"Area", @"", true);
    }

    public partial class FieldDefinitionImperviousArea : FieldDefinition
    {
        private FieldDefinitionImperviousArea(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionImperviousArea Instance = new FieldDefinitionImperviousArea(73, @"ImperviousArea", @"Impervious Area", @"", true);
    }

    public partial class FieldDefinitionGrossArea : FieldDefinition
    {
        private FieldDefinitionGrossArea(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionGrossArea Instance = new FieldDefinitionGrossArea(74, @"GrossArea", @"Gross Area", @"", true);
    }

    public partial class FieldDefinitionLandUseStatistics : FieldDefinition
    {
        private FieldDefinitionLandUseStatistics(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionLandUseStatistics Instance = new FieldDefinitionLandUseStatistics(75, @"LandUseStatistics", @"Land Use Statistics", @"", true);
    }

    public partial class FieldDefinitionNetworkCatchment : FieldDefinition
    {
        private FieldDefinitionNetworkCatchment(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionNetworkCatchment Instance = new FieldDefinitionNetworkCatchment(76, @"NetworkCatchment", @"Network Catchment", @"", true);
    }

    public partial class FieldDefinitionAverageDivertedFlowrate : FieldDefinition
    {
        private FieldDefinitionAverageDivertedFlowrate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAverageDivertedFlowrate Instance = new FieldDefinitionAverageDivertedFlowrate(77, @"Average Diverted Flowrate", @"Average Diverted Flowrate", @"Average actual diverted flowrate over the months of operation.", true);
    }

    public partial class FieldDefinitionAverageTreatmentFlowrate : FieldDefinition
    {
        private FieldDefinitionAverageTreatmentFlowrate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionAverageTreatmentFlowrate Instance = new FieldDefinitionAverageTreatmentFlowrate(78, @"Average Treatment Flowrate", @"Average Treatment Flowrate", @"Average actual treated flowrate over the months of operation.", true);
    }

    public partial class FieldDefinitionDesignDryWeatherTreatmentCapacity : FieldDefinition
    {
        private FieldDefinitionDesignDryWeatherTreatmentCapacity(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDesignDryWeatherTreatmentCapacity Instance = new FieldDefinitionDesignDryWeatherTreatmentCapacity(79, @"Design Dry Weather Treatment Capacity", @"Design Dry Weather Treatment Capacity", @"Flow treatment capacity of the BMP.", true);
    }

    public partial class FieldDefinitionDesignLowFlowDiversionCapacity : FieldDefinition
    {
        private FieldDefinitionDesignLowFlowDiversionCapacity(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDesignLowFlowDiversionCapacity Instance = new FieldDefinitionDesignLowFlowDiversionCapacity(80, @"Design Low Flow Diversion Capacity", @"Design Low Flow Diversion Capacity", @"The physical capacity of the low flow diversion or the maximum permitted flow.", true);
    }

    public partial class FieldDefinitionDesignMediaFiltrationRate : FieldDefinition
    {
        private FieldDefinitionDesignMediaFiltrationRate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDesignMediaFiltrationRate Instance = new FieldDefinitionDesignMediaFiltrationRate(81, @"Design Media Filtration Rate", @"Design Media Filtration Rate", @"Design filtration rate through the media bed. This may be controlled by the media permeability or by an outlet control on the underdrain system.", true);
    }

    public partial class FieldDefinitionDesignResidenceTimeforPermanentPool : FieldDefinition
    {
        private FieldDefinitionDesignResidenceTimeforPermanentPool(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDesignResidenceTimeforPermanentPool Instance = new FieldDefinitionDesignResidenceTimeforPermanentPool(82, @"Design Residence Time for Permanent Pool", @"Design Residence Time for Permanent Pool", @"Amount of residence time needed to meet full level of treatment for water that is stored in the permanent pool.", true);
    }

    public partial class FieldDefinitionDiversionRate : FieldDefinition
    {
        private FieldDefinitionDiversionRate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDiversionRate Instance = new FieldDefinitionDiversionRate(83, @"Diversion Rate", @"Diversion Rate", @"Flowrate diverted into the BMP.", true);
    }

    public partial class FieldDefinitionDrawdownTimeforWQDetentionVolume : FieldDefinition
    {
        private FieldDefinitionDrawdownTimeforWQDetentionVolume(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDrawdownTimeforWQDetentionVolume Instance = new FieldDefinitionDrawdownTimeforWQDetentionVolume(84, @"Drawdown Time for WQ Detention Volume", @"Drawdown Time for WQ Detention Volume", @"Time for water quality surcharge volume to draw down after the end of a storm if there is no further inflow.", true);
    }

    public partial class FieldDefinitionEffectiveFootprint : FieldDefinition
    {
        private FieldDefinitionEffectiveFootprint(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionEffectiveFootprint Instance = new FieldDefinitionEffectiveFootprint(85, @"Effective Footprint", @"Effective Footprint", @"The footprint of the BMP that is effective for filtration or infiltration. Unless other information is available, this can be estimated as the wetted footprint when BMP is half full.", true);
    }

    public partial class FieldDefinitionEffectiveRetentionDepth : FieldDefinition
    {
        private FieldDefinitionEffectiveRetentionDepth(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionEffectiveRetentionDepth Instance = new FieldDefinitionEffectiveRetentionDepth(86, @"Effective Retention Depth", @"Effective Retention Depth", @"Depth of water stored in shallow surface depression or media/rock sump for infiltration to occur.", true);
    }

    public partial class FieldDefinitionInfiltrationDischargeRate : FieldDefinition
    {
        private FieldDefinitionInfiltrationDischargeRate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionInfiltrationDischargeRate Instance = new FieldDefinitionInfiltrationDischargeRate(87, @"Infiltration Discharge Rate", @"Infiltration Discharge Rate", @"Design or tested infiltration flowrate of the drywell. This is specified in cubic feet per section, rather than inches per hour.", true);
    }

    public partial class FieldDefinitionInfiltrationSurfaceArea : FieldDefinition
    {
        private FieldDefinitionInfiltrationSurfaceArea(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionInfiltrationSurfaceArea Instance = new FieldDefinitionInfiltrationSurfaceArea(88, @"Infiltration Surface Area", @"Infiltration Surface Area", @"Surface area through which infiltration can occur in the system. If infiltration will occur into the sidewalls of a BMP, it is appropriate to include half of the sidewall area as as part of the infiltration surface area.", true);
    }

    public partial class FieldDefinitionMediaBedFootprint : FieldDefinition
    {
        private FieldDefinitionMediaBedFootprint(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMediaBedFootprint Instance = new FieldDefinitionMediaBedFootprint(89, @"Media Bed Footprint", @"Media Bed Footprint", @"Surface area of the media bed of the BMP.", true);
    }

    public partial class FieldDefinitionMonthsofOperation : FieldDefinition
    {
        private FieldDefinitionMonthsofOperation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionMonthsofOperation Instance = new FieldDefinitionMonthsofOperation(90, @"Months of Operation", @"Months of Operation", @"This defines the months that the facility is operational.", true);
    }

    public partial class FieldDefinitionPermanentPoolorWetlandVolume : FieldDefinition
    {
        private FieldDefinitionPermanentPoolorWetlandVolume(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionPermanentPoolorWetlandVolume Instance = new FieldDefinitionPermanentPoolorWetlandVolume(91, @"Permanent Pool or Wetland Volume", @"Permanent Pool or Wetland Volume", @"Volume of water below the lowest surface outlet. Serves as a wetland or permanent pool. Water may be harvested from this pool. ", true);
    }

    public partial class FieldDefinitionRoutingConfiguration : FieldDefinition
    {
        private FieldDefinitionRoutingConfiguration(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionRoutingConfiguration Instance = new FieldDefinitionRoutingConfiguration(92, @"Routing Configuration", @"Routing Configuration", @"This specifies whether the BMP receives all flow from the drainage area (online), or if there is a diversion structure that limits the flow into the BMP (offline).", true);
    }

    public partial class FieldDefinitionStorageVolumeBelowLowestOutletElevation : FieldDefinition
    {
        private FieldDefinitionStorageVolumeBelowLowestOutletElevation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionStorageVolumeBelowLowestOutletElevation Instance = new FieldDefinitionStorageVolumeBelowLowestOutletElevation(93, @"Storage Volume Below Lowest Outlet Elevation", @"Storage Volume Below Lowest Outlet Elevation", @"The volume of water stored below the lowest outlet (e.g., underdrain, orifice) of the system.", true);
    }

    public partial class FieldDefinitionSummerHarvestedWaterDemand : FieldDefinition
    {
        private FieldDefinitionSummerHarvestedWaterDemand(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionSummerHarvestedWaterDemand Instance = new FieldDefinitionSummerHarvestedWaterDemand(94, @"Summer Harvested Water Demand", @"Summer Harvested Water Demand", @"Average daily harvested water demand from May through October.", true);
    }

    public partial class FieldDefinitionTimeofConcentration : FieldDefinition
    {
        private FieldDefinitionTimeofConcentration(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTimeofConcentration Instance = new FieldDefinitionTimeofConcentration(95, @"Time of Concentration", @"Time of Concentration", @"The time required for the entire drainage to begin contributing runoff to the BMP. This value must be less than 60 minutes. See TGD guidance.", true);
    }

    public partial class FieldDefinitionTotalDrawdownTime : FieldDefinition
    {
        private FieldDefinitionTotalDrawdownTime(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTotalDrawdownTime Instance = new FieldDefinitionTotalDrawdownTime(96, @"Total Drawdown Time", @"Total Drawdown Time", @"Time for the basin to fully draw own after the end of a storm if there is no further inflow.", true);
    }

    public partial class FieldDefinitionTotalEffectiveBMPVolume : FieldDefinition
    {
        private FieldDefinitionTotalEffectiveBMPVolume(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTotalEffectiveBMPVolume Instance = new FieldDefinitionTotalEffectiveBMPVolume(97, @"Total Effective BMP Volume", @"Total Effective BMP Volume", @"The volume of the BMP available for water quality purposes. This includes ponding volume and the available pore volume in media layers and/or in gravel storage layers. It does not include flow control volumes or other volume that is not designed for water quality purposes. ", true);
    }

    public partial class FieldDefinitionTotalEffectiveDrywellBMPVolume : FieldDefinition
    {
        private FieldDefinitionTotalEffectiveDrywellBMPVolume(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTotalEffectiveDrywellBMPVolume Instance = new FieldDefinitionTotalEffectiveDrywellBMPVolume(98, @"Total Effective Drywell BMP Volume", @"Total Effective Drywell BMP Volume", @"The volume of the BMP available for water quality purposes. This includes the volume in any pre-treatment chamber as well as the volume in the well itself.", true);
    }

    public partial class FieldDefinitionTreatmentRate : FieldDefinition
    {
        private FieldDefinitionTreatmentRate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTreatmentRate Instance = new FieldDefinitionTreatmentRate(99, @"Treatment Rate", @"Treatment Rate", @"The flowrate at which the BMP can provide treatment of runoff.", true);
    }

    public partial class FieldDefinitionUnderlyingHydrologicSoilGroupHSG : FieldDefinition
    {
        private FieldDefinitionUnderlyingHydrologicSoilGroupHSG(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionUnderlyingHydrologicSoilGroupHSG Instance = new FieldDefinitionUnderlyingHydrologicSoilGroupHSG(100, @"Underlying Hydrologic Soil Group (HSG)", @"Underlying Hydrologic Soil Group (HSG)", @"Choose the soil group that best represents the soils underlying the BMP. This is used to estimate a default infiltration rate (A = XX, B = XX, C=XX, D=XX)", true);
    }

    public partial class FieldDefinitionUnderlyingInfiltrationRate : FieldDefinition
    {
        private FieldDefinitionUnderlyingInfiltrationRate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionUnderlyingInfiltrationRate Instance = new FieldDefinitionUnderlyingInfiltrationRate(101, @"Underlying Infiltration Rate", @"Underlying Infiltration Rate", @"The underlying infiltration rate below the BMP. This refers to the underlying soil, not engineered media.", true);
    }

    public partial class FieldDefinitionUpstreamBMP : FieldDefinition
    {
        private FieldDefinitionUpstreamBMP(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionUpstreamBMP Instance = new FieldDefinitionUpstreamBMP(102, @"Upstream BMP", @"Upstream BMP", @"Assign a delineation to the BMP through the normal delineation options.<br /><br />OR<br /><br />Indicate that the BMP receives flow from an upstream BMP.", true);
    }

    public partial class FieldDefinitionWaterQualityDetentionVolume : FieldDefinition
    {
        private FieldDefinitionWaterQualityDetentionVolume(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionWaterQualityDetentionVolume Instance = new FieldDefinitionWaterQualityDetentionVolume(103, @"Water Quality Detention Volume", @"Water Quality Detention Volume", @"Volume of water above the surface outlet that provides a water quality detention function; surcharges during storms and drains after storms.  Do not include volume intended for peak flow control.", true);
    }

    public partial class FieldDefinitionWettedFootprint : FieldDefinition
    {
        private FieldDefinitionWettedFootprint(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionWettedFootprint Instance = new FieldDefinitionWettedFootprint(104, @"Wetted Footprint", @"Wetted Footprint", @"Wetted footprint when BMP is half full.", true);
    }

    public partial class FieldDefinitionWinterHarvestedWaterDemand : FieldDefinition
    {
        private FieldDefinitionWinterHarvestedWaterDemand(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionWinterHarvestedWaterDemand Instance = new FieldDefinitionWinterHarvestedWaterDemand(105, @"Winter Harvested Water Demand", @"Winter Harvested Water Demand", @"Average daily harvested water demand from November through April. This should be averaged to account for any shutdowns during wet weather and reduction in demand during the winter season.", true);
    }
}