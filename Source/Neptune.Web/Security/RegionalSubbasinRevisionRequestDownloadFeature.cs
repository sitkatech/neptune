using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Requires JurisdictionManager role")]
    public class RegionalSubbasinRevisionRequestDownloadFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<RegionalSubbasinRevisionRequest>
    {
        private readonly NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest> _lakeTahoeInfoFeatureWithContextImpl;

        public RegionalSubbasinRevisionRequestDownloadFeature() : base(new List<Role> { Role.Admin, Role.SitkaAdmin, Role.JurisdictionManager })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject)
        {
            if (person.IsAdministrator() ||
                    (person.RoleID == Role.JurisdictionManager.RoleID && person.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdictionID).Contains(contextModelObject.TreatmentBMP.StormwaterJurisdictionID)))
            {
                return new PermissionCheckResult();
            }
            
            return new PermissionCheckResult("You do not have permission to download the Regional Subbasin Revision Request geometry.");
        }

        public void DemandPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }

    [SecurityFeatureDescription("Can close revision requests with manager or editor role")]
    public class RegionalSubbasinRevisionRequestCloseFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<RegionalSubbasinRevisionRequest>
    {
        private readonly NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest> _lakeTahoeInfoFeatureWithContextImpl;

        public RegionalSubbasinRevisionRequestCloseFeature() : base(new List<Role> { Role.Admin, Role.SitkaAdmin, Role.JurisdictionEditor, Role.JurisdictionManager })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject)
        {
            if (person.IsAdministrator())
            {
                return new PermissionCheckResult();
            }

            if (person.PersonID == contextModelObject.RequestPersonID)
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult("You do not have permission to close this Regional Subbasin Revision Request.");
        }

        public void DemandPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}
