using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows returning unverified Vield Fisits to edit if you are assigned to edit that jurisdiction")]
    public class FieldVisitReturnToEditFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<FieldVisit>
    {
        private readonly NeptuneFeatureWithContextImpl<FieldVisit> _lakeTahoeInfoFeatureWithContextImpl;

        public FieldVisitReturnToEditFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<FieldVisit>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, FieldVisit contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, FieldVisit contextModelObject)
        {
            var isAssignedToStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(contextModelObject.TreatmentBMP.StormwaterJurisdiction);

            if (!isAssignedToStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to edit Field Visit data for Jurisdiction {contextModelObject.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}