//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]
namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlan : IHavePrimaryKey
    {
        public int PrimaryKey => WaterQualityManagementPlanID;
        public WaterQualityManagementPlanLandUse? WaterQualityManagementPlanLandUse => WaterQualityManagementPlanLandUseID.HasValue ? WaterQualityManagementPlanLandUse.AllLookupDictionary[WaterQualityManagementPlanLandUseID.Value] : null;
        public WaterQualityManagementPlanPriority? WaterQualityManagementPlanPriority => WaterQualityManagementPlanPriorityID.HasValue ? WaterQualityManagementPlanPriority.AllLookupDictionary[WaterQualityManagementPlanPriorityID.Value] : null;
        public WaterQualityManagementPlanStatus? WaterQualityManagementPlanStatus => WaterQualityManagementPlanStatusID.HasValue ? WaterQualityManagementPlanStatus.AllLookupDictionary[WaterQualityManagementPlanStatusID.Value] : null;
        public WaterQualityManagementPlanDevelopmentType? WaterQualityManagementPlanDevelopmentType => WaterQualityManagementPlanDevelopmentTypeID.HasValue ? WaterQualityManagementPlanDevelopmentType.AllLookupDictionary[WaterQualityManagementPlanDevelopmentTypeID.Value] : null;
        public WaterQualityManagementPlanPermitTerm? WaterQualityManagementPlanPermitTerm => WaterQualityManagementPlanPermitTermID.HasValue ? WaterQualityManagementPlanPermitTerm.AllLookupDictionary[WaterQualityManagementPlanPermitTermID.Value] : null;
        public HydromodificationAppliesType? HydromodificationAppliesType => HydromodificationAppliesTypeID.HasValue ? HydromodificationAppliesType.AllLookupDictionary[HydromodificationAppliesTypeID.Value] : null;
        public TrashCaptureStatusType TrashCaptureStatusType => TrashCaptureStatusType.AllLookupDictionary[TrashCaptureStatusTypeID];
        public WaterQualityManagementPlanModelingApproach WaterQualityManagementPlanModelingApproach => WaterQualityManagementPlanModelingApproach.AllLookupDictionary[WaterQualityManagementPlanModelingApproachID];

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanName = 100;
            public const int MaintenanceContactName = 100;
            public const int MaintenanceContactOrganization = 100;
            public const int MaintenanceContactPhone = 100;
            public const int MaintenanceContactAddress1 = 100;
            public const int MaintenanceContactAddress2 = 100;
            public const int MaintenanceContactCity = 100;
            public const int MaintenanceContactState = 100;
            public const int MaintenanceContactZip = 100;
            public const int RecordNumber = 500;
        }
    }
}