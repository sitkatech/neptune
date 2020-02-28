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
            return new OnlandVisualTrashAssessmentAreaViewFeature().HasPermission(person, contextModelObject);
        }
    }
}