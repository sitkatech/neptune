using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;

namespace Neptune.Tests.Helpers
{
    public static class AuthorizationAttributeHelpers
    {
        // grantedRoles is a primary-constructor parameter captured on BaseAuthorizationAttribute;
        // locate its compiler-generated backing field by type rather than by (unstable) name.
        public static List<RoleEnum> GetGrantedRoles(BaseAuthorizationAttribute attribute)
        {
            var rolesField = typeof(BaseAuthorizationAttribute)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Single(f => typeof(IEnumerable<RoleEnum>).IsAssignableFrom(f.FieldType));
            return ((IEnumerable<RoleEnum>)rolesField.GetValue(attribute)!).ToList();
        }
    }
}
