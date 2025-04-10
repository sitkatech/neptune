using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows viewing the WQMP Annual Report if you are a user with a role")]
    public class WaterQualityManagementPlanAnnualReportFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanAnnualReportFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionEditor })
        {
        }
    }
}
