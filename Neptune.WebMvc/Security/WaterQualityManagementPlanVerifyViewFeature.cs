using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows viewing a WQMP  Verification if you are a user with a role")]
    public class WaterQualityManagementPlanVerifyViewFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanVerifyViewFeature()
            : base(new HashSet<RoleEnum> { RoleEnum.JurisdictionEditor, RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin })
        {
        }
    }
}