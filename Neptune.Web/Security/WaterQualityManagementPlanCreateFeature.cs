using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows creating a WQMP if you are a user with a role")]
    public class WaterQualityManagementPlanCreateFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanCreateFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionEditor })
        {
        }
    }
}
