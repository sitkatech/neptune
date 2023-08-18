//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]
namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanVerify : IHavePrimaryKey
    {
        public int PrimaryKey => WaterQualityManagementPlanVerifyID;
        public WaterQualityManagementPlanVerifyType WaterQualityManagementPlanVerifyType => WaterQualityManagementPlanVerifyType.AllLookupDictionary[WaterQualityManagementPlanVerifyTypeID];
        public WaterQualityManagementPlanVisitStatus WaterQualityManagementPlanVisitStatus => WaterQualityManagementPlanVisitStatus.AllLookupDictionary[WaterQualityManagementPlanVisitStatusID];
        public WaterQualityManagementPlanVerifyStatus WaterQualityManagementPlanVerifyStatus => WaterQualityManagementPlanVerifyStatusID.HasValue ? WaterQualityManagementPlanVerifyStatus.AllLookupDictionary[WaterQualityManagementPlanVerifyStatusID.Value] : null;
    }
}