using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows editing a Treatment BMP's Funding Events if you are assigned to manage that BMP's jurisdiction")]
    public class FundingEventManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<FundingEvent>
    {
        private readonly NeptuneFeatureWithContextImpl<FundingEvent> _lakeTahoeInfoFeatureWithContextImpl;

        public FundingEventManageFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<FundingEvent>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, FundingEvent contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, FundingEvent contextModelObject)
        {
            var canManageStormwaterJurisdiction = person.CanManageStormwaterJurisdiction(contextModelObject.TreatmentBMP.StormwaterJurisdiction);
            if (!canManageStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to manage Treatment BMPs for Jurisdiction {contextModelObject.TreatmentBMP.StormwaterJurisdiction.OrganizationDisplayName}");
            }

            return new PermissionCheckResult();
        }
    }
}