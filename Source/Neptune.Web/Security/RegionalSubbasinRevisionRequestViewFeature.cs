using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing a Treatment BMP")]
    public class RegionalSubbasinRevisionRequestViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<RegionalSubbasinRevisionRequest>
    {
        private readonly NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest> _lakeTahoeInfoFeatureWithContextImpl;

        public RegionalSubbasinRevisionRequestViewFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject)
        {
            // they just need permission for the BMP in question...
            return new TreatmentBMPEditFeature().HasPermission(person, contextModelObject.TreatmentBMP);
        }
    }
}
