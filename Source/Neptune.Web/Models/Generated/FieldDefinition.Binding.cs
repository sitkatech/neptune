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
        public static readonly FieldDefinitionTypeOfAssessment TypeOfAssessment = FieldDefinitionTypeOfAssessment.Instance;
        public static readonly FieldDefinitionConveyanceFunctionsAsIntended ConveyanceFunctionsAsIntended = FieldDefinitionConveyanceFunctionsAsIntended.Instance;
        public static readonly FieldDefinitionAssessmentScoreWeight AssessmentScoreWeight = FieldDefinitionAssessmentScoreWeight.Instance;
        public static readonly FieldDefinitionObservationScore ObservationScore = FieldDefinitionObservationScore.Instance;
        public static readonly FieldDefinitionAlternativeScore AlternativeScore = FieldDefinitionAlternativeScore.Instance;
        public static readonly FieldDefinitionAssessmentForInternalUseOnly AssessmentForInternalUseOnly = FieldDefinitionAssessmentForInternalUseOnly.Instance;
        public static readonly FieldDefinitionTreatmentBMPDesignDepth TreatmentBMPDesignDepth = FieldDefinitionTreatmentBMPDesignDepth.Instance;
        public static readonly FieldDefinitionReceivesSystemCommunications ReceivesSystemCommunications = FieldDefinitionReceivesSystemCommunications.Instance;
        public static readonly FieldDefinitionStormwaterJurisdiction StormwaterJurisdiction = FieldDefinitionStormwaterJurisdiction.Instance;
        public static readonly FieldDefinitionModeledCatchment ModeledCatchment = FieldDefinitionModeledCatchment.Instance;
        public static readonly FieldDefinitionTreatmentBMP TreatmentBMP = FieldDefinitionTreatmentBMP.Instance;
        public static readonly FieldDefinitionObservationType ObservationType = FieldDefinitionObservationType.Instance;
        public static readonly FieldDefinitionObservationCollectionMethod ObservationCollectionMethod = FieldDefinitionObservationCollectionMethod.Instance;
        public static readonly FieldDefinitionObservationThresholdType ObservationThresholdType = FieldDefinitionObservationThresholdType.Instance;
        public static readonly FieldDefinitionObservationTargetType ObservationTargetType = FieldDefinitionObservationTargetType.Instance;

        public static readonly List<FieldDefinition> All;
        public static readonly ReadOnlyDictionary<int, FieldDefinition> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static FieldDefinition()
        {
            All = new List<FieldDefinition> { IsPrimaryContactOrganization, Organization, Password, MeasurementUnit, PhotoCaption, PhotoCredit, PhotoTiming, PrimaryContact, OrganizationType, Username, ExternalLinks, RoleName, ChartLastUpdatedDate, TreatmentBMPType, TypeOfAssessment, ConveyanceFunctionsAsIntended, AssessmentScoreWeight, ObservationScore, AlternativeScore, AssessmentForInternalUseOnly, TreatmentBMPDesignDepth, ReceivesSystemCommunications, StormwaterJurisdiction, ModeledCatchment, TreatmentBMP, ObservationType, ObservationCollectionMethod, ObservationThresholdType, ObservationTargetType };
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
        [NotMapped]
        private string DefaultDefinition { get; set; }
        public HtmlString DefaultDefinitionHtmlString
        { 
            get { return DefaultDefinition == null ? null : new HtmlString(DefaultDefinition); }
            set { DefaultDefinition = value?.ToString(); }
        }
        public bool CanCustomizeLabel { get; private set; }
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
                case FieldDefinitionEnum.AssessmentForInternalUseOnly:
                    return AssessmentForInternalUseOnly;
                case FieldDefinitionEnum.AssessmentScoreWeight:
                    return AssessmentScoreWeight;
                case FieldDefinitionEnum.ChartLastUpdatedDate:
                    return ChartLastUpdatedDate;
                case FieldDefinitionEnum.ConveyanceFunctionsAsIntended:
                    return ConveyanceFunctionsAsIntended;
                case FieldDefinitionEnum.ExternalLinks:
                    return ExternalLinks;
                case FieldDefinitionEnum.IsPrimaryContactOrganization:
                    return IsPrimaryContactOrganization;
                case FieldDefinitionEnum.MeasurementUnit:
                    return MeasurementUnit;
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
                case FieldDefinitionEnum.ObservationType:
                    return ObservationType;
                case FieldDefinitionEnum.Organization:
                    return Organization;
                case FieldDefinitionEnum.OrganizationType:
                    return OrganizationType;
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
                case FieldDefinitionEnum.ReceivesSystemCommunications:
                    return ReceivesSystemCommunications;
                case FieldDefinitionEnum.RoleName:
                    return RoleName;
                case FieldDefinitionEnum.StormwaterJurisdiction:
                    return StormwaterJurisdiction;
                case FieldDefinitionEnum.TreatmentBMP:
                    return TreatmentBMP;
                case FieldDefinitionEnum.TreatmentBMPDesignDepth:
                    return TreatmentBMPDesignDepth;
                case FieldDefinitionEnum.TreatmentBMPType:
                    return TreatmentBMPType;
                case FieldDefinitionEnum.TypeOfAssessment:
                    return TypeOfAssessment;
                case FieldDefinitionEnum.Username:
                    return Username;
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
        TypeOfAssessment = 15,
        ConveyanceFunctionsAsIntended = 16,
        AssessmentScoreWeight = 17,
        ObservationScore = 18,
        AlternativeScore = 19,
        AssessmentForInternalUseOnly = 20,
        TreatmentBMPDesignDepth = 21,
        ReceivesSystemCommunications = 22,
        StormwaterJurisdiction = 23,
        ModeledCatchment = 24,
        TreatmentBMP = 25,
        ObservationType = 26,
        ObservationCollectionMethod = 27,
        ObservationThresholdType = 28,
        ObservationTargetType = 29
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
        public static readonly FieldDefinitionPassword Instance = new FieldDefinitionPassword(3, @"Password", @"Password", @"<p>Password required to log into the ProjectNeptune tool in order to access and edit project and program information.</p>", false);
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

    public partial class FieldDefinitionTypeOfAssessment : FieldDefinition
    {
        private FieldDefinitionTypeOfAssessment(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionTypeOfAssessment Instance = new FieldDefinitionTypeOfAssessment(15, @"TypeOfAssessment", @"Type of Assessment", @"", true);
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

    public partial class FieldDefinitionStormwaterJurisdiction : FieldDefinition
    {
        private FieldDefinitionStormwaterJurisdiction(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionStormwaterJurisdiction Instance = new FieldDefinitionStormwaterJurisdiction(23, @"StormwaterJurisdiction", @"Stormwater Jurisdiction", @"", true);
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

    public partial class FieldDefinitionObservationType : FieldDefinition
    {
        private FieldDefinitionObservationType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationType Instance = new FieldDefinitionObservationType(26, @"ObservationType", @"Observation Type", @"", true);
    }

    public partial class FieldDefinitionObservationCollectionMethod : FieldDefinition
    {
        private FieldDefinitionObservationCollectionMethod(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationCollectionMethod Instance = new FieldDefinitionObservationCollectionMethod(27, @"ObservationCollectionMethod", @"Observation Collection Method", @"", true);
    }

    public partial class FieldDefinitionObservationThresholdType : FieldDefinition
    {
        private FieldDefinitionObservationThresholdType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationThresholdType Instance = new FieldDefinitionObservationThresholdType(28, @"ObservationThresholdType", @"Observation Threshold Type", @"", true);
    }

    public partial class FieldDefinitionObservationTargetType : FieldDefinition
    {
        private FieldDefinitionObservationTargetType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition, bool canCustomizeLabel) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition, canCustomizeLabel) {}
        public static readonly FieldDefinitionObservationTargetType Instance = new FieldDefinitionObservationTargetType(29, @"ObservationTargetType", @"Observation Target Type", @"", true);
    }
}