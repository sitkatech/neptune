using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows viewing a WQMP  Verification if you are a user with a role")]
    public class WaterQualityManagementPlanVerifyViewFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanVerifyViewFeature()
            : base(new HashSet<Role> { Role.JurisdictionEditor, Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin })
        {
        }
    }
}