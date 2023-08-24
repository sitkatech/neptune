using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows editing Jurisdiction Assets")]
    public class JurisdictionEditFeature : NeptuneFeature
    {
        public JurisdictionEditFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionEditor, RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin })
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