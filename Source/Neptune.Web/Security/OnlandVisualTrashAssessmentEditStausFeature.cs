using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing an OVTA")]
    public class OnlandVisualTrashAssessmentEditStausFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<OnlandVisualTrashAssessment>
    {
        private readonly NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessment> _lakeTahoeInfoFeatureWithContextImpl;

        public OnlandVisualTrashAssessmentEditStausFeature()
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
            var stormwaterJurisdiction = contextModelObject.StormwaterJurisdiction;

            if (stormwaterJurisdiction != null && !person.CanEditStormwaterJurisdiction(stormwaterJurisdiction))
            {
                return new PermissionCheckResult($"You do not have permission to view or manage OVTAs for {stormwaterJurisdiction.GetOrganizationDisplayName()}");
            }
            return new PermissionCheckResult();
        }
    }
}