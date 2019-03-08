using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows viewing a WQMP if you are a user with a role")]
    public class WaterQualityManagementPlanViewFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanViewFeature()
            : base(new HashSet<Role> {Role.JurisdictionEditor, Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin})
        {
        }
    }
}
