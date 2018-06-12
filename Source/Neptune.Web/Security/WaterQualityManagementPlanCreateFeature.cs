using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    public class WaterQualityManagementPlanCreateFeature : NeptuneFeature
    {
        public WaterQualityManagementPlanCreateFeature()
            : base(new List<Role> { Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin })
        {
        }
    }
}
