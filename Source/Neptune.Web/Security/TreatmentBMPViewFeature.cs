using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing a Treatment BMP")]
    public class TreatmentBMPViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMP>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMP> _lakeTahoeInfoFeatureWithContextImpl;

        public TreatmentBMPViewFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMP>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, TreatmentBMP contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP contextModelObject)
        {
            if (person.IsAnonymousOrUnassigned() &&
                contextModelObject.StormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID ==
                (int) StormwaterJurisdictionPublicBMPVisibilityTypeEnum.None)
            {
                return new PermissionCheckResult($"You don't have permission to view BMPs for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            // verified BMPs are available for unassigned/anonymous users and therefore all users
            if (contextModelObject.InventoryIsVerified)
            {
                return new PermissionCheckResult();
            }

            var isAssignedToTreatmentBMP = person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID);
            if (!isAssignedToTreatmentBMP)
            {
                return new PermissionCheckResult($"You don't have permission to view BMPs for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}
