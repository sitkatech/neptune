using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing an OVTA Area")]
    public class OnlandVisualTrashAssessmentAreaViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<OnlandVisualTrashAssessmentArea>
    {
        private readonly NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessmentArea> _lakeTahoeInfoFeatureWithContextImpl;

        public OnlandVisualTrashAssessmentAreaViewFeature()
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
            if (!HasPermissionByPerson(person))
            {
                return new PermissionCheckResult("Person does not have permission by role.");
            }

            if (person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult($"You do not have permission to view or manage OVTA Areas for {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
        }
    }
}