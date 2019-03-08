using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows creating a WQMP if you are a user with a role")]
    public class WaterQualityManagementPlanCreateFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanCreateFeature()
            : base(new List<Role> { Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin, Role.JurisdictionEditor })
        {
        }
    }
}
