//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class NeptunePageType : IHavePrimaryKey
    {
        public static readonly NeptunePageTypeHomePage HomePage = Neptune.EFModels.Entities.NeptunePageTypeHomePage.Instance;
        public static readonly NeptunePageTypeAbout About = Neptune.EFModels.Entities.NeptunePageTypeAbout.Instance;
        public static readonly NeptunePageTypeOrganizationsList OrganizationsList = Neptune.EFModels.Entities.NeptunePageTypeOrganizationsList.Instance;
        public static readonly NeptunePageTypeHomeMapInfo HomeMapInfo = Neptune.EFModels.Entities.NeptunePageTypeHomeMapInfo.Instance;
        public static readonly NeptunePageTypeHomeAdditionalInfo HomeAdditionalInfo = Neptune.EFModels.Entities.NeptunePageTypeHomeAdditionalInfo.Instance;
        public static readonly NeptunePageTypeTreatmentBMP TreatmentBMP = Neptune.EFModels.Entities.NeptunePageTypeTreatmentBMP.Instance;
        public static readonly NeptunePageTypeTreatmentBMPType TreatmentBMPType = Neptune.EFModels.Entities.NeptunePageTypeTreatmentBMPType.Instance;
        public static readonly NeptunePageTypeJurisdiction Jurisdiction = Neptune.EFModels.Entities.NeptunePageTypeJurisdiction.Instance;
        public static readonly NeptunePageTypeAssessment Assessment = Neptune.EFModels.Entities.NeptunePageTypeAssessment.Instance;
        public static readonly NeptunePageTypeManageObservationTypesList ManageObservationTypesList = Neptune.EFModels.Entities.NeptunePageTypeManageObservationTypesList.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPTypesList ManageTreatmentBMPTypesList = Neptune.EFModels.Entities.NeptunePageTypeManageTreatmentBMPTypesList.Instance;
        public static readonly NeptunePageTypeManageObservationTypeInstructions ManageObservationTypeInstructions = Neptune.EFModels.Entities.NeptunePageTypeManageObservationTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageObservationTypeObservationInstructions ManageObservationTypeObservationInstructions = Neptune.EFModels.Entities.NeptunePageTypeManageObservationTypeObservationInstructions.Instance;
        public static readonly NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions ManageObservationTypeLabelsAndUnitsInstructions = Neptune.EFModels.Entities.NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions.Instance;
        public static readonly NeptunePageTypeManageTreatmentBMPTypeInstructions ManageTreatmentBMPTypeInstructions = Neptune.EFModels.Entities.NeptunePageTypeManageTreatmentBMPTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageCustomAttributeTypeInstructions ManageCustomAttributeTypeInstructions = Neptune.EFModels.Entities.NeptunePageTypeManageCustomAttributeTypeInstructions.Instance;
        public static readonly NeptunePageTypeManageCustomAttributeInstructions ManageCustomAttributeInstructions = Neptune.EFModels.Entities.NeptunePageTypeManageCustomAttributeInstructions.Instance;
        public static readonly NeptunePageTypeManageCustomAttributeTypesList ManageCustomAttributeTypesList = Neptune.EFModels.Entities.NeptunePageTypeManageCustomAttributeTypesList.Instance;
        public static readonly NeptunePageTypeLegal Legal = Neptune.EFModels.Entities.NeptunePageTypeLegal.Instance;
        public static readonly NeptunePageTypeFundingSourcesList FundingSourcesList = Neptune.EFModels.Entities.NeptunePageTypeFundingSourcesList.Instance;
        public static readonly NeptunePageTypeFindABMP FindABMP = Neptune.EFModels.Entities.NeptunePageTypeFindABMP.Instance;
        public static readonly NeptunePageTypeLaunchPad LaunchPad = Neptune.EFModels.Entities.NeptunePageTypeLaunchPad.Instance;
        public static readonly NeptunePageTypeFieldRecords FieldRecords = Neptune.EFModels.Entities.NeptunePageTypeFieldRecords.Instance;
        public static readonly NeptunePageTypeRequestSupport RequestSupport = Neptune.EFModels.Entities.NeptunePageTypeRequestSupport.Instance;
        public static readonly NeptunePageTypeInviteUser InviteUser = Neptune.EFModels.Entities.NeptunePageTypeInviteUser.Instance;
        public static readonly NeptunePageTypeWaterQualityMaintenancePlan WaterQualityMaintenancePlan = Neptune.EFModels.Entities.NeptunePageTypeWaterQualityMaintenancePlan.Instance;
        public static readonly NeptunePageTypeParcelList ParcelList = Neptune.EFModels.Entities.NeptunePageTypeParcelList.Instance;
        public static readonly NeptunePageTypeTraining Training = Neptune.EFModels.Entities.NeptunePageTypeTraining.Instance;
        public static readonly NeptunePageTypeManagerDashboard ManagerDashboard = Neptune.EFModels.Entities.NeptunePageTypeManagerDashboard.Instance;
        public static readonly NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications WaterQualityMaintenancePlanOandMVerifications = Neptune.EFModels.Entities.NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications.Instance;
        public static readonly NeptunePageTypeModelingHomePage ModelingHomePage = Neptune.EFModels.Entities.NeptunePageTypeModelingHomePage.Instance;
        public static readonly NeptunePageTypeTrashHomePage TrashHomePage = Neptune.EFModels.Entities.NeptunePageTypeTrashHomePage.Instance;
        public static readonly NeptunePageTypeOVTAInstructions OVTAInstructions = Neptune.EFModels.Entities.NeptunePageTypeOVTAInstructions.Instance;
        public static readonly NeptunePageTypeOVTAIndex OVTAIndex = Neptune.EFModels.Entities.NeptunePageTypeOVTAIndex.Instance;
        public static readonly NeptunePageTypeTrashModuleProgramOverview TrashModuleProgramOverview = Neptune.EFModels.Entities.NeptunePageTypeTrashModuleProgramOverview.Instance;
        public static readonly NeptunePageTypeDelineationMap DelineationMap = Neptune.EFModels.Entities.NeptunePageTypeDelineationMap.Instance;
        public static readonly NeptunePageTypeBulkUploadRequest BulkUploadRequest = Neptune.EFModels.Entities.NeptunePageTypeBulkUploadRequest.Instance;
        public static readonly NeptunePageTypeTreatmentBMPAssessment TreatmentBMPAssessment = Neptune.EFModels.Entities.NeptunePageTypeTreatmentBMPAssessment.Instance;
        public static readonly NeptunePageTypeEditOVTAArea EditOVTAArea = Neptune.EFModels.Entities.NeptunePageTypeEditOVTAArea.Instance;
        public static readonly NeptunePageTypeLandUseBlock LandUseBlock = Neptune.EFModels.Entities.NeptunePageTypeLandUseBlock.Instance;
        public static readonly NeptunePageTypeExportAssessmentGeospatialData ExportAssessmentGeospatialData = Neptune.EFModels.Entities.NeptunePageTypeExportAssessmentGeospatialData.Instance;
        public static readonly NeptunePageTypeHRUCharacteristics HRUCharacteristics = Neptune.EFModels.Entities.NeptunePageTypeHRUCharacteristics.Instance;
        public static readonly NeptunePageTypeRegionalSubbasins RegionalSubbasins = Neptune.EFModels.Entities.NeptunePageTypeRegionalSubbasins.Instance;
        public static readonly NeptunePageTypeDelineationReconciliationReport DelineationReconciliationReport = Neptune.EFModels.Entities.NeptunePageTypeDelineationReconciliationReport.Instance;
        public static readonly NeptunePageTypeViewTreatmentBMPModelingAttributes ViewTreatmentBMPModelingAttributes = Neptune.EFModels.Entities.NeptunePageTypeViewTreatmentBMPModelingAttributes.Instance;
        public static readonly NeptunePageTypeUploadTreatmentBMPs UploadTreatmentBMPs = Neptune.EFModels.Entities.NeptunePageTypeUploadTreatmentBMPs.Instance;
        public static readonly NeptunePageTypeAboutModelingBMPPerformance AboutModelingBMPPerformance = Neptune.EFModels.Entities.NeptunePageTypeAboutModelingBMPPerformance.Instance;
        public static readonly NeptunePageTypeBulkUploadFieldVisits BulkUploadFieldVisits = Neptune.EFModels.Entities.NeptunePageTypeBulkUploadFieldVisits.Instance;
        public static readonly NeptunePageTypeHippocampHomePage HippocampHomePage = Neptune.EFModels.Entities.NeptunePageTypeHippocampHomePage.Instance;
        public static readonly NeptunePageTypeHippocampTraining HippocampTraining = Neptune.EFModels.Entities.NeptunePageTypeHippocampTraining.Instance;
        public static readonly NeptunePageTypeHippocampLabelsAndDefinitionsList HippocampLabelsAndDefinitionsList = Neptune.EFModels.Entities.NeptunePageTypeHippocampLabelsAndDefinitionsList.Instance;
        public static readonly NeptunePageTypeHippocampAbout HippocampAbout = Neptune.EFModels.Entities.NeptunePageTypeHippocampAbout.Instance;
        public static readonly NeptunePageTypeHippocampProjectsList HippocampProjectsList = Neptune.EFModels.Entities.NeptunePageTypeHippocampProjectsList.Instance;
        public static readonly NeptunePageTypeHippocampProjectInstructions HippocampProjectInstructions = Neptune.EFModels.Entities.NeptunePageTypeHippocampProjectInstructions.Instance;
        public static readonly NeptunePageTypeHippocampProjectBasics HippocampProjectBasics = Neptune.EFModels.Entities.NeptunePageTypeHippocampProjectBasics.Instance;
        public static readonly NeptunePageTypeHippocampProjectAttachments HippocampProjectAttachments = Neptune.EFModels.Entities.NeptunePageTypeHippocampProjectAttachments.Instance;
        public static readonly NeptunePageTypeHippocampTreatmentBMPs HippocampTreatmentBMPs = Neptune.EFModels.Entities.NeptunePageTypeHippocampTreatmentBMPs.Instance;
        public static readonly NeptunePageTypeHippocampDelineations HippocampDelineations = Neptune.EFModels.Entities.NeptunePageTypeHippocampDelineations.Instance;
        public static readonly NeptunePageTypeHippocampModeledPerformance HippocampModeledPerformance = Neptune.EFModels.Entities.NeptunePageTypeHippocampModeledPerformance.Instance;
        public static readonly NeptunePageTypeHippocampReview HippocampReview = Neptune.EFModels.Entities.NeptunePageTypeHippocampReview.Instance;
        public static readonly NeptunePageTypeHippocampPlanningMap HippocampPlanningMap = Neptune.EFModels.Entities.NeptunePageTypeHippocampPlanningMap.Instance;
        public static readonly NeptunePageTypeOCTAM2Tier2GrantProgramMetrics OCTAM2Tier2GrantProgramMetrics = Neptune.EFModels.Entities.NeptunePageTypeOCTAM2Tier2GrantProgramMetrics.Instance;
        public static readonly NeptunePageTypeOCTAM2Tier2GrantProgramDashboard OCTAM2Tier2GrantProgramDashboard = Neptune.EFModels.Entities.NeptunePageTypeOCTAM2Tier2GrantProgramDashboard.Instance;
        public static readonly NeptunePageTypeEditWQMPBoundary EditWQMPBoundary = Neptune.EFModels.Entities.NeptunePageTypeEditWQMPBoundary.Instance;
        public static readonly NeptunePageTypeUploadWQMPs UploadWQMPs = Neptune.EFModels.Entities.NeptunePageTypeUploadWQMPs.Instance;

        public static readonly List<NeptunePageType> All;
        public static readonly List<NeptunePageTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, NeptunePageType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, NeptunePageTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static NeptunePageType()
        {
            All = new List<NeptunePageType> { HomePage, About, OrganizationsList, HomeMapInfo, HomeAdditionalInfo, TreatmentBMP, TreatmentBMPType, Jurisdiction, Assessment, ManageObservationTypesList, ManageTreatmentBMPTypesList, ManageObservationTypeInstructions, ManageObservationTypeObservationInstructions, ManageObservationTypeLabelsAndUnitsInstructions, ManageTreatmentBMPTypeInstructions, ManageCustomAttributeTypeInstructions, ManageCustomAttributeInstructions, ManageCustomAttributeTypesList, Legal, FundingSourcesList, FindABMP, LaunchPad, FieldRecords, RequestSupport, InviteUser, WaterQualityMaintenancePlan, ParcelList, Training, ManagerDashboard, WaterQualityMaintenancePlanOandMVerifications, ModelingHomePage, TrashHomePage, OVTAInstructions, OVTAIndex, TrashModuleProgramOverview, DelineationMap, BulkUploadRequest, TreatmentBMPAssessment, EditOVTAArea, LandUseBlock, ExportAssessmentGeospatialData, HRUCharacteristics, RegionalSubbasins, DelineationReconciliationReport, ViewTreatmentBMPModelingAttributes, UploadTreatmentBMPs, AboutModelingBMPPerformance, BulkUploadFieldVisits, HippocampHomePage, HippocampTraining, HippocampLabelsAndDefinitionsList, HippocampAbout, HippocampProjectsList, HippocampProjectInstructions, HippocampProjectBasics, HippocampProjectAttachments, HippocampTreatmentBMPs, HippocampDelineations, HippocampModeledPerformance, HippocampReview, HippocampPlanningMap, OCTAM2Tier2GrantProgramMetrics, OCTAM2Tier2GrantProgramDashboard, EditWQMPBoundary, UploadWQMPs };
            AllAsDto = new List<NeptunePageTypeDto> { HomePage.AsDto(), About.AsDto(), OrganizationsList.AsDto(), HomeMapInfo.AsDto(), HomeAdditionalInfo.AsDto(), TreatmentBMP.AsDto(), TreatmentBMPType.AsDto(), Jurisdiction.AsDto(), Assessment.AsDto(), ManageObservationTypesList.AsDto(), ManageTreatmentBMPTypesList.AsDto(), ManageObservationTypeInstructions.AsDto(), ManageObservationTypeObservationInstructions.AsDto(), ManageObservationTypeLabelsAndUnitsInstructions.AsDto(), ManageTreatmentBMPTypeInstructions.AsDto(), ManageCustomAttributeTypeInstructions.AsDto(), ManageCustomAttributeInstructions.AsDto(), ManageCustomAttributeTypesList.AsDto(), Legal.AsDto(), FundingSourcesList.AsDto(), FindABMP.AsDto(), LaunchPad.AsDto(), FieldRecords.AsDto(), RequestSupport.AsDto(), InviteUser.AsDto(), WaterQualityMaintenancePlan.AsDto(), ParcelList.AsDto(), Training.AsDto(), ManagerDashboard.AsDto(), WaterQualityMaintenancePlanOandMVerifications.AsDto(), ModelingHomePage.AsDto(), TrashHomePage.AsDto(), OVTAInstructions.AsDto(), OVTAIndex.AsDto(), TrashModuleProgramOverview.AsDto(), DelineationMap.AsDto(), BulkUploadRequest.AsDto(), TreatmentBMPAssessment.AsDto(), EditOVTAArea.AsDto(), LandUseBlock.AsDto(), ExportAssessmentGeospatialData.AsDto(), HRUCharacteristics.AsDto(), RegionalSubbasins.AsDto(), DelineationReconciliationReport.AsDto(), ViewTreatmentBMPModelingAttributes.AsDto(), UploadTreatmentBMPs.AsDto(), AboutModelingBMPPerformance.AsDto(), BulkUploadFieldVisits.AsDto(), HippocampHomePage.AsDto(), HippocampTraining.AsDto(), HippocampLabelsAndDefinitionsList.AsDto(), HippocampAbout.AsDto(), HippocampProjectsList.AsDto(), HippocampProjectInstructions.AsDto(), HippocampProjectBasics.AsDto(), HippocampProjectAttachments.AsDto(), HippocampTreatmentBMPs.AsDto(), HippocampDelineations.AsDto(), HippocampModeledPerformance.AsDto(), HippocampReview.AsDto(), HippocampPlanningMap.AsDto(), OCTAM2Tier2GrantProgramMetrics.AsDto(), OCTAM2Tier2GrantProgramDashboard.AsDto(), EditWQMPBoundary.AsDto(), UploadWQMPs.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, NeptunePageType>(All.ToDictionary(x => x.NeptunePageTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, NeptunePageTypeDto>(AllAsDto.ToDictionary(x => x.NeptunePageTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected NeptunePageType(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName)
        {
            NeptunePageTypeID = neptunePageTypeID;
            NeptunePageTypeName = neptunePageTypeName;
            NeptunePageTypeDisplayName = neptunePageTypeDisplayName;
        }

        [Key]
        public int NeptunePageTypeID { get; private set; }
        public string NeptunePageTypeName { get; private set; }
        public string NeptunePageTypeDisplayName { get; private set; }
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

        public NeptunePageTypeEnum ToEnum => (NeptunePageTypeEnum)GetHashCode();

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
                case NeptunePageTypeEnum.AboutModelingBMPPerformance:
                    return AboutModelingBMPPerformance;
                case NeptunePageTypeEnum.Assessment:
                    return Assessment;
                case NeptunePageTypeEnum.BulkUploadFieldVisits:
                    return BulkUploadFieldVisits;
                case NeptunePageTypeEnum.BulkUploadRequest:
                    return BulkUploadRequest;
                case NeptunePageTypeEnum.DelineationMap:
                    return DelineationMap;
                case NeptunePageTypeEnum.DelineationReconciliationReport:
                    return DelineationReconciliationReport;
                case NeptunePageTypeEnum.EditOVTAArea:
                    return EditOVTAArea;
                case NeptunePageTypeEnum.EditWQMPBoundary:
                    return EditWQMPBoundary;
                case NeptunePageTypeEnum.ExportAssessmentGeospatialData:
                    return ExportAssessmentGeospatialData;
                case NeptunePageTypeEnum.FieldRecords:
                    return FieldRecords;
                case NeptunePageTypeEnum.FindABMP:
                    return FindABMP;
                case NeptunePageTypeEnum.FundingSourcesList:
                    return FundingSourcesList;
                case NeptunePageTypeEnum.HippocampAbout:
                    return HippocampAbout;
                case NeptunePageTypeEnum.HippocampDelineations:
                    return HippocampDelineations;
                case NeptunePageTypeEnum.HippocampHomePage:
                    return HippocampHomePage;
                case NeptunePageTypeEnum.HippocampLabelsAndDefinitionsList:
                    return HippocampLabelsAndDefinitionsList;
                case NeptunePageTypeEnum.HippocampModeledPerformance:
                    return HippocampModeledPerformance;
                case NeptunePageTypeEnum.HippocampPlanningMap:
                    return HippocampPlanningMap;
                case NeptunePageTypeEnum.HippocampProjectAttachments:
                    return HippocampProjectAttachments;
                case NeptunePageTypeEnum.HippocampProjectBasics:
                    return HippocampProjectBasics;
                case NeptunePageTypeEnum.HippocampProjectInstructions:
                    return HippocampProjectInstructions;
                case NeptunePageTypeEnum.HippocampProjectsList:
                    return HippocampProjectsList;
                case NeptunePageTypeEnum.HippocampReview:
                    return HippocampReview;
                case NeptunePageTypeEnum.HippocampTraining:
                    return HippocampTraining;
                case NeptunePageTypeEnum.HippocampTreatmentBMPs:
                    return HippocampTreatmentBMPs;
                case NeptunePageTypeEnum.HomeAdditionalInfo:
                    return HomeAdditionalInfo;
                case NeptunePageTypeEnum.HomeMapInfo:
                    return HomeMapInfo;
                case NeptunePageTypeEnum.HomePage:
                    return HomePage;
                case NeptunePageTypeEnum.HRUCharacteristics:
                    return HRUCharacteristics;
                case NeptunePageTypeEnum.InviteUser:
                    return InviteUser;
                case NeptunePageTypeEnum.Jurisdiction:
                    return Jurisdiction;
                case NeptunePageTypeEnum.LandUseBlock:
                    return LandUseBlock;
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
                case NeptunePageTypeEnum.ModelingHomePage:
                    return ModelingHomePage;
                case NeptunePageTypeEnum.OCTAM2Tier2GrantProgramDashboard:
                    return OCTAM2Tier2GrantProgramDashboard;
                case NeptunePageTypeEnum.OCTAM2Tier2GrantProgramMetrics:
                    return OCTAM2Tier2GrantProgramMetrics;
                case NeptunePageTypeEnum.OrganizationsList:
                    return OrganizationsList;
                case NeptunePageTypeEnum.OVTAIndex:
                    return OVTAIndex;
                case NeptunePageTypeEnum.OVTAInstructions:
                    return OVTAInstructions;
                case NeptunePageTypeEnum.ParcelList:
                    return ParcelList;
                case NeptunePageTypeEnum.RegionalSubbasins:
                    return RegionalSubbasins;
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
                case NeptunePageTypeEnum.UploadTreatmentBMPs:
                    return UploadTreatmentBMPs;
                case NeptunePageTypeEnum.UploadWQMPs:
                    return UploadWQMPs;
                case NeptunePageTypeEnum.ViewTreatmentBMPModelingAttributes:
                    return ViewTreatmentBMPModelingAttributes;
                case NeptunePageTypeEnum.WaterQualityMaintenancePlan:
                    return WaterQualityMaintenancePlan;
                case NeptunePageTypeEnum.WaterQualityMaintenancePlanOandMVerifications:
                    return WaterQualityMaintenancePlanOandMVerifications;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
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
        TreatmentBMPAssessment = 41,
        EditOVTAArea = 42,
        LandUseBlock = 43,
        ExportAssessmentGeospatialData = 44,
        HRUCharacteristics = 45,
        RegionalSubbasins = 46,
        DelineationReconciliationReport = 47,
        ViewTreatmentBMPModelingAttributes = 48,
        UploadTreatmentBMPs = 49,
        AboutModelingBMPPerformance = 50,
        BulkUploadFieldVisits = 51,
        HippocampHomePage = 52,
        HippocampTraining = 53,
        HippocampLabelsAndDefinitionsList = 54,
        HippocampAbout = 55,
        HippocampProjectsList = 56,
        HippocampProjectInstructions = 57,
        HippocampProjectBasics = 58,
        HippocampProjectAttachments = 59,
        HippocampTreatmentBMPs = 60,
        HippocampDelineations = 61,
        HippocampModeledPerformance = 62,
        HippocampReview = 63,
        HippocampPlanningMap = 64,
        OCTAM2Tier2GrantProgramMetrics = 65,
        OCTAM2Tier2GrantProgramDashboard = 66,
        EditWQMPBoundary = 67,
        UploadWQMPs = 68
    }

    public partial class NeptunePageTypeHomePage : NeptunePageType
    {
        private NeptunePageTypeHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHomePage Instance = new NeptunePageTypeHomePage(1, @"HomePage", @"Home Page");
    }

    public partial class NeptunePageTypeAbout : NeptunePageType
    {
        private NeptunePageTypeAbout(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeAbout Instance = new NeptunePageTypeAbout(2, @"About", @"About");
    }

    public partial class NeptunePageTypeOrganizationsList : NeptunePageType
    {
        private NeptunePageTypeOrganizationsList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeOrganizationsList Instance = new NeptunePageTypeOrganizationsList(3, @"OrganizationsList", @"Organizations List");
    }

    public partial class NeptunePageTypeHomeMapInfo : NeptunePageType
    {
        private NeptunePageTypeHomeMapInfo(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHomeMapInfo Instance = new NeptunePageTypeHomeMapInfo(4, @"HomeMapInfo", @"Home Page Map Info");
    }

    public partial class NeptunePageTypeHomeAdditionalInfo : NeptunePageType
    {
        private NeptunePageTypeHomeAdditionalInfo(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHomeAdditionalInfo Instance = new NeptunePageTypeHomeAdditionalInfo(5, @"HomeAdditionalInfo", @"Home Page Additional Info");
    }

    public partial class NeptunePageTypeTreatmentBMP : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMP(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeTreatmentBMP Instance = new NeptunePageTypeTreatmentBMP(6, @"TreatmentBMP", @"Treatment BMP");
    }

    public partial class NeptunePageTypeTreatmentBMPType : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMPType(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeTreatmentBMPType Instance = new NeptunePageTypeTreatmentBMPType(7, @"TreatmentBMPType", @"Treatment BMP Type");
    }

    public partial class NeptunePageTypeJurisdiction : NeptunePageType
    {
        private NeptunePageTypeJurisdiction(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeJurisdiction Instance = new NeptunePageTypeJurisdiction(9, @"Jurisdiction", @"Jurisdiction");
    }

    public partial class NeptunePageTypeAssessment : NeptunePageType
    {
        private NeptunePageTypeAssessment(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeAssessment Instance = new NeptunePageTypeAssessment(10, @"Assessment", @"Assessment");
    }

    public partial class NeptunePageTypeManageObservationTypesList : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageObservationTypesList Instance = new NeptunePageTypeManageObservationTypesList(11, @"ManageObservationTypesList", @"Manage Observation Types List");
    }

    public partial class NeptunePageTypeManageTreatmentBMPTypesList : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageTreatmentBMPTypesList Instance = new NeptunePageTypeManageTreatmentBMPTypesList(12, @"ManageTreatmentBMPTypesList", @"Manage Treatment BMP Types List");
    }

    public partial class NeptunePageTypeManageObservationTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageObservationTypeInstructions Instance = new NeptunePageTypeManageObservationTypeInstructions(13, @"ManageObservationTypeInstructions", @"Manage Observation Type Instructions");
    }

    public partial class NeptunePageTypeManageObservationTypeObservationInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeObservationInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageObservationTypeObservationInstructions Instance = new NeptunePageTypeManageObservationTypeObservationInstructions(14, @"ManageObservationTypeObservationInstructions", @"Manage Observation Type Instructions for Observation Instructions");
    }

    public partial class NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions : NeptunePageType
    {
        private NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions Instance = new NeptunePageTypeManageObservationTypeLabelsAndUnitsInstructions(15, @"ManageObservationTypeLabelsAndUnitsInstructions", @"Manage Observation Type Labels and Units Instructions");
    }

    public partial class NeptunePageTypeManageTreatmentBMPTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageTreatmentBMPTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageTreatmentBMPTypeInstructions Instance = new NeptunePageTypeManageTreatmentBMPTypeInstructions(16, @"ManageTreatmentBMPTypeInstructions", @"Manage Treatment BMP Type Instructions");
    }

    public partial class NeptunePageTypeManageCustomAttributeTypeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageCustomAttributeTypeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageCustomAttributeTypeInstructions Instance = new NeptunePageTypeManageCustomAttributeTypeInstructions(17, @"ManageCustomAttributeTypeInstructions", @"Manage Custom Attribute Type Instructions");
    }

    public partial class NeptunePageTypeManageCustomAttributeInstructions : NeptunePageType
    {
        private NeptunePageTypeManageCustomAttributeInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageCustomAttributeInstructions Instance = new NeptunePageTypeManageCustomAttributeInstructions(18, @"ManageCustomAttributeInstructions", @"Manage Custom Attribute Instructions");
    }

    public partial class NeptunePageTypeManageCustomAttributeTypesList : NeptunePageType
    {
        private NeptunePageTypeManageCustomAttributeTypesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManageCustomAttributeTypesList Instance = new NeptunePageTypeManageCustomAttributeTypesList(19, @"ManageCustomAttributeTypesList", @"Manage Custom Attribute Types List");
    }

    public partial class NeptunePageTypeLegal : NeptunePageType
    {
        private NeptunePageTypeLegal(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeLegal Instance = new NeptunePageTypeLegal(20, @"Legal", @"Legal");
    }

    public partial class NeptunePageTypeFundingSourcesList : NeptunePageType
    {
        private NeptunePageTypeFundingSourcesList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeFundingSourcesList Instance = new NeptunePageTypeFundingSourcesList(21, @"FundingSourcesList", @"Funding Sources List");
    }

    public partial class NeptunePageTypeFindABMP : NeptunePageType
    {
        private NeptunePageTypeFindABMP(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeFindABMP Instance = new NeptunePageTypeFindABMP(22, @"FindABMP", @"Find a BMP");
    }

    public partial class NeptunePageTypeLaunchPad : NeptunePageType
    {
        private NeptunePageTypeLaunchPad(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeLaunchPad Instance = new NeptunePageTypeLaunchPad(23, @"LaunchPad", @"Launch Pad");
    }

    public partial class NeptunePageTypeFieldRecords : NeptunePageType
    {
        private NeptunePageTypeFieldRecords(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeFieldRecords Instance = new NeptunePageTypeFieldRecords(24, @"FieldRecords", @"Field Records");
    }

    public partial class NeptunePageTypeRequestSupport : NeptunePageType
    {
        private NeptunePageTypeRequestSupport(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeRequestSupport Instance = new NeptunePageTypeRequestSupport(25, @"RequestSupport", @"Request Support");
    }

    public partial class NeptunePageTypeInviteUser : NeptunePageType
    {
        private NeptunePageTypeInviteUser(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeInviteUser Instance = new NeptunePageTypeInviteUser(26, @"InviteUser", @"Invite User");
    }

    public partial class NeptunePageTypeWaterQualityMaintenancePlan : NeptunePageType
    {
        private NeptunePageTypeWaterQualityMaintenancePlan(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeWaterQualityMaintenancePlan Instance = new NeptunePageTypeWaterQualityMaintenancePlan(27, @"WaterQualityMaintenancePlan", @"Water Quality Maintenance Plan");
    }

    public partial class NeptunePageTypeParcelList : NeptunePageType
    {
        private NeptunePageTypeParcelList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeParcelList Instance = new NeptunePageTypeParcelList(28, @"ParcelList", @"Parcel List");
    }

    public partial class NeptunePageTypeTraining : NeptunePageType
    {
        private NeptunePageTypeTraining(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeTraining Instance = new NeptunePageTypeTraining(29, @"Training", @"Training");
    }

    public partial class NeptunePageTypeManagerDashboard : NeptunePageType
    {
        private NeptunePageTypeManagerDashboard(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeManagerDashboard Instance = new NeptunePageTypeManagerDashboard(30, @"ManagerDashboard", @"Manager Dashboard");
    }

    public partial class NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications : NeptunePageType
    {
        private NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications Instance = new NeptunePageTypeWaterQualityMaintenancePlanOandMVerifications(31, @"WaterQualityMaintenancePlanOandMVerifications", @"Water Quality Maintenance Plan O&M Verifications");
    }

    public partial class NeptunePageTypeModelingHomePage : NeptunePageType
    {
        private NeptunePageTypeModelingHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeModelingHomePage Instance = new NeptunePageTypeModelingHomePage(32, @"ModelingHomePage", @"Modeling Home Page");
    }

    public partial class NeptunePageTypeTrashHomePage : NeptunePageType
    {
        private NeptunePageTypeTrashHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeTrashHomePage Instance = new NeptunePageTypeTrashHomePage(33, @"TrashHomePage", @"Trash Module Home Page");
    }

    public partial class NeptunePageTypeOVTAInstructions : NeptunePageType
    {
        private NeptunePageTypeOVTAInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeOVTAInstructions Instance = new NeptunePageTypeOVTAInstructions(34, @"OVTAInstructions", @"OVTA Instructions");
    }

    public partial class NeptunePageTypeOVTAIndex : NeptunePageType
    {
        private NeptunePageTypeOVTAIndex(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeOVTAIndex Instance = new NeptunePageTypeOVTAIndex(35, @"OVTAIndex", @"OVTA Index");
    }

    public partial class NeptunePageTypeTrashModuleProgramOverview : NeptunePageType
    {
        private NeptunePageTypeTrashModuleProgramOverview(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeTrashModuleProgramOverview Instance = new NeptunePageTypeTrashModuleProgramOverview(36, @"TrashModuleProgramOverview", @"Trash Module Program Overview");
    }

    public partial class NeptunePageTypeDelineationMap : NeptunePageType
    {
        private NeptunePageTypeDelineationMap(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeDelineationMap Instance = new NeptunePageTypeDelineationMap(37, @"DelineationMap", @"Delineation Map");
    }

    public partial class NeptunePageTypeBulkUploadRequest : NeptunePageType
    {
        private NeptunePageTypeBulkUploadRequest(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeBulkUploadRequest Instance = new NeptunePageTypeBulkUploadRequest(38, @"BulkUploadRequest", @"Bulk Upload Request");
    }

    public partial class NeptunePageTypeTreatmentBMPAssessment : NeptunePageType
    {
        private NeptunePageTypeTreatmentBMPAssessment(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeTreatmentBMPAssessment Instance = new NeptunePageTypeTreatmentBMPAssessment(41, @"TreatmentBMPAssessment", @"Treatment BMP Assessment");
    }

    public partial class NeptunePageTypeEditOVTAArea : NeptunePageType
    {
        private NeptunePageTypeEditOVTAArea(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeEditOVTAArea Instance = new NeptunePageTypeEditOVTAArea(42, @"EditOVTAArea", @"Edit OVTA Area");
    }

    public partial class NeptunePageTypeLandUseBlock : NeptunePageType
    {
        private NeptunePageTypeLandUseBlock(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeLandUseBlock Instance = new NeptunePageTypeLandUseBlock(43, @"LandUseBlock", @"Land Use Block");
    }

    public partial class NeptunePageTypeExportAssessmentGeospatialData : NeptunePageType
    {
        private NeptunePageTypeExportAssessmentGeospatialData(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeExportAssessmentGeospatialData Instance = new NeptunePageTypeExportAssessmentGeospatialData(44, @"ExportAssessmentGeospatialData", @"Export Assessment Geospatial Data");
    }

    public partial class NeptunePageTypeHRUCharacteristics : NeptunePageType
    {
        private NeptunePageTypeHRUCharacteristics(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHRUCharacteristics Instance = new NeptunePageTypeHRUCharacteristics(45, @"HRUCharacteristics", @"HRU Characteristics");
    }

    public partial class NeptunePageTypeRegionalSubbasins : NeptunePageType
    {
        private NeptunePageTypeRegionalSubbasins(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeRegionalSubbasins Instance = new NeptunePageTypeRegionalSubbasins(46, @"RegionalSubbasins", @"Regional Subbasins");
    }

    public partial class NeptunePageTypeDelineationReconciliationReport : NeptunePageType
    {
        private NeptunePageTypeDelineationReconciliationReport(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeDelineationReconciliationReport Instance = new NeptunePageTypeDelineationReconciliationReport(47, @"DelineationReconciliationReport", @"Delineation Reconciliation Report");
    }

    public partial class NeptunePageTypeViewTreatmentBMPModelingAttributes : NeptunePageType
    {
        private NeptunePageTypeViewTreatmentBMPModelingAttributes(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeViewTreatmentBMPModelingAttributes Instance = new NeptunePageTypeViewTreatmentBMPModelingAttributes(48, @"ViewTreatmentBMPModelingAttributes", @"View Treatment BMP Modeling Attributes");
    }

    public partial class NeptunePageTypeUploadTreatmentBMPs : NeptunePageType
    {
        private NeptunePageTypeUploadTreatmentBMPs(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeUploadTreatmentBMPs Instance = new NeptunePageTypeUploadTreatmentBMPs(49, @"UploadTreatmentBMPs", @"Upload Treatment BMPs");
    }

    public partial class NeptunePageTypeAboutModelingBMPPerformance : NeptunePageType
    {
        private NeptunePageTypeAboutModelingBMPPerformance(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeAboutModelingBMPPerformance Instance = new NeptunePageTypeAboutModelingBMPPerformance(50, @"AboutModelingBMPPerformance", @"About Modeling BMP Performance");
    }

    public partial class NeptunePageTypeBulkUploadFieldVisits : NeptunePageType
    {
        private NeptunePageTypeBulkUploadFieldVisits(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeBulkUploadFieldVisits Instance = new NeptunePageTypeBulkUploadFieldVisits(51, @"BulkUploadFieldVisits", @"Bulk Upload Field Visits");
    }

    public partial class NeptunePageTypeHippocampHomePage : NeptunePageType
    {
        private NeptunePageTypeHippocampHomePage(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampHomePage Instance = new NeptunePageTypeHippocampHomePage(52, @"HippocampHomePage", @"Hippocamp Home Page");
    }

    public partial class NeptunePageTypeHippocampTraining : NeptunePageType
    {
        private NeptunePageTypeHippocampTraining(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampTraining Instance = new NeptunePageTypeHippocampTraining(53, @"HippocampTraining", @"Hippocamp Training");
    }

    public partial class NeptunePageTypeHippocampLabelsAndDefinitionsList : NeptunePageType
    {
        private NeptunePageTypeHippocampLabelsAndDefinitionsList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampLabelsAndDefinitionsList Instance = new NeptunePageTypeHippocampLabelsAndDefinitionsList(54, @"HippocampLabelsAndDefinitionsList", @"Hippocamp Labels and Definitions List");
    }

    public partial class NeptunePageTypeHippocampAbout : NeptunePageType
    {
        private NeptunePageTypeHippocampAbout(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampAbout Instance = new NeptunePageTypeHippocampAbout(55, @"HippocampAbout", @"Hippocamp About Page");
    }

    public partial class NeptunePageTypeHippocampProjectsList : NeptunePageType
    {
        private NeptunePageTypeHippocampProjectsList(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampProjectsList Instance = new NeptunePageTypeHippocampProjectsList(56, @"HippocampProjectsList", @"Hippocamp Projects List");
    }

    public partial class NeptunePageTypeHippocampProjectInstructions : NeptunePageType
    {
        private NeptunePageTypeHippocampProjectInstructions(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampProjectInstructions Instance = new NeptunePageTypeHippocampProjectInstructions(57, @"HippocampProjectInstructions", @"Hippocamp Project Instructions Page");
    }

    public partial class NeptunePageTypeHippocampProjectBasics : NeptunePageType
    {
        private NeptunePageTypeHippocampProjectBasics(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampProjectBasics Instance = new NeptunePageTypeHippocampProjectBasics(58, @"HippocampProjectBasics", @"Hippocamp Project Basics");
    }

    public partial class NeptunePageTypeHippocampProjectAttachments : NeptunePageType
    {
        private NeptunePageTypeHippocampProjectAttachments(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampProjectAttachments Instance = new NeptunePageTypeHippocampProjectAttachments(59, @"HippocampProjectAttachments", @"Hippocamp Project Attachments");
    }

    public partial class NeptunePageTypeHippocampTreatmentBMPs : NeptunePageType
    {
        private NeptunePageTypeHippocampTreatmentBMPs(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampTreatmentBMPs Instance = new NeptunePageTypeHippocampTreatmentBMPs(60, @"HippocampTreatmentBMPs", @"Hippocamp Treatment BMPs");
    }

    public partial class NeptunePageTypeHippocampDelineations : NeptunePageType
    {
        private NeptunePageTypeHippocampDelineations(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampDelineations Instance = new NeptunePageTypeHippocampDelineations(61, @"HippocampDelineations", @"Hippocamp Delineations");
    }

    public partial class NeptunePageTypeHippocampModeledPerformance : NeptunePageType
    {
        private NeptunePageTypeHippocampModeledPerformance(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampModeledPerformance Instance = new NeptunePageTypeHippocampModeledPerformance(62, @"HippocampModeledPerformance", @"Hippocamp Modeled Performance");
    }

    public partial class NeptunePageTypeHippocampReview : NeptunePageType
    {
        private NeptunePageTypeHippocampReview(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampReview Instance = new NeptunePageTypeHippocampReview(63, @"HippocampReview", @"Hippocamp Review");
    }

    public partial class NeptunePageTypeHippocampPlanningMap : NeptunePageType
    {
        private NeptunePageTypeHippocampPlanningMap(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeHippocampPlanningMap Instance = new NeptunePageTypeHippocampPlanningMap(64, @"HippocampPlanningMap", @"Hippocamp Planning Map");
    }

    public partial class NeptunePageTypeOCTAM2Tier2GrantProgramMetrics : NeptunePageType
    {
        private NeptunePageTypeOCTAM2Tier2GrantProgramMetrics(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeOCTAM2Tier2GrantProgramMetrics Instance = new NeptunePageTypeOCTAM2Tier2GrantProgramMetrics(65, @"OCTAM2Tier2GrantProgramMetrics", @"OCTA M2 Tier 2 Grant Program Metrics");
    }

    public partial class NeptunePageTypeOCTAM2Tier2GrantProgramDashboard : NeptunePageType
    {
        private NeptunePageTypeOCTAM2Tier2GrantProgramDashboard(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeOCTAM2Tier2GrantProgramDashboard Instance = new NeptunePageTypeOCTAM2Tier2GrantProgramDashboard(66, @"OCTAM2Tier2GrantProgramDashboard", @"OCTA M2 Tier 2 Grant Program Dashboard");
    }

    public partial class NeptunePageTypeEditWQMPBoundary : NeptunePageType
    {
        private NeptunePageTypeEditWQMPBoundary(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeEditWQMPBoundary Instance = new NeptunePageTypeEditWQMPBoundary(67, @"EditWQMPBoundary", @"Refine WQMP Boundary Area");
    }

    public partial class NeptunePageTypeUploadWQMPs : NeptunePageType
    {
        private NeptunePageTypeUploadWQMPs(int neptunePageTypeID, string neptunePageTypeName, string neptunePageTypeDisplayName) : base(neptunePageTypeID, neptunePageTypeName, neptunePageTypeDisplayName) {}
        public static readonly NeptunePageTypeUploadWQMPs Instance = new NeptunePageTypeUploadWQMPs(68, @"UploadWQMPs", @"Bulk Upload Water Quality Management Plans");
    }
}