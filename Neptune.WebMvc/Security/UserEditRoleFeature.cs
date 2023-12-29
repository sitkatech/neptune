using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Edit a User's Role")]
    public class UserEditRoleFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<Person>
    {
        private readonly NeptuneFeatureWithContextImpl<Person> _neptuneFeatureWithContextImpl;

        public UserEditRoleFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin })
        {
            _neptuneFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<Person>(this);
            ActionFilter = _neptuneFeatureWithContextImpl;
        }


        public PermissionCheckResult HasPermission(Person person, Person contextModelObject,
            NeptuneDbContext dbContext)
        {
            return HasPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Person contextModelObject)
        {
            if (contextModelObject == null)
            {
                return new PermissionCheckResult("The Person who you are requesting to edit doesn't exist.");
            }

            var currentPersonIsAdmin = person.Role == Role.SitkaAdmin || person.Role == Role.Admin;
            var currentPersonIsJurisdictionManager = person.Role == Role.JurisdictionManager;
            var personBeingEditedIsAdmin = contextModelObject.Role == Role.SitkaAdmin || contextModelObject.Role == Role.Admin;

            if (!(currentPersonIsAdmin || currentPersonIsJurisdictionManager))
            {
                return new PermissionCheckResult("You don\'t have permission to edit this user's role.");
            }

            if (!currentPersonIsAdmin && personBeingEditedIsAdmin)
            {
                return new PermissionCheckResult("You don\'t have permission to edit this user's role.");
            }

            return new PermissionCheckResult();
        }
    }
}