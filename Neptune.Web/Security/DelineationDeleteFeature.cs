using System.Collections.Generic;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows deleting a Delineation if you are assigned to manage its BMP's jurisdiction")]
    public class DelineationDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<Delineation>
    {
        private readonly NeptuneFeatureWithContextImpl<Delineation> _lakeTahoeInfoFeatureWithContextImpl;
        public string FeatureName { get; } // todo

        public DelineationDeleteFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<Delineation>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Delineation contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Delineation contextModelObject)
        {
            var canManageStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(contextModelObject.TreatmentBMP.StormwaterJurisdictionID);
            if (!canManageStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to manage Treatment BMPs for Jurisdiction {contextModelObject.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            if (!(person.IsAdministrator() || person.Role == Role.JurisdictionManager))
            {
                return new PermissionCheckResult($"You do not have permission to delete Treatment BMP delineation for Jurisdiction {contextModelObject.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}