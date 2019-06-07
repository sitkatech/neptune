//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageType]
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
    public abstract partial class NeptunePageType : IHavePrimaryKey
    {
        public static readonly NeptunePageTypeHomePage HomePage = NeptunePageTypeHomePage.Instance;
        public static readonly NeptunePageTypeAbout About = NeptunePageTypeAbout.Instance;
        public static readonly NeptunePageTypeOrganizationsList OrganizationsList = NeptunePageTypeOrganizationsList.Instance;
        public static readonly NeptunePageTypeHomeMapInfo HomeMapInfo = NeptunePageTypeHomeMapInfo.Instance;
        public static readonly NeptunePageTypeHomeAdditionalInfo HomeAdditionalInfo = NeptunePageTypeHomeAdditionalInfo.Instance;
        public static readonly NeptunePageTypeTreatmentBMP TreatmentBMP = NeptunePageTypeTreatmentBMP.Instance;
        public static readonly NeptunePageTypeTreatmentBMPType TreatmentBMPType = NeptunePageTypeTreatmentBMPType.Instance;
        public static readonly NeptunePageTypeModeledCatchment ModeledCatchment = NeptunePageTypeModeledCatchment.Instance;
        public static readonly NeptunePageTypeJurisdiction Jurisdiction = NeptunePageTypeJurisdiction.Instance;
        public static readonly NeptunePageTypeAssessment Assessment = NeptunePageTypeAssessment.Instance;
        public static readonly NeptunePageTypeManageObservationTypesList ManageObservationTypesList = NeptunePageTypeManageObservationTypesList.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPTypesList ManageTreatmentBMPTypesList = NeptunePageTypeManageTreatmentBMPTypesList.Instance;
        public static readonly NeptunePageTypeManageObservationTypeInstructions ManageObservationTypeInstructions = NeptunePageTypeManageObservationTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageObservationTypeObservationInstructions ManageObservationTypeObservationInstructions = NeptunePageTypeManageObservationTypeObservationInstructions.Instance;
        public static readonly NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions ManageObservationTypeLabelsAndUnitsInstructions = NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPTypeInstructions ManageTreatmentBMPTypeInstructions = NeptunePageTypeManageTreatmentBMPTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageCustomAttributeTypeInstructions ManageCustomAttributeTypeInstructions = NeptunePageTypeManageCustomAttributeTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageCustomAttributeInstructions ManageCustomAttributeInstructions = NeptunePageTypeManageCustomAttributeInstructions.Instance;
        public static readonly NeptunePageTypeManageCustomAttributeTypesList ManageCustomAttributeTypesList = NeptunePageTypeManageCustomAttributeTypesList.Instance;
        public static readonly NeptunePageTypeLegal Legal = NeptunePageTypeLegal.Instance;
        public static readonly NeptunePageTypeFundingSourcesList FundingSourcesList = NeptunePageTypeFundingSourcesList.Instance;
        public static readonly NeptunePageTypeFindABMP FindABMP = NeptunePageTypeFindABMP.Instance;
        public static readonly NeptunePageTypeLaunchPad LaunchPad = NeptunePageTypeLaunchPad.Instance;
        public static readonly NeptunePageTypeFieldRecords FieldRecords = NeptunePageTypeFieldRecords.Instance;
        public static readonly NeptunePageTypeRequestSupport RequestSupport = NeptunePageTypeRequestSupport.Instance;
        public static readonly NeptunePageTypeInviteUser InviteUser = NeptunePageTypeInviteUser.Instance;
        public static readonly NeptunePageTypeWaterQualityMaintenancePlan WaterQualityMaintenancePlan = NeptunePageTypeWaterQualityMaintenancePlan.Instance;
        public static readonly NeptunePageTypeParcelList ParcelList = NeptunePageTypeParcelList.Instance;
        public static readonly NeptunePageTypeTraining Training = NeptunePageTypeTraining.Instance;
        public static readonly NeptunePageTypeManagerDashboard ManagerDashboard = NeptunePageTypeManagerDashboard.Instance;
        public static readonly NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications WaterQualityMaintenancePlanOandMVerifications = NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications.Instance;
        public static readonly NeptunePageTypeModelingHomePage ModelingHomePage = NeptunePageTypeModelingHomePage.Instance;
        public static readonly NeptunePageTypeTrashHomePage TrashHomePage = NeptunePageTypeTrashHomePage.Instance;
        public static readonly NeptunePageTypeOVTAInstructions OVTAInstructions = NeptunePageTypeOVTAInstructions.Instance;
        public static readonly NeptunePageTypeOVTAIndex OVTAIndex = NeptunePageTypeOVTAIndex.Instance;
        public static readonly NeptunePageTypeTrashModuleProgramOverview TrashModuleProgramOverview = NeptunePageTypeTrashModuleProgramOverview.Instance;
        public static readonly NeptunePageTypeDelineationMap DelineationMap = NeptunePageTypeDelineationMap.Instance;
        public static readonly NeptunePageTypeBulkUploadRequest BulkUploadRequest = NeptunePageTypeBulkUploadRequest.Instance;
        public static readonly NeptunePageTypeDroolToolHomePage DroolToolHomePage = NeptunePageTypeDroolToolHomePage.Instance;
        public static readonly NeptunePageTypeDroolToolAboutPage DroolToolAboutPage = NeptunePageTypeDroolToolAboutPage.Instance;
        public static readonly NeptunePageTypeTreatmentBMPAssessment TreatmentBMPAssessment = NeptunePageTypeTreatmentBMPAssessment.Instance;

        public static readonly List<NeptunePageType> All;
        public static readonly ReadOnlyDictionary<int, NeptunePageType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static NeptunePageType()
        {
            All = new List<NeptunePageType> { HomePage, About, OrganizationsList, HomeMapInfo, HomeAdditionalInfo, TreatmentBMP, TreatmentBMPType, ModeledCatchment, Jurisdiction, Assessment, ManageObservationTypesList, ManageTreatmentBMPTypesList, ManageObservationTypeInstructions, ManageObservationTypeObservationInstructions, ManageObservationTypeLabelsAndUnitsInstructions, ManageTreatmentBMPTypeInstructions, ManageCustomAttributeTypeInstructions, ManageCustomAttributeInstructions, ManageCustomAttributeTypesList, Legal, FundingSourcesList, FindABMP, LaunchPad, FieldRecords, RequestSupport, InviteUser, WaterQualityMaintenancePlan, ParcelList, Training, ManagerDashboard, WaterQualityMaintenancePlanOandMVerifications, ModelingHomePage, TrashHomePage, OVTAInstructions, OVTAIndex, TrashModuleProgramOverview, DelineationMap, BulkUploadRequest, DroolToolHomePage, DroolToolAboutPage, TreatmentBMPAssessment };
            AllLookupDictionary = new ReadOnlyDictionary<int, NeptunePageType>(All.ToDictionary(x => x.NeptunePageTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected NeptunePageType(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID)
        {
            NeptunePageTypeID = neptunePageTypeID;
            NeptunePageTypeName = neptunePageTypeName;
            NeptunePageTypeDisplayName = neptunePageTypeDisplayName;
            NeptunePageRenderTypeID = neptunePageRenderTypeID;
        }
        public NeptunePageRenderType NeptunePageRenderType { get { return NeptunePageRenderType.AllLookupDictionary[NeptunePageRenderTypeID]; } }
        [Key]
        public int NeptunePageTypeID { get; private set; }
        public string NeptunePageTypeName { get; private set; }
        public string NeptunePageTypeDisplayName { get; private set; }
        public int NeptunePageRenderTypeID { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return NeptunePageTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(NeptunePageType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.NeptunePageTypeID == NeptunePageTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as NeptunePageType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return NeptunePageTypeID;
        }

        public static bool operator ==(NeptunePageType left, NeptunePageType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NeptunePageType left, NeptunePageType right)
        {
            return !Equals(left, right);
        }

        public NeptunePageTypeEnum ToEnum { get { return (NeptunePageTypeEnum)GetHashCode(); } }

        public static NeptunePageType ToType(int enumValue)
        {
            return ToType((NeptunePageTypeEnum)enumValue);
        }

        public static NeptunePageType ToType(NeptunePageTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case NeptunePageTypeEnum.About:
                    return About;
                case NeptunePageTypeEnum.Assessment:
                    return Assessment;
                case NeptunePageTypeEnum.BulkUploadRequest:
                    return BulkUploadRequest;
                case NeptunePageTypeEnum.DelineationMap:
                    return DelineationMap;
                case NeptunePageTypeEnum.DroolToolAboutPage:
                    return DroolToolAboutPage;
                case NeptunePageTypeEnum.DroolToolHomePage:
                    return DroolToolHomePage;
                case NeptunePageTypeEnum.FieldRecords:
                    return FieldRecords;
                case NeptunePageTypeEnum.FindABMP:
                    return FindABMP;
                case NeptunePageTypeEnum.FundingSourcesList:
                    return FundingSourcesList;
                case NeptunePageTypeEnum.HomeAdditionalInfo:
                    return HomeAdditionalInfo;
                case NeptunePageTypeEnum.HomeMapInfo:
                    return HomeMapInfo;
                case NeptunePageTypeEnum.HomePage:
                    return HomePage;
                case NeptunePageTypeEnum.InviteUser:
                    return InviteUser;
                case NeptunePageTypeEnum.Jurisdiction:
                    return Jurisdiction;
                case NeptunePageTypeEnum.LaunchPad:
                    return LaunchPad;
                case NeptunePageTypeEnum.Legal:
                    return Legal;
                case NeptunePageTypeEnum.ManageCustomAttributeInstructions:
                    return ManageCustomAttributeInstructions;
                case NeptunePageTypeEnum.ManageCustomAttributeTypeInstructions:
                    return ManageCustomAttributeTypeInstructions;
                case NeptunePageTypeEnum.ManageCustomAttributeTypesList:
                    return ManageCustomAttributeTypesList;
                case NeptunePageTypeEnum.ManageObservationTypeInstructions:
                    return ManageObservationTypeInstructions;
                case NeptunePageTypeEnum.ManageObservationTypeLabelsAndUnitsInstructions:
                    return ManageObservationTypeLabelsAndUnitsInstructions;
                case NeptunePageTypeEnum.ManageObservationTypeObservationInstructions:
                    return ManageObservationTypeObservationInstructions;
                case NeptunePageTypeEnum.ManageObservationTypesList:
                    return ManageObservationTypesList;
                case NeptunePageTypeEnum.ManagerDashboard:
                    return ManagerDashboard;
                case NeptunePageTypeEnum.ManageTreatmentBMPTypeInstructions:
                    return ManageTreatmentBMPTypeInstructions;
                case NeptunePageTypeEnum.ManageTreatmentBMPTypesList:
                    return ManageTreatmentBMPTypesList;
                case NeptunePageTypeEnum.ModeledCatchment:
                    return ModeledCatchment;
                case NeptunePageTypeEnum.ModelingHomePage:
                    return ModelingHomePage;
                case NeptunePageTypeEnum.OrganizationsList:
                    return OrganizationsList;
                case NeptunePageTypeEnum.OVTAIndex:
                    return OVTAIndex;
                case NeptunePageTypeEnum.OVTAInstructions:
                    return OVTAInstructions;
                case NeptunePageTypeEnum.ParcelList:
                    return ParcelList;
                case NeptunePageTypeEnum.RequestSupport:
                    return RequestSupport;
                case NeptunePageTypeEnum.Training:
                    return Training;
                case NeptunePageTypeEnum.TrashHomePage:
                    return TrashHomePage;
                case NeptunePageTypeEnum.TrashModuleProgramOverview:
                    return TrashModuleProgramOverview;
                case NeptunePageTypeEnum.TreatmentBMP:
                    return TreatmentBMP;
                case NeptunePageTypeEnum.TreatmentBMPAssessment:
                    return TreatmentBMPAssessment;
                case NeptunePageTypeEnum.TreatmentBMPType:
                    return TreatmentBMPType;
                case NeptunePageTypeEnum.WaterQualityMaintenancePlan:
                    return WaterQualityMaintenancePlan;
                case NeptunePageTypeEnum.WaterQualityMaintenancePlanOandMVerifications:
                    return WaterQualityMaintenancePlanOandMVerifications;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum NeptunePageTypeEnum
    {
        HomePage = 1,
        About = 2,
        OrganizationsList = 3,
        HomeMapInfo = 4,
        HomeAdditionalInfo = 5,
        TreatmentBMP = 6,
        TreatmentBMPType = 7,
        ModeledCatchment = 8,
        Jurisdiction = 9,
        Assessment = 10,
        ManageObservationTypesList = 11,
        ManageTreatmentBMPTypesList = 12,
        ManageObservationTypeInstructions = 13,
        ManageObservationTypeObservationInstructions = 14,
        ManageObservationTypeLabelsAndUnitsInstructions = 15,
        ManageTreatmentBMPTypeInstructions = 16,
        ManageCustomAttributeTypeInstructions = 17,
        ManageCustomAttributeInstructions = 18,
        ManageCustomAttributeTypesList = 19,
        Legal = 20,
        FundingSourcesList = 21,
        FindABMP = 22,
        LaunchPad = 23,
        FieldRecords = 24,
        RequestSupport = 25,
        InviteUser = 26,
        WaterQualityMaintenancePlan = 27,
        ParcelList = 28,
        Training = 29,
        ManagerDashboard = 30,
        WaterQualityMaintenancePlanOandMVerifications = 31,
        ModelingHomePage = 32,
        TrashHomePage = 33,
        OVTAInstructions = 34,
        OVTAIndex = 35,
        TrashModuleProgramOverview = 36,
        DelineationMap = 37,
        BulkUploadRequest = 38,
        DroolToolHomePage = 39,
        DroolToolAboutPage = 40,
        TreatmentBMPAssessment = 41
    }

    public partial class NeptunePageTypeHomePage : NeptunePageType
    {
        private NeptunePageTypeHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeHomePage Instance = new NeptunePageTypeHomePage(1, @"HomePage", @"Home Page", 2);
    }

    public partial class NeptunePageTypeAbout : NeptunePageType
    {
        private NeptunePageTypeAbout(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeAbout Instance = new NeptunePageTypeAbout(2, @"About", @"About", 2);
    }

    public partial class NeptunePageTypeOrganizationsList : NeptunePageType
    {
        private NeptunePageTypeOrganizationsList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeOrganizationsList Instance = new NeptunePageTypeOrganizationsList(3, @"OrganizationsList", @"Organizations List", 1);
    }

    public partial class NeptunePageTypeHomeMapInfo : NeptunePageType
    {
        private NeptunePageTypeHomeMapInfo(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeHomeMapInfo Instance = new NeptunePageTypeHomeMapInfo(4, @"HomeMapInfo", @"Home Page Map Info", 2);
    }

    public partial class NeptunePageTypeHomeAdditionalInfo : NeptunePageType
    {
        private NeptunePageTypeHomeAdditionalInfo(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeHomeAdditionalInfo Instance = new NeptunePageTypeHomeAdditionalInfo(5, @"HomeAdditionalInfo", @"Home Page Additional Info", 2);
    }

    public partial class NeptunePageTypeTreatmentBMP : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMP(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTreatmentBMP Instance = new NeptunePageTypeTreatmentBMP(6, @"TreatmentBMP", @"Treatment BMP", 2);
    }

    public partial class NeptunePageTypeTreatmentBMPType : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMPType(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTreatmentBMPType Instance = new NeptunePageTypeTreatmentBMPType(7, @"TreatmentBMPType", @"Treatment BMP Type", 2);
    }

    public partial class NeptunePageTypeModeledCatchment : NeptunePageType
    {
        private NeptunePageTypeModeledCatchment(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeModeledCatchment Instance = new NeptunePageTypeModeledCatchment(8, @"ModeledCatchment", @"Modeled Catchment", 2);
    }

    public partial class NeptunePageTypeJurisdiction : NeptunePageType
    {
        private NeptunePageTypeJurisdiction(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeJurisdiction Instance = new NeptunePageTypeJurisdiction(9, @"Jurisdiction", @"Jurisdiction", 2);
    }

    public partial class NeptunePageTypeAssessment : NeptunePageType
    {
        private NeptunePageTypeAssessment(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeAssessment Instance = new NeptunePageTypeAssessment(10, @"Assessment", @"Assessment", 2);
    }

    public partial class NeptunePageTypeManageObservationTypesList : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypesList Instance = new NeptunePageTypeManageObservationTypesList(11, @"ManageObservationTypesList", @"Manage Observation Types List", 2);
    }

    public partial class NeptunePageTypeManageTreatmentBMPTypesList : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageTreatmentBMPTypesList Instance = new NeptunePageTypeManageTreatmentBMPTypesList(12, @"ManageTreatmentBMPTypesList", @"Manage Treatment BMP Types List", 2);
    }

    public partial class NeptunePageTypeManageObservationTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypeInstructions Instance = new NeptunePageTypeManageObservationTypeInstructions(13, @"ManageObservationTypeInstructions", @"Manage Observation Type Instructions", 2);
    }

    public partial class NeptunePageTypeManageObservationTypeObservationInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeObservationInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypeObservationInstructions Instance = new NeptunePageTypeManageObservationTypeObservationInstructions(14, @"ManageObservationTypeObservationInstructions", @"Manage Observation Type Instructions for Observation Instructions", 2);
    }

    public partial class NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions Instance = new NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions(15, @"ManageObservationTypeLabelsAndUnitsInstructions", @"Manage Observation Type Labels and Units Instructions", 2);
    }

    public partial class NeptunePageTypeManageTreatmentBMPTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageTreatmentBMPTypeInstructions Instance = new NeptunePageTypeManageTreatmentBMPTypeInstructions(16, @"ManageTreatmentBMPTypeInstructions", @"Manage Treatment BMP Type Instructions", 2);
    }

    public partial class NeptunePageTypeManageCustomAttributeTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageCustomAttributeTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageCustomAttributeTypeInstructions Instance = new NeptunePageTypeManageCustomAttributeTypeInstructions(17, @"ManageCustomAttributeTypeInstructions", @"Manage Custom Attribute Type Instructions", 2);
    }

    public partial class NeptunePageTypeManageCustomAttributeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageCustomAttributeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageCustomAttributeInstructions Instance = new NeptunePageTypeManageCustomAttributeInstructions(18, @"ManageCustomAttributeInstructions", @"Manage Custom Attribute Instructions", 2);
    }

    public partial class NeptunePageTypeManageCustomAttributeTypesList : NeptunePageType
    {
        private NeptunePageTypeManageCustomAttributeTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManageCustomAttributeTypesList Instance = new NeptunePageTypeManageCustomAttributeTypesList(19, @"ManageCustomAttributeTypesList", @"Manage Custom Attribute Types List", 2);
    }

    public partial class NeptunePageTypeLegal : NeptunePageType
    {
        private NeptunePageTypeLegal(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeLegal Instance = new NeptunePageTypeLegal(20, @"Legal", @"Legal", 2);
    }

    public partial class NeptunePageTypeFundingSourcesList : NeptunePageType
    {
        private NeptunePageTypeFundingSourcesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeFundingSourcesList Instance = new NeptunePageTypeFundingSourcesList(21, @"FundingSourcesList", @"Funding Sources List", 2);
    }

    public partial class NeptunePageTypeFindABMP : NeptunePageType
    {
        private NeptunePageTypeFindABMP(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeFindABMP Instance = new NeptunePageTypeFindABMP(22, @"FindABMP", @"Find a BMP", 2);
    }

    public partial class NeptunePageTypeLaunchPad : NeptunePageType
    {
        private NeptunePageTypeLaunchPad(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeLaunchPad Instance = new NeptunePageTypeLaunchPad(23, @"LaunchPad", @"Launch Pad", 2);
    }

    public partial class NeptunePageTypeFieldRecords : NeptunePageType
    {
        private NeptunePageTypeFieldRecords(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeFieldRecords Instance = new NeptunePageTypeFieldRecords(24, @"FieldRecords", @"Field Records", 2);
    }

    public partial class NeptunePageTypeRequestSupport : NeptunePageType
    {
        private NeptunePageTypeRequestSupport(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeRequestSupport Instance = new NeptunePageTypeRequestSupport(25, @"RequestSupport", @"Request Support", 2);
    }

    public partial class NeptunePageTypeInviteUser : NeptunePageType
    {
        private NeptunePageTypeInviteUser(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeInviteUser Instance = new NeptunePageTypeInviteUser(26, @"InviteUser", @"Invite User", 2);
    }

    public partial class NeptunePageTypeWaterQualityMaintenancePlan : NeptunePageType
    {
        private NeptunePageTypeWaterQualityMaintenancePlan(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeWaterQualityMaintenancePlan Instance = new NeptunePageTypeWaterQualityMaintenancePlan(27, @"WaterQualityMaintenancePlan", @"Water Quality Maintenance Plan", 2);
    }

    public partial class NeptunePageTypeParcelList : NeptunePageType
    {
        private NeptunePageTypeParcelList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeParcelList Instance = new NeptunePageTypeParcelList(28, @"ParcelList", @"Parcel List", 2);
    }

    public partial class NeptunePageTypeTraining : NeptunePageType
    {
        private NeptunePageTypeTraining(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTraining Instance = new NeptunePageTypeTraining(29, @"Training", @"Training", 2);
    }

    public partial class NeptunePageTypeManagerDashboard : NeptunePageType
    {
        private NeptunePageTypeManagerDashboard(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeManagerDashboard Instance = new NeptunePageTypeManagerDashboard(30, @"ManagerDashboard", @"Manager Dashboard", 2);
    }

    public partial class NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications : NeptunePageType
    {
        private NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications Instance = new NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications(31, @"WaterQualityMaintenancePlanOandMVerifications", @"Water Quality Maintenance Plan O&M Verifications", 2);
    }

    public partial class NeptunePageTypeModelingHomePage : NeptunePageType
    {
        private NeptunePageTypeModelingHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeModelingHomePage Instance = new NeptunePageTypeModelingHomePage(32, @"ModelingHomePage", @"Modeling Home Page", 2);
    }

    public partial class NeptunePageTypeTrashHomePage : NeptunePageType
    {
        private NeptunePageTypeTrashHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTrashHomePage Instance = new NeptunePageTypeTrashHomePage(33, @"TrashHomePage", @"Trash Module Home Page", 2);
    }

    public partial class NeptunePageTypeOVTAInstructions : NeptunePageType
    {
        private NeptunePageTypeOVTAInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeOVTAInstructions Instance = new NeptunePageTypeOVTAInstructions(34, @"OVTAInstructions", @"OVTA Instructions", 2);
    }

    public partial class NeptunePageTypeOVTAIndex : NeptunePageType
    {
        private NeptunePageTypeOVTAIndex(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeOVTAIndex Instance = new NeptunePageTypeOVTAIndex(35, @"OVTAIndex", @"OVTA Index", 2);
    }

    public partial class NeptunePageTypeTrashModuleProgramOverview : NeptunePageType
    {
        private NeptunePageTypeTrashModuleProgramOverview(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTrashModuleProgramOverview Instance = new NeptunePageTypeTrashModuleProgramOverview(36, @"TrashModuleProgramOverview", @"Trash Module Program Overview", 2);
    }

    public partial class NeptunePageTypeDelineationMap : NeptunePageType
    {
        private NeptunePageTypeDelineationMap(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeDelineationMap Instance = new NeptunePageTypeDelineationMap(37, @"DelineationMap", @"Delineation Map", 2);
    }

    public partial class NeptunePageTypeBulkUploadRequest : NeptunePageType
    {
        private NeptunePageTypeBulkUploadRequest(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeBulkUploadRequest Instance = new NeptunePageTypeBulkUploadRequest(38, @"BulkUploadRequest", @"Bulk Upload Request", 2);
    }

    public partial class NeptunePageTypeDroolToolHomePage : NeptunePageType
    {
        private NeptunePageTypeDroolToolHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeDroolToolHomePage Instance = new NeptunePageTypeDroolToolHomePage(39, @"DroolToolHomePage", @"Drool Tool Home Page", 2);
    }

    public partial class NeptunePageTypeDroolToolAboutPage : NeptunePageType
    {
        private NeptunePageTypeDroolToolAboutPage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeDroolToolAboutPage Instance = new NeptunePageTypeDroolToolAboutPage(40, @"DroolToolAboutPage", @"Drool Tool About Page", 2);
    }

    public partial class NeptunePageTypeTreatmentBMPAssessment : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMPAssessment(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName, int neptunePageRenderTypeID) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName, neptunePageRenderTypeID) {}
        public static readonly NeptunePageTypeTreatmentBMPAssessment Instance = new NeptunePageTypeTreatmentBMPAssessment(41, @"TreatmentBMPAssessment", @"Treatment BMP Assessment", 2);
    }
}