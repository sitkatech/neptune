using System.Collections.Generic;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Editing a Treatment BMP")]
    public class TreatmentBMPEditFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMP>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMP> _neptuneFeatureWithContextImpl;

        public TreatmentBMPEditFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor })
        {
            _neptuneFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMP>(this);
            ActionFilter = _neptuneFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP contextModelObject,
            NeptuneDbContext dbContext)
        {
            var isAssignedToTreatmentBMP = person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID);
            if (!isAssignedToTreatmentBMP)
            {
                return new PermissionCheckResult($"You aren't assigned as an editor for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}