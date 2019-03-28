using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing an OVTA Area")]
    public class OnlandVisualTrashAssessmentAreaDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<OnlandVisualTrashAssessmentArea>
    {
        private readonly NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessmentArea> _lakeTahoeInfoFeatureWithContextImpl;

        public OnlandVisualTrashAssessmentAreaDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessmentArea>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, OnlandVisualTrashAssessmentArea contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, OnlandVisualTrashAssessmentArea contextModelObject)
        {
            var stormwaterJurisdiction = contextModelObject.StormwaterJurisdiction;

            if (stormwaterJurisdiction != null && !person.CanEditStormwaterJurisdiction(stormwaterJurisdiction))
            {
                return new PermissionCheckResult($"You do not have permission to view or manage OVTA Areas for {stormwaterJurisdiction.GetOrganizationDisplayName()}");
            }
            return new PermissionCheckResult();
        }
    }
}