using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    public class WaterQualityManagementPlanViewFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanViewFeature()
            : base(new HashSet<Role> {Role.JurisdictionEditor, Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin})
        {
        }
    }
}
