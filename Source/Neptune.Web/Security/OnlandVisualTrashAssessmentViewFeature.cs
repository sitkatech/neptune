using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing an OVTA")]
    public class OnlandVisualTrashAssessmentViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<OnlandVisualTrashAssessment>
    {
        private readonly NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessment> _lakeTahoeInfoFeatureWithContextImpl;

        public OnlandVisualTrashAssessmentViewFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessment>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, OnlandVisualTrashAssessment contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, OnlandVisualTrashAssessment contextModelObject)
        {
            if (!person.CanEditStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult($"You do not have permission to view or manage OVTAs for {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }
            return new PermissionCheckResult();
        }
    }
}