using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Managing Jurisdicton Assets")]
    public class JurisdictionEditFeature : NeptuneFeature
    {
        public JurisdictionEditFeature()
            : base(new List<Role> { Role.JurisdictionEditor, Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin })
        {
        }

        public override bool HasPermissionByPerson(Person person)
        {
            var hasRolePermission = base.HasPermissionByPerson(person);

            if (!hasRolePermission)
            {
                return false;
            }

            return new NeptuneAdminFeature().HasPermissionByPerson(person) || person.StormwaterJurisdictionPeople.Any();
        }
    }
}