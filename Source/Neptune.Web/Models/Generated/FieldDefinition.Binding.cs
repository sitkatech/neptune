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
        public static readonly FieldDefinitionModeledCatchment ModeledCatchment = FieldDefinitionModeledCatchment.Instance;
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
        public static readonly FieldDefinitionOVTAScore OVTAScore = FieldDefinitionOVTAScore.Instance;

        public static readonly List<FieldDefinition> All;
        public static readonly ReadOnlyDictionary<int, FieldDefinition> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static FieldDefinition()
        {
            All = new List<FieldDefinition> { IsPrimaryContactOrganization, Organization, Password, MeasurementUnit, PhotoCaption, PhotoCredit, PhotoTiming, PrimaryContact, OrganizationType, Username, ExternalLinks, RoleName, ChartLastUpdatedDate, TreatmentBMPType, ConveyanceFunctionsAsIntended, AssessmentScoreWeight, ObservationScore, AlternativeScore, AssessmentForInternalUseOnly, TreatmentBMPDesignDepth, ReceivesSystemCommunications, Jurisdiction, ModeledCatchment, TreatmentBMP, TreatmentBMPAssessmentObservationType, ObservationCollectionMethod, ObservationThresholdType, ObservationTargetType, MeasurementUnitLabel, PropertiesToObserve, MinimumNumberOfObservations, MaximumNumberOfObservations, MinimumValueOfEachObservation, MaximumValueOfEachObservation, DefaultThresholdValue, DefaultBenchmarkValue, AssessmentFailsIfObservationFails, CustomAttributeType, CustomAttributeDataType, MaintenanceRecordType, MaintenanceRecord, AttributeTypePurpose, FundingSource, IsPostMaintenanceAssessment, FundingEvent, FieldVisit, FieldVisitStatus, WaterQualityManagementPlan, Parcel, RequiredLifespanOfInstallation, RequiredFieldVisitsPerYear, RequiredPostStormFieldVisitsPerYear, WaterQualityManagementPlanDocumentType, HasAllRequiredDocuments, DateOfLastInventoryChange, TrashCaptureStatus, OnlandVisualTrashAssessment, OnlandVisualTrashAssessmentNotes, DelineationType, OVTAScore };
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
                case FieldDefinitionEnum.AssessmentFailsIfObservationFails:
                    return AssessmentFailsIfObservationFails;
                case FieldDefinitionEnum.AssessmentForInternalUseOnly:
                    return AssessmentForInternalUseOnly;
                case FieldDefinitionEnum.AssessmentScoreWeight:
                    return AssessmentScoreWeight;
                case FieldDefinitionEnum.AttributeTypePurpose:
                    return AttributeTypePurpose;
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
                case FieldDefinitionEnum.DelineationType:
                    return DelineationType;
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
                case FieldDefinitionEnum.HasAllRequiredDocuments:
                    return HasAllRequiredDocuments;
                case FieldDefinitionEnum.IsPostMaintenanceAssessment:
                    return IsPostMaintenanceAssessment;
                case FieldDefinitionEnum.IsPrimaryContactOrganization:
                    return IsPrimaryContactOrganization;
                case FieldDefinitionEnum.Jurisdiction:
                    return Jurisdiction;
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
                case FieldDefinitionEnum.MinimumNumberOfObservations:
                    return MinimumNumberOfObservations;
                case FieldDefinitionEnum.MinimumValueOfEachObservation:
                    return MinimumValueOfEachObservation;
                case FieldDefinitionEnum.ModeledCatchment:
                    return ModeledCatchment;
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
                case FieldDefinitionEnum.OVTAScore:
                    return OVTAScore;
                case FieldDefinitionEnum.Parcel:
                    return Parcel;
                case FieldDefinitionEnum.Password:
                    return Password;
                case FieldDefinitionEnum.PhotoCaption:
                    return PhotoCaption;
                case FieldDefinitionEnum.PhotoCredit:
                    return PhotoCredit;
                case FieldDefinitionEnum.PhotoTiming:
                    return PhotoTiming;
                case FieldDefinitionEnum.PrimaryContact:
                    return PrimaryContact;
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
                case FieldDefinitionEnum.TrashCaptureStatus:
                    return TrashCaptureStatus;
                case FieldDefinitionEnum.TreatmentBMP:
                    return TreatmentBMP;
                case FieldDefinitionEnum.TreatmentBMPAssessmentObservationType:
                    return TreatmentBMPAssessmentObservationType;
                case FieldDefinitionEnum.TreatmentBMPDesignDepth:
                    return TreatmentBMPDesignDepth;
                case FieldDefinitionEnum.TreatmentBMPType:
                    return TreatmentBMPType;
                case FieldDefinitionEnum.Username:
                    return Username;
                case FieldDefinitionEnum.WaterQualityManagementPlan:
                    return WaterQualityManagementPlan;
                case FieldDefinitionEnum.WaterQualityManagementPlanDocumentType:
                    return WaterQualityManagementPlanDocumentType;
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
        ModeledCatchment = 24,
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
        OVTAScore = 61
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

    public partial class FieldDefinitionModeledCatchment : FieldDefinition
    {
        private FieldDefinitionModeledCatchment(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionModeledCatchment Instance = new FieldDefinitionModeledCatchment(24, @"ModeledCatchment", @"Modeled Catchment", @"", true);
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
        public static readonly FieldDefinitionOnlandVisualTrashAssessmentNotes Instance = new FieldDefinitionOnlandVisualTrashAssessmentNotes(59, @"OnlandVisualTrashAssessmentNotes", @"Notes", @"Enter the name of all assessors and any other notes about the assessment.", true);
    }

    public partial class FieldDefinitionDelineationType : FieldDefinition
    {
        private FieldDefinitionDelineationType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionDelineationType Instance = new FieldDefinitionDelineationType(60, @"DelineationType", @"Delineation Type", @"Indicates whether the delineation is distributed or centralized.", true);
    }

    public partial class FieldDefinitionOVTAScore : FieldDefinition
    {
        private FieldDefinitionOVTAScore(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionOVTAScore Instance = new FieldDefinitionOVTAScore(61, @"OVTAScore", @"OVTA Score", @"For an OVTA, scores range from A to D and indicate the condition of the assessed area at the time of the assessment. For an OVTA Area, the score is an aggregate of all of its Assessments' scores.", true);
    }
}